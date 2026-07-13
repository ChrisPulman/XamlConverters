// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.ComponentModel;
using System.Globalization;
using Avalonia;
using Avalonia.Data;

namespace CP.Xaml.Converters.Avalonia.Internal;

internal static class ConversionHelpers
{
    /// <summary>
    /// Gets the Avalonia do-nothing binding sentinel.
    /// </summary>
    public static object DoNothing => BindingOperations.DoNothing;

    /// <summary>
    /// Gets the Avalonia unset-value sentinel.
    /// </summary>
    public static object UnsetValue => AvaloniaProperty.UnsetValue;

    /// <summary>
    /// Provides converter behavior.
    /// </summary>
    public static bool IsUnset(object? value) =>
        ReferenceEquals(value, AvaloniaProperty.UnsetValue) || ReferenceEquals(value, BindingOperations.DoNothing);

    /// <summary>
    /// Provides converter behavior.
    /// </summary>
    public static bool IsNullLike(object? value) => value is null || IsUnset(value);

    /// <summary>
    /// Provides converter behavior.
    /// </summary>
    public static bool IsTrue(object? value, CultureInfo culture)
    {
        if (value is bool boolean)
        {
            return boolean;
        }

        return value is not null && bool.TryParse(Convert.ToString(value, culture), out var result) && result;
    }

    /// <summary>
    /// Provides converter behavior.
    /// </summary>
    public static bool TryConvert(object? value, Type targetType, CultureInfo culture, out object? result)
    {
        ArgumentNullException.ThrowIfNull(targetType);

        var effectiveType = Nullable.GetUnderlyingType(targetType) ?? targetType;
        if (IsNullLike(value))
        {
            result = null;
            return !effectiveType.IsValueType || Nullable.GetUnderlyingType(targetType) is not null;
        }

        if (targetType == typeof(object) || targetType.IsInstanceOfType(value))
        {
            result = value;
            return true;
        }

        var text = Convert.ToString(value, culture);

        try
        {
            if (effectiveType.IsEnum)
            {
                if (value is string enumText)
                {
                    result = Enum.Parse(effectiveType, enumText, ignoreCase: true);
                    return true;
                }

                var underlying = Enum.GetUnderlyingType(effectiveType);
                var numeric = Convert.ChangeType(value, underlying, culture);
                result = Enum.ToObject(effectiveType, numeric!);
                return true;
            }

            if (effectiveType == typeof(Guid) && Guid.TryParse(text, out var guid))
            {
                result = guid;
                return true;
            }

            if (effectiveType == typeof(Uri) && Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out var uri))
            {
                result = uri;
                return true;
            }

            if (effectiveType == typeof(TimeSpan) && TimeSpan.TryParse(text, culture, out var timeSpan))
            {
                result = timeSpan;
                return true;
            }

            if (effectiveType == typeof(DateTimeOffset) && DateTimeOffset.TryParse(text, culture, DateTimeStyles.None, out var dateTimeOffset))
            {
                result = dateTimeOffset;
                return true;
            }

            if (effectiveType == typeof(DateOnly) && DateOnly.TryParse(text, culture, DateTimeStyles.None, out var dateOnly))
            {
                result = dateOnly;
                return true;
            }

            if (effectiveType == typeof(TimeOnly) && TimeOnly.TryParse(text, culture, DateTimeStyles.None, out var timeOnly))
            {
                result = timeOnly;
                return true;
            }

            var targetConverter = TypeDescriptor.GetConverter(effectiveType);
            if (targetConverter.CanConvertFrom(value!.GetType()))
            {
                result = targetConverter.ConvertFrom(null, culture, value);
                return true;
            }

            if (targetConverter.CanConvertFrom(typeof(string)) && text is not null)
            {
                result = targetConverter.ConvertFrom(null, culture, text);
                return true;
            }

            var sourceConverter = TypeDescriptor.GetConverter(value!.GetType());
            if (sourceConverter.CanConvertTo(effectiveType))
            {
                result = sourceConverter.ConvertTo(null, culture, value, effectiveType);
                return true;
            }

            result = Convert.ChangeType(value, effectiveType, culture);
            return true;
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException or InvalidCastException or NotSupportedException or OverflowException)
        {
            result = null;
            return false;
        }
    }

    /// <summary>
    /// Provides converter behavior.
    /// </summary>
    public static bool TryDecimal(object? value, CultureInfo culture, out decimal result)
    {
        if (IsNullLike(value))
        {
            result = default;
            return false;
        }

        try
        {
            result = Convert.ToDecimal(value, culture);
            return true;
        }
        catch (Exception ex) when (ex is FormatException or InvalidCastException or OverflowException)
        {
            result = default;
            return false;
        }
    }

    /// <summary>
    /// Provides converter behavior.
    /// </summary>
    public static object ConvertDecimal(decimal value, Type targetType, CultureInfo culture)
    {
        var effectiveType = Nullable.GetUnderlyingType(targetType) ?? targetType;
        if (effectiveType == typeof(object) || effectiveType == typeof(decimal))
        {
            return value;
        }

        return TryConvert(value, effectiveType, culture, out var result) ? result! : value;
    }

    /// <summary>
    /// Provides converter behavior.
    /// </summary>
    public static bool TryCount(object? value, out int count)
    {
        switch (value)
        {
            case string text:
                count = text.Length;
                return true;
            case ICollection collection:
                count = collection.Count;
                return true;
            case IEnumerable enumerable:
                count = 0;
                foreach (var unused in enumerable)
                {
                    count++;
                }

                return true;
            default:
                count = 0;
                return false;
        }
    }

    /// <summary>
    /// Provides converter behavior.
    /// </summary>
    public static bool Compare(decimal left, string expression, CultureInfo culture)
    {
        expression = expression.Trim();
        foreach (var op in new[] { ">=", "<=", "==", "!=", ">", "<" })
        {
            if (!expression.StartsWith(op, StringComparison.Ordinal) ||
                !decimal.TryParse(expression.Substring(op.Length).Trim(), NumberStyles.Any, culture, out var right))
            {
                continue;
            }

            return op switch
            {
                ">=" => left >= right,
                "<=" => left <= right,
                "==" => left == right,
                "!=" => left != right,
                ">" => left > right,
                "<" => left < right,
                _ => false,
            };
        }

        return false;
    }
}

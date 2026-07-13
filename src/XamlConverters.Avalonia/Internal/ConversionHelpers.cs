// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections;
using System.ComponentModel;
using System.Globalization;
using Avalonia;
using Avalonia.Data;

namespace CP.Xaml.Converters.Avalonia.Internal;

/// <summary>Provides shared conversion and binding-sentinel helpers for Avalonia converters.</summary>
internal static class ConversionHelpers
{
    /// <summary>The parsers used for well-known non-BCL-convertible value types.</summary>
    private static readonly Dictionary<Type, Func<string?, CultureInfo, (bool Success, object? Result)>> TextParsers = new()
    {
        [typeof(Guid)] = TryParseGuid,
        [typeof(Uri)] = TryParseUri,
        [typeof(TimeSpan)] = TryParseTimeSpan,
        [typeof(DateTimeOffset)] = TryParseDateTimeOffset,
        [typeof(DateOnly)] = TryParseDateOnly,
        [typeof(TimeOnly)] = TryParseTimeOnly,
    };

    /// <summary>Gets the Avalonia do-nothing binding sentinel.</summary>
    public static object DoNothing => BindingOperations.DoNothing;

    /// <summary>Gets the Avalonia unset-value sentinel.</summary>
    public static object UnsetValue => AvaloniaProperty.UnsetValue;

    /// <summary>Determines whether a value is an Avalonia binding sentinel.</summary>
    /// <param name="value">The value to inspect.</param>
    /// <returns><see langword="true"/> for an unset or do-nothing sentinel; otherwise, <see langword="false"/>.</returns>
    public static bool IsUnset(object? value) =>
        ReferenceEquals(value, AvaloniaProperty.UnsetValue) || ReferenceEquals(value, BindingOperations.DoNothing);

    /// <summary>Determines whether a value is null or a binding sentinel.</summary>
    /// <param name="value">The value to inspect.</param>
    /// <returns><see langword="true"/> when the value is null-like; otherwise, <see langword="false"/>.</returns>
    public static bool IsNullLike(object? value) => value is null || IsUnset(value);

    /// <summary>Converts a value to a Boolean result.</summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns><see langword="true"/> when the value represents true; otherwise, <see langword="false"/>.</returns>
    public static bool IsTrue(object? value, CultureInfo culture)
    {
        return value is bool boolean ? boolean : value is not null && bool.TryParse(Convert.ToString(value, culture), out var result) && result;
    }

    /// <summary>Attempts to convert a value to a requested type.</summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <param name="result">The converted value when conversion succeeds.</param>
    /// <returns><see langword="true"/> when conversion succeeds; otherwise, <see langword="false"/>.</returns>
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
                result = ConvertEnum(value!, effectiveType, culture);
                return true;
            }

            if (TryParseKnownText(text, effectiveType, culture, out result))
            {
                return true;
            }

            if (TryTypeConverters(value!, effectiveType, text, culture, out result))
            {
                return true;
            }

            result = Convert.ChangeType(value, effectiveType, culture);
            return true;
        }
        catch (Exception ex) when (IsConversionException(ex))
        {
            result = null;
            return false;
        }
    }

    /// <summary>Converts a string or numeric value to an enum.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="enumType">The enum type.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The converted enum value.</returns>
    public static object ConvertEnum(object value, Type enumType, CultureInfo culture)
    {
        if (value is string enumText)
        {
            return Enum.Parse(enumType, enumText, ignoreCase: true);
        }

        var underlyingType = Enum.GetUnderlyingType(enumType);
        var numericValue = Convert.ChangeType(value, underlyingType, culture);
        return Enum.ToObject(enumType, numericValue!);
    }

    /// <summary>Attempts conversion through source and destination type converters.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <param name="text">The formatted source text.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <param name="result">The converted result.</param>
    /// <returns><see langword="true"/> when a type converter succeeds; otherwise, <see langword="false"/>.</returns>
    public static bool TryTypeConverters(object value, Type targetType, string? text, CultureInfo culture, out object? result)
    {
        var targetConverter = TypeDescriptor.GetConverter(targetType);
        if (targetConverter.CanConvertFrom(value.GetType()))
        {
            result = targetConverter.ConvertFrom(null, culture, value);
            return true;
        }

        if (text is not null && targetConverter.CanConvertFrom(typeof(string)))
        {
            result = targetConverter.ConvertFrom(null, culture, text);
            return true;
        }

        var sourceConverter = TypeDescriptor.GetConverter(value.GetType());
        if (sourceConverter.CanConvertTo(targetType))
        {
            result = sourceConverter.ConvertTo(null, culture, value, targetType);
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>Attempts to parse a GUID.</summary>
    /// <param name="text">The text to parse.</param>
    /// <param name="culture">The unused parsing culture.</param>
    /// <returns>The parsing status and parsed result.</returns>
    public static (bool Success, object? Result) TryParseGuid(string? text, CultureInfo culture)
    {
        var success = Guid.TryParse(text, out var value);
        return (success, success ? value : null);
    }

    /// <summary>Attempts to parse a URI.</summary>
    /// <param name="text">The text to parse.</param>
    /// <param name="culture">The unused parsing culture.</param>
    /// <returns>The parsing status and parsed result.</returns>
    public static (bool Success, object? Result) TryParseUri(string? text, CultureInfo culture)
    {
        var success = Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out var value);
        return (success, value);
    }

    /// <summary>Attempts to parse a time span.</summary>
    /// <param name="text">The text to parse.</param>
    /// <param name="culture">The parsing culture.</param>
    /// <returns>The parsing status and parsed result.</returns>
    public static (bool Success, object? Result) TryParseTimeSpan(string? text, CultureInfo culture)
    {
        var success = TimeSpan.TryParse(text, culture, out var value);
        return (success, success ? value : null);
    }

    /// <summary>Attempts to parse a date and time with an offset.</summary>
    /// <param name="text">The text to parse.</param>
    /// <param name="culture">The parsing culture.</param>
    /// <returns>The parsing status and parsed result.</returns>
    public static (bool Success, object? Result) TryParseDateTimeOffset(string? text, CultureInfo culture)
    {
        var success = DateTimeOffset.TryParse(text, culture, DateTimeStyles.None, out var value);
        return (success, success ? value : null);
    }

    /// <summary>Attempts to parse a date.</summary>
    /// <param name="text">The text to parse.</param>
    /// <param name="culture">The parsing culture.</param>
    /// <returns>The parsing status and parsed result.</returns>
    public static (bool Success, object? Result) TryParseDateOnly(string? text, CultureInfo culture)
    {
        var success = DateOnly.TryParse(text, culture, DateTimeStyles.None, out var value);
        return (success, success ? value : null);
    }

    /// <summary>Attempts to parse a time.</summary>
    /// <param name="text">The text to parse.</param>
    /// <param name="culture">The parsing culture.</param>
    /// <returns>The parsing status and parsed result.</returns>
    public static (bool Success, object? Result) TryParseTimeOnly(string? text, CultureInfo culture)
    {
        var success = TimeOnly.TryParse(text, culture, DateTimeStyles.None, out var value);
        return (success, success ? value : null);
    }

    /// <summary>Attempts to convert a value to <see cref="decimal"/>.</summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <param name="result">The decimal value when conversion succeeds.</param>
    /// <returns><see langword="true"/> when conversion succeeds; otherwise, <see langword="false"/>.</returns>
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

    /// <summary>Converts a decimal result to the requested binding target type.</summary>
    /// <param name="value">The decimal value.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The converted value, or the original decimal when conversion is unavailable.</returns>
    public static object ConvertDecimal(decimal value, Type targetType, CultureInfo culture)
    {
        var effectiveType = Nullable.GetUnderlyingType(targetType) ?? targetType;
        if (effectiveType == typeof(object) || effectiveType == typeof(decimal))
        {
            return value;
        }

        return TryConvert(value, effectiveType, culture, out var result) ? result! : value;
    }

    /// <summary>Attempts to obtain the number of elements in a supported value.</summary>
    /// <param name="value">The value to count.</param>
    /// <param name="count">The resulting element count.</param>
    /// <returns><see langword="true"/> when the value can be counted; otherwise, <see langword="false"/>.</returns>
    public static bool TryCount(object? value, out int count)
    {
        switch (value)
        {
            case string text:
                {
                    count = text.Length;
                    return true;
                }

            case ICollection collection:
                {
                    count = collection.Count;
                    return true;
                }

            case IEnumerable enumerable:
                {
                    count = 0;
                    foreach (var unused in enumerable)
                    {
                        count++;
                    }

                    return true;
                }

            default:
                {
                    count = 0;
                    return false;
                }
        }
    }

    /// <summary>Evaluates a comparison expression against a decimal value.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="expression">The operator and right operand.</param>
    /// <param name="culture">The parsing culture.</param>
    /// <returns><see langword="true"/> when the comparison succeeds; otherwise, <see langword="false"/>.</returns>
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

    /// <summary>Determines whether an exception represents a recoverable conversion failure.</summary>
    /// <param name="exception">The exception to inspect.</param>
    /// <returns><see langword="true"/> for a recoverable conversion failure; otherwise, <see langword="false"/>.</returns>
    private static bool IsConversionException(Exception exception) =>
        exception is ArgumentException or FormatException or InvalidCastException or NotSupportedException or OverflowException;

    /// <summary>Attempts to parse a value through a registered textual parser.</summary>
    /// <param name="text">The text to parse.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <param name="culture">The parsing culture.</param>
    /// <param name="result">The parsed result.</param>
    /// <returns><see langword="true"/> when parsing succeeds; otherwise, <see langword="false"/>.</returns>
    private static bool TryParseKnownText(string? text, Type targetType, CultureInfo culture, out object? result)
    {
        if (TextParsers.TryGetValue(targetType, out var parser))
        {
            var parsed = parser(text, culture);
            result = parsed.Result;
            return parsed.Success;
        }

        result = null;
        return false;
    }
}

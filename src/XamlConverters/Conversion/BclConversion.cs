// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Globalization;

namespace CP.Xaml.Converters;

/// <summary>
/// Provides the common, platform-independent coercion rules used by the BCL converters.
/// </summary>
internal static class BclConversion
{
    /// <summary>
    /// Attempts to convert a value to the requested target type without throwing conversion exceptions.
    /// </summary>
    /// <param name="value">The source value.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <param name="culture">The culture to use for parsing and formatting.</param>
    /// <param name="result">The converted result when conversion succeeds.</param>
    /// <returns><see langword="true"/> when conversion succeeds; otherwise, <see langword="false"/>.</returns>
    public static bool TryChangeType(object? value, Type targetType, CultureInfo culture, out object? result)
    {
        if (targetType == null)
        {
            throw new ArgumentNullException(nameof(targetType));
        }

        var nullableType = Nullable.GetUnderlyingType(targetType);
        var destinationType = nullableType ?? targetType;

        if (value == null || value == DBNull.Value)
        {
            result = null;
            return nullableType != null || !destinationType.IsValueType;
        }

        if (destinationType == typeof(object) || destinationType.IsInstanceOfType(value))
        {
            result = value;
            return true;
        }

        var text = value as string;
        if (nullableType != null && string.IsNullOrWhiteSpace(text))
        {
            result = null;
            return true;
        }

        try
        {
            if (destinationType == typeof(string))
            {
                result = value is IFormattable formattable
                    ? formattable.ToString(null, culture)
                    : value.ToString() ?? string.Empty;
                return true;
            }

            if (destinationType.IsEnum)
            {
                result = text != null
                    ? Enum.Parse(destinationType, text, ignoreCase: true)
                    : Enum.ToObject(destinationType, value);
                return true;
            }

            if (destinationType == typeof(Guid))
            {
                if (value is byte[] bytes && bytes.Length == 16)
                {
                    result = new Guid(bytes);
                    return true;
                }

                if (Guid.TryParse(value.ToString(), out var guid))
                {
                    result = guid;
                    return true;
                }
            }

            if (destinationType == typeof(Uri) && Uri.TryCreate(value.ToString(), UriKind.RelativeOrAbsolute, out var uri))
            {
                result = uri;
                return true;
            }

            if (destinationType == typeof(TimeSpan) && TimeSpan.TryParse(value.ToString(), culture, out var timeSpan))
            {
                result = timeSpan;
                return true;
            }

            if (destinationType == typeof(DateTimeOffset))
            {
                if (value is DateTime dateTime)
                {
                    result = new DateTimeOffset(dateTime);
                    return true;
                }

                if (DateTimeOffset.TryParse(value.ToString(), culture, DateTimeStyles.AllowWhiteSpaces, out var dateTimeOffset))
                {
                    result = dateTimeOffset;
                    return true;
                }
            }

            if (destinationType == typeof(DateTime) && value is DateTimeOffset offset)
            {
                result = offset.DateTime;
                return true;
            }

            if (destinationType == typeof(Type) && text != null)
            {
                result = Type.GetType(text, throwOnError: false, ignoreCase: true);
                return result != null;
            }

            if (destinationType == typeof(bool) && text != null)
            {
                if (bool.TryParse(text, out var boolean))
                {
                    result = boolean;
                    return true;
                }

                if (string.Equals(text, "yes", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(text, "on", StringComparison.OrdinalIgnoreCase))
                {
                    result = true;
                    return true;
                }

                if (string.Equals(text, "no", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(text, "off", StringComparison.OrdinalIgnoreCase))
                {
                    result = false;
                    return true;
                }
            }

            if (text != null && IsNumericType(destinationType) && TryParseNumber(text, destinationType, culture, out result))
            {
                return true;
            }

            var destinationConverter = TypeDescriptor.GetConverter(destinationType);
            if (destinationConverter.CanConvertFrom(value.GetType()))
            {
                result = destinationConverter.ConvertFrom(null, culture, value);
                return true;
            }

            var sourceConverter = TypeDescriptor.GetConverter(value.GetType());
            if (sourceConverter.CanConvertTo(destinationType))
            {
                result = sourceConverter.ConvertTo(null, culture, value, destinationType);
                return true;
            }

            result = System.Convert.ChangeType(value, destinationType, culture);
            return true;
        }
        catch (Exception exception) when (exception is ArgumentException
            || exception is FormatException
            || exception is InvalidCastException
            || exception is NotSupportedException
            || exception is OverflowException)
        {
            result = null;
            return false;
        }
    }

    /// <summary>
    /// Determines whether the supplied type is one of the built-in numeric types.
    /// </summary>
    /// <param name="type">The type to inspect.</param>
    /// <returns><see langword="true"/> for a built-in numeric type.</returns>
    public static bool IsNumericType(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;
        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Byte:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.SByte:
            case TypeCode.Single:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
                return true;
            default:
                return false;
        }
    }

    private static bool TryParseNumber(string text, Type destinationType, CultureInfo culture, out object? result)
    {
        const NumberStyles integerStyles = NumberStyles.Integer | NumberStyles.AllowThousands;
        const NumberStyles floatingStyles = NumberStyles.Float | NumberStyles.AllowThousands;
        const NumberStyles decimalStyles = NumberStyles.Number | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowParentheses;

        switch (Type.GetTypeCode(destinationType))
        {
            case TypeCode.Byte when byte.TryParse(text, integerStyles, culture, out var byteValue):
                result = byteValue;
                return true;
            case TypeCode.Decimal when decimal.TryParse(text, decimalStyles, culture, out var decimalValue):
                result = decimalValue;
                return true;
            case TypeCode.Double when double.TryParse(text, floatingStyles, culture, out var doubleValue):
                result = doubleValue;
                return true;
            case TypeCode.Int16 when short.TryParse(text, integerStyles, culture, out var int16Value):
                result = int16Value;
                return true;
            case TypeCode.Int32 when int.TryParse(text, integerStyles, culture, out var int32Value):
                result = int32Value;
                return true;
            case TypeCode.Int64 when long.TryParse(text, integerStyles, culture, out var int64Value):
                result = int64Value;
                return true;
            case TypeCode.SByte when sbyte.TryParse(text, integerStyles, culture, out var sbyteValue):
                result = sbyteValue;
                return true;
            case TypeCode.Single when float.TryParse(text, floatingStyles, culture, out var singleValue):
                result = singleValue;
                return true;
            case TypeCode.UInt16 when ushort.TryParse(text, integerStyles, culture, out var uint16Value):
                result = uint16Value;
                return true;
            case TypeCode.UInt32 when uint.TryParse(text, integerStyles, culture, out var uint32Value):
                result = uint32Value;
                return true;
            case TypeCode.UInt64 when ulong.TryParse(text, integerStyles, culture, out var uint64Value):
                result = uint64Value;
                return true;
            default:
                result = null;
                return false;
        }
    }
}

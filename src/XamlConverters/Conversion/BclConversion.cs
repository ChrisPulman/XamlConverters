// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Globalization;

namespace CP.Xaml.Converters;

/// <summary>Provides the common, platform-independent coercion rules used by the BCL converters.</summary>
internal static class BclConversion
{
    /// <summary>The number of bytes in a GUID.</summary>
    private const int GuidByteCount = 16;

    /// <summary>The number styles used for integral values.</summary>
    private const NumberStyles IntegerStyles = NumberStyles.Integer | NumberStyles.AllowThousands;

    /// <summary>The number styles used for floating-point values.</summary>
    private const NumberStyles FloatingStyles = NumberStyles.Float | NumberStyles.AllowThousands;

    /// <summary>The number styles used for decimal values.</summary>
    private const NumberStyles DecimalStyles =
        NumberStyles.Number | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowParentheses;

    /// <summary>Converters for well-known types not handled consistently by <see cref="System.Convert"/>.</summary>
    private static readonly Dictionary<
        Type,
        Func<object, string?, CultureInfo, ConversionAttempt>
    > SpecialConverters = new()
    {
        [typeof(Guid)] = TryConvertGuid,
        [typeof(Uri)] = TryConvertUri,
        [typeof(TimeSpan)] = TryConvertTimeSpan,
        [typeof(DateTimeOffset)] = TryConvertDateTimeOffset,
        [typeof(DateTime)] = TryConvertDateTime,
        [typeof(Type)] = TryConvertType,
        [typeof(bool)] = TryConvertBoolean,
    };

    /// <summary>Parsers for the built-in numeric types.</summary>
    private static readonly Dictionary<TypeCode, Func<string, CultureInfo, object>> NumericParsers =
        new()
        {
            [TypeCode.Byte] = (text, culture) => byte.Parse(text, IntegerStyles, culture),
            [TypeCode.Decimal] = (text, culture) => decimal.Parse(text, DecimalStyles, culture),
            [TypeCode.Double] = (text, culture) => double.Parse(text, FloatingStyles, culture),
            [TypeCode.Int16] = (text, culture) => short.Parse(text, IntegerStyles, culture),
            [TypeCode.Int32] = (text, culture) => int.Parse(text, IntegerStyles, culture),
            [TypeCode.Int64] = (text, culture) => long.Parse(text, IntegerStyles, culture),
            [TypeCode.SByte] = (text, culture) => sbyte.Parse(text, IntegerStyles, culture),
            [TypeCode.Single] = (text, culture) => float.Parse(text, FloatingStyles, culture),
            [TypeCode.UInt16] = (text, culture) => ushort.Parse(text, IntegerStyles, culture),
            [TypeCode.UInt32] = (text, culture) => uint.Parse(text, IntegerStyles, culture),
            [TypeCode.UInt64] = (text, culture) => ulong.Parse(text, IntegerStyles, culture),
        };

    /// <summary>Represents the outcome of a direct conversion attempt.</summary>
    private enum ConversionResult
    {
        /// <summary>The conversion requires another strategy.</summary>
        NotHandled,

        /// <summary>The conversion succeeded.</summary>
        Succeeded,

        /// <summary>The conversion was handled but failed.</summary>
        Failed,
    }

    /// <summary>Attempts to convert a value without throwing conversion exceptions.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <param name="culture">The culture to use for parsing and formatting.</param>
    /// <param name="result">The converted result when conversion succeeds.</param>
    /// <returns><see langword="true"/> when conversion succeeds; otherwise, <see langword="false"/>.</returns>
    public static bool TryChangeType(
        object? value,
        Type targetType,
        CultureInfo culture,
        out object? result)
    {
        if (targetType is null)
        {
            throw new ArgumentNullException(nameof(targetType));
        }

        var nullableType = Nullable.GetUnderlyingType(targetType);
        var destinationType = nullableType ?? targetType;
        var text = value as string;
        var directResult = TryDirectConversion(
            value,
            destinationType,
            nullableType,
            text,
            culture,
            out result);
        if (directResult != ConversionResult.NotHandled)
        {
            return directResult == ConversionResult.Succeeded;
        }

        try
        {
            if (destinationType.IsEnum)
            {
                result = ConvertEnum(value!, destinationType, text);
                return true;
            }

            if (TrySpecialConversion(value!, text, destinationType, culture, out result))
            {
                return true;
            }

            if (TryConvertNumeric(text, destinationType, culture, out result))
            {
                return true;
            }

            if (TryTypeConverters(value!, destinationType, culture, out result))
            {
                return true;
            }

            result = System.Convert.ChangeType(value, destinationType, culture);
            return true;
        }
        catch (Exception exception) when (IsConversionException(exception))
        {
            result = null;
            return false;
        }
    }

    /// <summary>Determines whether the supplied type is one of the built-in numeric types.</summary>
    /// <param name="type">The type to inspect.</param>
    /// <returns><see langword="true"/> for a built-in numeric type.</returns>
    public static bool IsNumericType(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;
        return NumericParsers.ContainsKey(Type.GetTypeCode(type));
    }

    /// <summary>Handles null, identity, nullable-string, and string conversions.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="destinationType">The non-nullable destination type.</param>
    /// <param name="nullableType">The nullable destination type, when applicable.</param>
    /// <param name="text">The source value as text, when it is already a string.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <param name="result">The conversion result.</param>
    /// <returns>The direct-conversion status.</returns>
    private static ConversionResult TryDirectConversion(
        object? value,
        Type destinationType,
        Type? nullableType,
        string? text,
        CultureInfo culture,
        out object? result)
    {
        if (IsNullValue(value))
        {
            result = null;
            return CanAcceptNull(destinationType, nullableType)
                ? ConversionResult.Succeeded
                : ConversionResult.Failed;
        }

        if (IsDirectlyAssignable(value!, destinationType))
        {
            result = value;
            return ConversionResult.Succeeded;
        }

        if (IsEmptyNullableText(text, nullableType))
        {
            result = null;
            return ConversionResult.Succeeded;
        }

        if (destinationType == typeof(string))
        {
            result = value is IFormattable formattable
                ? formattable.ToString(null, culture)
                : value!.ToString() ?? string.Empty;
            return ConversionResult.Succeeded;
        }

        result = null;
        return ConversionResult.NotHandled;
    }

    /// <summary>Determines whether a value represents a database or language null.</summary>
    /// <param name="value">The value to inspect.</param>
    /// <returns><see langword="true"/> when the value is null-like; otherwise, <see langword="false"/>.</returns>
    private static bool IsNullValue(object? value) => value is null || value == DBNull.Value;

    /// <summary>Determines whether a destination type accepts null.</summary>
    /// <param name="destinationType">The non-nullable destination type.</param>
    /// <param name="nullableType">The nullable destination type, when applicable.</param>
    /// <returns><see langword="true"/> when null is accepted; otherwise, <see langword="false"/>.</returns>
    private static bool CanAcceptNull(Type destinationType, Type? nullableType) =>
        nullableType is not null || !destinationType.IsValueType;

    /// <summary>Determines whether a value is already assignable to the destination type.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="destinationType">The destination type.</param>
    /// <returns><see langword="true"/> when the value is directly assignable; otherwise, <see langword="false"/>.
    /// </returns>
    private static bool IsDirectlyAssignable(object value, Type destinationType) =>
        destinationType == typeof(object) || destinationType.IsInstanceOfType(value);

    /// <summary>Determines whether text is empty for a nullable destination.</summary>
    /// <param name="text">The source text.</param>
    /// <param name="nullableType">The nullable destination type, when applicable.</param>
    /// <returns><see langword="true"/> when the nullable text is empty; otherwise, <see langword="false"/>.</returns>
    private static bool IsEmptyNullableText(string? text, Type? nullableType) =>
        nullableType is not null && string.IsNullOrWhiteSpace(text);

    /// <summary>Converts a string or numeric value to an enum.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="enumType">The enum type.</param>
    /// <param name="text">The source text, when applicable.</param>
    /// <returns>The enum value.</returns>
    private static object ConvertEnum(object value, Type enumType, string? text) =>
        text is not null
            ? Enum.Parse(enumType, text, ignoreCase: true)
            : Enum.ToObject(enumType, value);

    /// <summary>Attempts a conversion through the strategy registered for a well-known destination type.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="text">The source text, when applicable.</param>
    /// <param name="destinationType">The destination type.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <param name="result">The converted result.</param>
    /// <returns><see langword="true"/> when conversion succeeds; otherwise, <see langword="false"/>.</returns>
    private static bool TrySpecialConversion(
        object value,
        string? text,
        Type destinationType,
        CultureInfo culture,
        out object? result)
    {
        if (SpecialConverters.TryGetValue(destinationType, out var converter))
        {
            var converted = converter(value, text, culture);
            result = converted.Result;
            return converted.Success;
        }

        result = null;
        return false;
    }

    /// <summary>Attempts to parse a built-in numeric type.</summary>
    /// <param name="text">The numeric text.</param>
    /// <param name="destinationType">The requested numeric type.</param>
    /// <param name="culture">The parsing culture.</param>
    /// <param name="result">The parsed result.</param>
    /// <returns><see langword="true"/> when parsing is available; otherwise, <see langword="false"/>.</returns>
    private static bool TryConvertNumeric(
        string? text,
        Type destinationType,
        CultureInfo culture,
        out object? result)
    {
        if (
            text is null
            || !NumericParsers.TryGetValue(Type.GetTypeCode(destinationType), out var parser))
        {
            result = null;
            return false;
        }

        result = parser(text, culture);
        return true;
    }

    /// <summary>Attempts conversion through source and destination type converters.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="destinationType">The destination type.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <param name="result">The converted result.</param>
    /// <returns><see langword="true"/> when a type converter succeeds; otherwise, <see langword="false"/>.</returns>
    private static bool TryTypeConverters(
        object value,
        Type destinationType,
        CultureInfo culture,
        out object? result)
    {
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

        result = null;
        return false;
    }

    /// <summary>Attempts to create a GUID.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="text">The source text, when applicable.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The conversion status and result.</returns>
    private static ConversionAttempt TryConvertGuid(object value, string? text, CultureInfo culture)
    {
        _ = culture;
        if (value is byte[] bytes && bytes.Length == GuidByteCount)
        {
            return new(true, new Guid(bytes));
        }

        var success = Guid.TryParse(text ?? value.ToString(), out var guid);
        return new(success, success ? guid : null);
    }

    /// <summary>Attempts to create a URI.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="text">The source text, when applicable.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The conversion status and result.</returns>
    private static ConversionAttempt TryConvertUri(object value, string? text, CultureInfo culture)
    {
        _ = culture;
        var success = Uri.TryCreate(
            text ?? value.ToString(),
            UriKind.RelativeOrAbsolute,
            out var uri);
        return new(success, uri);
    }

    /// <summary>Attempts to create a time span.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="text">The source text, when applicable.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The conversion status and result.</returns>
    private static ConversionAttempt TryConvertTimeSpan(
        object value,
        string? text,
        CultureInfo culture)
    {
        var success = TimeSpan.TryParse(text ?? value.ToString(), culture, out var timeSpan);
        return new(success, success ? timeSpan : null);
    }

    /// <summary>Attempts to create a date and time with an offset.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="text">The source text, when applicable.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The conversion status and result.</returns>
    private static ConversionAttempt TryConvertDateTimeOffset(
        object value,
        string? text,
        CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            return new(true, new DateTimeOffset(dateTime));
        }

        var success = DateTimeOffset.TryParse(
            text ?? value.ToString(),
            culture,
            DateTimeStyles.AllowWhiteSpaces,
            out var offset);
        return new(success, success ? offset : null);
    }

    /// <summary>Attempts to create a date and time.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="text">The source text, when applicable.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The conversion status and result.</returns>
    private static ConversionAttempt TryConvertDateTime(
        object value,
        string? text,
        CultureInfo culture)
    {
        _ = text;
        _ = culture;
        var result = value is DateTimeOffset offset ? offset.DateTime : (DateTime?)null;
        return new(result.HasValue, result);
    }

    /// <summary>Attempts to resolve a runtime type.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="text">The source text, when applicable.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The conversion status and result.</returns>
    private static ConversionAttempt TryConvertType(object value, string? text, CultureInfo culture)
    {
        _ = value;
        _ = culture;
        var result = text is null
            ? null
            : Type.GetType(text, throwOnError: false, ignoreCase: true);
        return new(result is not null, result);
    }

    /// <summary>Attempts to parse a Boolean value, including yes/no and on/off aliases.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="text">The source text, when applicable.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The conversion status and result.</returns>
    private static ConversionAttempt TryConvertBoolean(
        object value,
        string? text,
        CultureInfo culture)
    {
        _ = value;
        _ = culture;
        if (bool.TryParse(text, out var boolean))
        {
            return new(true, boolean);
        }

        if (
            string.Equals(text, "yes", StringComparison.OrdinalIgnoreCase)
            || string.Equals(text, "on", StringComparison.OrdinalIgnoreCase))
        {
            return new(true, true);
        }

        return
            string.Equals(text, "no", StringComparison.OrdinalIgnoreCase)
            || string.Equals(text, "off", StringComparison.OrdinalIgnoreCase)
                ? new ConversionAttempt(true, false)
                : new ConversionAttempt(false, null);
    }

    /// <summary>Determines whether an exception represents a recoverable conversion failure.</summary>
    /// <param name="exception">The exception to inspect.</param>
    /// <returns><see langword="true"/> for a recoverable conversion failure; otherwise, <see langword="false"/>.
    /// </returns>
    private static bool IsConversionException(Exception exception) =>
        exception
            is ArgumentException
                or FormatException
                or InvalidCastException
                or NotSupportedException
                or OverflowException;

    /// <summary>Stores the outcome of a special conversion attempt.</summary>
    private sealed class ConversionAttempt
    {
        /// <summary>Initializes a new instance of the <see cref="ConversionAttempt"/> class.</summary>
        /// <param name="success">The conversion status.</param>
        /// <param name="result">The converted result.</param>
        public ConversionAttempt(bool success, object? result)
        {
            Success = success;
            Result = result;
        }

        /// <summary>Gets the converted result.</summary>
        public object? Result { get; }

        /// <summary>Gets a value indicating whether conversion succeeded.</summary>
        public bool Success { get; }
    }
}

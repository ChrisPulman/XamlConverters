// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Formats built-in numeric values and parses them back to a requested numeric type.</summary>
public sealed class NumberFormatConverter : IValueConverter
{
    /// <summary>The built-in numeric type codes supported by the converter.</summary>
    private static readonly HashSet<TypeCode> NumericTypeCodes =
    [
        TypeCode.Byte,
        TypeCode.SByte,
        TypeCode.UInt16,
        TypeCode.UInt32,
        TypeCode.UInt64,
        TypeCode.Int16,
        TypeCode.Int32,
        TypeCode.Int64,
        TypeCode.Decimal,
        TypeCode.Double,
        TypeCode.Single,];

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null || !IsNumericType(value.GetType()) || value is not IFormattable formattable)
        {
            return ConversionHelpers.DoNothing;
        }

        try
        {
            return formattable.ToString(parameter as string, culture);
        }
        catch (FormatException)
        {
            return ConversionHelpers.DoNothing;
        }
    }

    /// <inheritdoc/>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        IsNumericType(targetType) && ConversionHelpers.TryConvert(value, targetType, culture, out var result)
            ? result
            : ConversionHelpers.DoNothing;

    /// <summary>Determines whether a type is a supported numeric type.</summary>
    /// <param name="type">The type to inspect.</param>
    /// <returns><see langword="true"/> for a supported numeric type; otherwise, <see langword="false"/>.</returns>
    private static bool IsNumericType(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;
        return NumericTypeCodes.Contains(Type.GetTypeCode(type));
    }
}

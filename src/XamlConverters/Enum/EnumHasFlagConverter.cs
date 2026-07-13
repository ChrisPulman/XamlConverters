// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Determines whether a flags enum contains the flag supplied as the converter parameter.</summary>
public sealed class EnumHasFlagConverter : IValueConverter
{
    /// <summary>Tests an enum value for the requested flag or composite flags value.</summary>
    /// <param name="value">The source enum value.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">The flag value or its member name.</param>
    /// <param name="culture">The culture used for parameter coercion.</param>
    /// <returns><see langword="true"/> when all requested bits are present.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (
            value is not Enum enumValue
            || !BclConversion.TryChangeType(
                parameter,
                enumValue.GetType(),
                culture,
                out var convertedFlag)
            || convertedFlag is not Enum flag)
        {
            return false;
        }

        var valueBits = ToUInt64(enumValue);
        var flagBits = ToUInt64(flag);
        return (valueBits & flagBits) == flagBits;
    }

    /// <summary>Reverse conversion is unsupported because removing a flag requires the current source value.</summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns><see cref="Binding.DoNothing"/>.</returns>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => Binding.DoNothing;

    /// <summary>Converts an enum value to its unsigned bit representation.</summary>
    /// <param name="value">The enum value.</param>
    /// <returns>The unsigned bit representation.</returns>
    private static ulong ToUInt64(Enum value)
    {
        try
        {
            return System.Convert.ToUInt64(value, CultureInfo.InvariantCulture);
        }
        catch (OverflowException)
        {
            return unchecked((ulong)System.Convert.ToInt64(value, CultureInfo.InvariantCulture));
        }
    }
}

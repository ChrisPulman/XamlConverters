// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Determines whether a flags enum contains a requested flag.</summary>
public sealed class EnumHasFlagConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (
            value is not Enum enumValue
            || !ConversionHelpers.TryConvert(
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

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.DoNothing;

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

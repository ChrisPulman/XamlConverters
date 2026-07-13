// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Multiplies by the parameter on conversion and divides by it on conversion back.</summary>
public sealed class MultiplierConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => Apply(value, parameter, targetType, culture, divide: false);

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Apply(value, parameter, targetType, culture, divide: true);

    /// <summary>Multiplies or divides a numeric value by the converter parameter.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="parameter">The multiplier or divisor.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <param name="divide">Whether to divide instead of multiply.</param>
    /// <returns>The calculated value or an Avalonia binding sentinel.</returns>
    private static object Apply(object? value, object? parameter, Type targetType, CultureInfo culture, bool divide)
    {
        if (!ConversionHelpers.TryDecimal(value, culture, out var number) ||
            !ConversionHelpers.TryDecimal(parameter, culture, out var multiplier) || (divide && multiplier == 0))
        {
            return ConversionHelpers.UnsetValue;
        }

        return ConversionHelpers.ConvertDecimal(divide ? number / multiplier : number * multiplier, targetType, culture);
    }
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
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

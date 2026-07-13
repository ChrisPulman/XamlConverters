// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Multiplies a numeric value by a factor or percentage parameter.</summary>
public sealed class PercentageConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!ConversionHelpers.TryDecimal(value, culture, out var number))
        {
            return ConversionHelpers.UnsetValue;
        }

        var text = parameter?.ToString()?.Trim() ?? "100%";
        var percentage = text.EndsWith("%", StringComparison.Ordinal);
        if (percentage)
        {
            text = text.TrimEnd('%');
        }

        if (!decimal.TryParse(text, NumberStyles.Any, culture, out var factor))
        {
            return ConversionHelpers.UnsetValue;
        }

        return ConversionHelpers.ConvertDecimal(number * (percentage ? factor / 100m : factor), targetType, culture);
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

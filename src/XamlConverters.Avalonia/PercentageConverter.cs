// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Multiplies a numeric value by a factor or percentage parameter.</summary>
public sealed class PercentageConverter : IValueConverter
{
    /// <summary>The divisor used to convert a percentage to a factor.</summary>
    private const decimal PercentageDivisor = 100M;

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

        return ConversionHelpers.ConvertDecimal(
            number * (percentage ? factor / PercentageDivisor : factor),
            targetType,
            culture);
    }

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.DoNothing;
}

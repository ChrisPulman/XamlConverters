// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Clamps a numeric value to a min/max parameter such as 0,100.</summary>
public sealed class ClampConverter : IValueConverter
{
    /// <summary>The number of bounds required in a clamp parameter.</summary>
    private const int RequiredBoundCount = 2;

    /// <summary>Gets or sets a value used by the converter.</summary>
    public decimal Minimum { get; set; } = decimal.MinValue;

    /// <summary>Gets or sets a value used by the converter.</summary>
    public decimal Maximum { get; set; } = decimal.MaxValue;

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!ConversionHelpers.TryDecimal(value, culture, out var number))
        {
            return ConversionHelpers.UnsetValue;
        }

        var minimum = Minimum;
        var maximum = Maximum;
        var parts = parameter?.ToString()?.Split(',', ';');
        if (parts is { Length: >= RequiredBoundCount })
        {
            _ = decimal.TryParse(parts[0], NumberStyles.Any, culture, out minimum);
            _ = decimal.TryParse(parts[1], NumberStyles.Any, culture, out maximum);
        }

        return ConversionHelpers.ConvertDecimal(
            Math.Clamp(number, Math.Min(minimum, maximum), Math.Max(minimum, maximum)),
            targetType,
            culture);
    }

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => Convert(value, targetType, parameter, culture);
}

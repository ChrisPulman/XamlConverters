// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Clamps a numeric value to a min/max parameter such as 0,100.</summary>
public sealed class ClampConverter : IValueConverter
{
    /// <summary>
    /// Gets or sets a value used by the converter.
    /// </summary>
    public decimal Minimum { get; set; } = decimal.MinValue;

    /// <summary>
    /// Gets or sets a value used by the converter.
    /// </summary>
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
        if (parts is { Length: >= 2 })
        {
            decimal.TryParse(parts[0], NumberStyles.Any, culture, out minimum);
            decimal.TryParse(parts[1], NumberStyles.Any, culture, out maximum);
        }

        return ConversionHelpers.ConvertDecimal(Math.Clamp(number, Math.Min(minimum, maximum), Math.Max(minimum, maximum)), targetType, culture);
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Convert(value, targetType, parameter, culture);
}

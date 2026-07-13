// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Selects green, yellow, or red according to low-high threshold parameters.</summary>
public sealed class LineStrokeLevelConverter : IValueConverter
{
    /// <summary>The number of thresholds required in the converter parameter.</summary>
    private const int RequiredThresholdCount = 2;

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!ConversionHelpers.TryDecimal(value, culture, out var number))
        {
            return Brushes.Red;
        }

        var parts = parameter?.ToString()?.Split('-');
        if (parts is not { Length: >= RequiredThresholdCount } || !decimal.TryParse(parts[0], NumberStyles.Any, culture, out var low) ||
            !decimal.TryParse(parts[1], NumberStyles.Any, culture, out var high))
        {
            return Brushes.Red;
        }

        if (number >= high)
        {
            return Brushes.Red;
        }

        return number >= low ? Brushes.Yellow : Brushes.Lime;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

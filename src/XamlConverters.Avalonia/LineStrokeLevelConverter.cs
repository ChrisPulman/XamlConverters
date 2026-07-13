// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Selects green, yellow, or red according to low-high threshold parameters.</summary>
public sealed class LineStrokeLevelConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!ConversionHelpers.TryDecimal(value, culture, out var number))
        {
            return Brushes.Red;
        }

        var parts = parameter?.ToString()?.Split('-');
        if (parts is not { Length: >= 2 } || !decimal.TryParse(parts[0], NumberStyles.Any, culture, out var low) ||
            !decimal.TryParse(parts[1], NumberStyles.Any, culture, out var high))
        {
            return Brushes.Red;
        }

        return number >= high ? Brushes.Red : number >= low ? Brushes.Yellow : Brushes.Lime;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

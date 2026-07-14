// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Returns the original brush/color or a configurable red fallback brush.</summary>
public sealed class FallbackBrushConverter : IValueConverter
{
    /// <summary>Gets or sets a value used by the converter.</summary>
    public IBrush Fallback { get; set; } = Brushes.Red;

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            IBrush brush => brush,
            Color color => new SolidColorBrush(color),
            _ when ColorHelpers.TryColor(parameter, out var parsed) => new SolidColorBrush(parsed),
            _ => Fallback,
        };

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.DoNothing;
}

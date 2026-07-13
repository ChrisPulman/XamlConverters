// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Selects a conventional brush according to a numeric or Boolean state.</summary>
public sealed class ValueToBrushConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var active = value is bool boolean ? !boolean : ConversionHelpers.TryDecimal(value, culture, out var number) && number > 0;
        var mode = parameter?.ToString() ?? string.Empty;
        if (mode.Contains("BackPressure", StringComparison.OrdinalIgnoreCase))
        {
            return active ? Brushes.DarkBlue : Brushes.LightYellow;
        }

        if (mode.Contains("Pressure", StringComparison.OrdinalIgnoreCase))
        {
            return active ? Brushes.Black : Brushes.LightYellow;
        }

        return active ? Brushes.DarkGreen : Brushes.LightYellow;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

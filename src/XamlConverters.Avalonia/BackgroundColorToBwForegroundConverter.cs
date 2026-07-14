// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Selects black or white foreground for contrast with a background color.</summary>
public sealed class BackgroundColorToBwForegroundConverter : IValueConverter
{
    /// <summary>The red-channel luminance coefficient.</summary>
    private const double RedLuminanceWeight = 0.2126D;

    /// <summary>The green-channel luminance coefficient.</summary>
    private const double GreenLuminanceWeight = 0.7152D;

    /// <summary>The blue-channel luminance coefficient.</summary>
    private const double BlueLuminanceWeight = 0.0722D;

    /// <summary>The maximum value of a color channel.</summary>
    private const double MaximumChannelValue = 255D;

    /// <summary>The normalized luminance threshold between white and black foregrounds.</summary>
    private const double ContrastThreshold = 0.5D;

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!ColorHelpers.TryColor(value ?? parameter, out var color))
        {
            return Brushes.Black;
        }

        var luminance =
            (
                (RedLuminanceWeight * color.R)
                + (GreenLuminanceWeight * color.G)
                + (BlueLuminanceWeight * color.B)) / MaximumChannelValue;
        return luminance <= ContrastThreshold ? Brushes.White : Brushes.Black;
    }

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.DoNothing;
}

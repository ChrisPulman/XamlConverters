// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CP.Xaml.Converters;

/// <summary>Chooses a black or white foreground brush for a hexadecimal background color.</summary>
public class BackgroundColorToBwForegroundConverter : IValueConverter
{
    /// <summary>The minimum number of hexadecimal digits in an RGB value.</summary>
    private const int MinimumRgbLength = 6;

    /// <summary>The number of color channels averaged for grayscale.</summary>
    private const uint ColorChannelCount = 3;

    /// <summary>The grayscale threshold between white and black foregrounds.</summary>
    private const uint ContrastThreshold = 127;

    /// <summary>Converts a hexadecimal background color to a contrasting foreground brush.</summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The fallback background value.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A black or white foreground brush.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        value ??= parameter;

        if (value is null || value.ToString()?.Length < MinimumRgbLength)
        {
            return new SolidColorBrush(Colors.Black);
        }

        var text = value.ToString()!;
        if (text[0] == '#')
        {
            text = text.Remove(0, 1);
        }

        var colorValue = uint.Parse(text, NumberStyles.HexNumber);
        var grayScale = (((colorValue & 0xff0000) >> 16) + ((colorValue & 0x00ff00) >> 8) + (colorValue & 0x0000ff)) / ColorChannelCount;
        return grayScale <= ContrastThreshold ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
    }

    /// <summary>Returns the WPF do-nothing binding sentinel because reverse conversion is unsupported.</summary>
    /// <param name="value">The value produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>The WPF do-nothing binding sentinel.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}

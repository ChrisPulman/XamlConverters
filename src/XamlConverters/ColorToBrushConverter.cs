// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CP.Xaml.Converters;

/// <summary>Color To Brush Converter.</summary>
public class ColorToBrushConverter : IValueConverter
{
    /// <summary>The number of hexadecimal characters in one color component.</summary>
    private const int ComponentLength = 2;

    /// <summary>The starting index of the red component.</summary>
    private const int RedComponentIndex = 1;

    /// <summary>The starting index of the green component.</summary>
    private const int GreenComponentIndex = 3;

    /// <summary>The starting index of the blue component.</summary>
    private const int BlueComponentIndex = 5;

    /// <summary>The radix used to parse hexadecimal components.</summary>
    private const int HexadecimalRadix = 16;

    /// <summary>Converts a value.</summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return new SolidColorBrush(Colors.Transparent);
        }

        var color = value.ToString();
        var r = System.Convert.ToByte(color?.Substring(RedComponentIndex, ComponentLength), HexadecimalRadix);
        var g = System.Convert.ToByte(color?.Substring(GreenComponentIndex, ComponentLength), HexadecimalRadix);
        var b = System.Convert.ToByte(color?.Substring(BlueComponentIndex, ComponentLength), HexadecimalRadix);
        return new SolidColorBrush(Color.FromArgb(byte.MaxValue, r, g, b));
    }

    /// <summary>Converts a value.</summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is null ? Colors.Transparent : ((SolidColorBrush)value).Color;
    }
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CP.Xaml.Converters;

/// <summary>
/// Color To Brush Converter.
/// </summary>
public class ColorToBrushConverter : IValueConverter
{
    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return new SolidColorBrush(Colors.Transparent);
        }

        var color = value.ToString();
        var r = System.Convert.ToByte(color?.Substring(1, 2), 16);
        var g = System.Convert.ToByte(color?.Substring(3, 2), 16);
        var b = System.Convert.ToByte(color?.Substring(5, 2), 16);
        return new SolidColorBrush(Color.FromArgb(255, r, g, b));
    }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return Colors.Transparent;
        }

        return ((SolidColorBrush)value).Color;
    }
}

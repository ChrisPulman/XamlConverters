// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CP.Xaml.Converters;

/// <summary>
/// Background Color To BW Foreground Converter.
/// </summary>
public class BackgroundColorToBwForegroundConverter : IValueConverter
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
        value ??= parameter;

        if (value == null || (value?.ToString()?.Length < 6))
        {
            return new SolidColorBrush(Colors.Black);
        }

        var strValue = value?.ToString();
        if (strValue![0] == '#')
        {
            strValue = strValue.Substring(1);
        }

        var colorValue = uint.Parse(strValue, NumberStyles.HexNumber);
        var grayScale = (((colorValue & 0xff0000) >> 16) + ((colorValue & 0x00ff00) >> 8) + (colorValue & 0x0000ff)) / 3;
        return grayScale <= 127 ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
    }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
}

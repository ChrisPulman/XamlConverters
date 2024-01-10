// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace CP.Xaml.Converters;

/// <summary>
/// Value converter for converting hexadecimal strings to a SolidColorBrush.
/// </summary>
public class HexStringToColorConverter : IValueConverter
{
    /// <summary>
    /// The hexadecimal digits.
    /// </summary>
    private static readonly char[] _hexDigits =
    [
        '0',
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7',
        '8',
        '9',
        'A',
        'B',
        'C',
        'D',
        'E',
        'F'
    ];

    /// <summary>
    /// Appends the hexadecimal.
    /// </summary>
    /// <param name="colorValue">The color value.</param>
    /// <param name="builder">The builder.</param>
    public static void AppendHex(byte colorValue, StringBuilder builder) =>
        builder?.Append(_hexDigits[colorValue >> 4]).Append(_hexDigits[colorValue & 0xF]);

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        value ??= parameter;

        if (value == null || (value?.ToString()!.Length < 6))
        {
            return null;
        }

        var strValue = value?.ToString();
        if (strValue![0] == '#')
        {
            strValue = strValue.Substring(1);
        }

        var colorValue = uint.Parse(strValue, NumberStyles.HexNumber);
        return Color.FromArgb(0xff, (byte)((colorValue & 0xff0000) >> 16), (byte)((colorValue & 0x00ff00) >> 8), (byte)(colorValue & 0x0000ff));
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
        var color = value as Color?;
        StringBuilder builder = new();

        AppendHex(color?.R ?? 0, builder);
        AppendHex(color?.G ?? 0, builder);
        AppendHex(color?.B ?? 0, builder);
        return builder.ToString();
    }
}

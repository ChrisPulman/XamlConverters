// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Multiplier Converter.
/// </summary>
/// <seealso cref="IValueConverter"/>
public class MultiplierConverter : IValueConverter
{
    /// <summary>
    /// Converts the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Multiplies by the parameter.</returns>
    /// <exception cref="Exception">
    /// The converter needs a parameter of a double or The converter needs a value of a int,
    /// float or double.
    /// </exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var param = parameter?.ToString();
        if (string.IsNullOrWhiteSpace(param))
        {
            throw new Exception("The converter needs a parameter of a double");
        }

        var val = value switch
        {
            short x => x,
            int x2 => x2,
            float x3 => x3,
            double x4 => x4,
            _ => throw new Exception("The converter needs a value of a int, float or double")
        };

        var multipler = double.Parse(param);
        return val * multipler;
    }

    /// <summary>
    /// Converts the back.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Divides the value by the parameter.</returns>
    /// <exception cref="Exception">
    /// The converter needs a parameter of a double or The converter needs a value of a int,
    /// float or double.
    /// </exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var param = parameter?.ToString();
        if (string.IsNullOrWhiteSpace(param))
        {
            throw new Exception("The converter needs a parameter of a double");
        }

        var val = value switch
        {
            short x => x,
            int x2 => x2,
            float x3 => (double)x3,
            double x4 => x4,
            _ => throw new Exception("The converter needs a value of a int, float or double"),
        };
        var divisor = double.Parse(param);
        return val / divisor;
    }
}

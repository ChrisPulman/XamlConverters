// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Multiplier Converter.</summary>
/// <seealso cref="IValueConverter"/>
public class MultiplierConverter : IValueConverter
{
    /// <summary>Converts the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Multiplies by the parameter.</returns>
    /// <exception cref="ArgumentException">
    /// The converter needs a parameter of a double or The converter needs a value of a int,
    /// float or double.
    /// </exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var param = parameter?.ToString() ?? throw new ArgumentNullException(nameof(parameter));

        if (param.Trim().Length == 0)
        {
            throw new ArgumentException("The converter needs a double parameter.", nameof(parameter));
        }

        var val = value switch
        {
            short x => x,
            int x2 => x2,
            float x3 => x3,
            double x4 => x4,
            _ => throw new InvalidCastException("The converter needs a short, integer, float, or double value."),
        };

        var multipler = double.Parse(param);
        return val * multipler;
    }

    /// <summary>Converts the back.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Divides the value by the parameter.</returns>
    /// <exception cref="ArgumentException">
    /// The converter needs a parameter of a double or The converter needs a value of a int,
    /// float or double.
    /// </exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var param = parameter?.ToString() ?? throw new ArgumentNullException(nameof(parameter));

        if (param.Trim().Length == 0)
        {
            throw new ArgumentException("The converter needs a double parameter.", nameof(parameter));
        }

        var val = value switch
        {
            short x => x,
            int x2 => x2,
            float x3 => (double)x3,
            double x4 => x4,
            _ => throw new InvalidCastException("The converter needs a short, integer, float, or double value."),
        };
        var divisor = double.Parse(param);
        return val / divisor;
    }
}

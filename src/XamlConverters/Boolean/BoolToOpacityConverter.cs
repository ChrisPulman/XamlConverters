// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Bool To Opacity Converter.
/// </summary>
/// <seealso cref="IValueConverter"/>
public class BoolToOpacityConverter : IValueConverter
{
    /// <summary>
    /// Converts the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="Exception">The paramater needs to be between 0 and 1 for the opacity.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool val)
        {
            if (parameter is string parm)
            {
                var newVal = double.Parse(parm);
                return newVal switch
                {
                    <= 1 => val ? 1 : newVal,
                    _ => throw new Exception("The paramater needs to be between 0 and 1 for the opacity")
                };
            }

            throw new Exception("A paramater need to be enetered for the opacity");
        }

        throw new Exception("The value bound is not of type Boolean");
    }

    /// <summary>
    /// Converts the back.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="Exception">A paramater need to be enetered for the opacity.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => parameter switch
    {
        string => value switch
        {
            double vald => vald == 1d,
            float valf => valf == 1f,
            int vali => (object)(vali == 1),
            _ => throw new Exception("The value bound to is not of type double, float or integer"),
        },
        _ => throw new Exception("A paramater need to be enetered for the opacity")
    };
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Invert Sign Converter.
/// </summary>
/// <seealso cref="IValueConverter"/>
public class InvertSignConverter : IValueConverter
{
    /// <summary>
    /// Converts the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>inverted value.</returns>
    /// <exception cref="Exception">
    /// The value bounded is not of a integer, float or double.
    /// </exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || (value is string && !(value is short || value is int || value is float || value is double || value is decimal)))
        {
            throw new Exception("The value bound is not typeof a short, integer, float, double or decimal.");
        }

        var val = (decimal)value * -1;
        if (targetType == null)
        {
            throw new Exception("The target value is not typeof a short, integer, float, double or decimal.");
        }
        else if (targetType == typeof(short))
        {
            return (short)val;
        }
        else if (targetType == typeof(int))
        {
            return (int)val;
        }
        else if (targetType == typeof(float))
        {
            return (float)val;
        }
        else if (targetType == typeof(double))
        {
            return (double)val;
        }
        else if (targetType == typeof(decimal))
        {
            return val;
        }
        else if (targetType == typeof(string))
        {
            return val.ToString();
        }
        else
        {
            throw new Exception("The target value is not typeof a short, integer, float, double or decimal.");
        }
    }

    /// <summary>
    /// Converts the back.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>original value.</returns>
    /// <exception cref="Exception">The value bounded is not of a integer, float or double.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || (value is string && !(value is short || value is int || value is float || value is double || value is decimal)))
        {
            throw new Exception("The value bound is not typeof a short, integer, float, double or decimal.");
        }

        var val = (decimal)value * -1;
        if (targetType == typeof(short))
        {
            return (short)val;
        }
        else if (targetType == typeof(int))
        {
            return (int)val;
        }
        else if (targetType == typeof(float))
        {
            return (float)val;
        }
        else if (targetType == typeof(double))
        {
            return (double)val;
        }
        else if (targetType == typeof(decimal))
        {
            return val;
        }
        else if (targetType == typeof(string))
        {
            return val.ToString();
        }
        else
        {
            throw new Exception("The target value is not typeof a short, integer, float, double or decimal.");
        }
    }
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Invert Boolean from one value to another.
/// </summary>
public class InvertValueConverter : IValueConverter
{
    /// <summary>
    /// Converts a boolean to the inverse value.
    /// </summary>
    /// <param name="value">takes the binding value.</param>
    /// <param name="targetType">Boolean value.</param>
    /// <param name="parameter">The parameter is not used.</param>
    /// <param name="culture">The parameter is not used.</param>
    /// <returns>Inverted Boolean.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch
    {
        null => throw new Exception("The target value is NULL and can not be typeof a short, integer, float, double or decimal."),
        _ => targetType?.FullName switch
        {
            "System.Double" => (double)value * -1,
            "System.Single" => (float)value * -1,
            "System.Int64" => (long)value * -1,
            "System.Int32" => (int)value * -1,
            "System.Int16" => (short)value * -1,
            _ => value,
        }
    };

    /// <summary>
    /// Not enabled.
    /// </summary>
    /// <param name="value">The parameter is not used.</param>
    /// <param name="targetType">The parameter is not used.</param>
    /// <param name="parameter">The parameter is not used.</param>
    /// <param name="culture">The parameter is not used.</param>
    /// <returns>The parameter is not used.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value switch
    {
        null => throw new Exception("The target value is NULL and can not be typeof a short, integer, float, double or decimal."),
        _ => targetType?.FullName switch
        {
            "System.Double" => (double)value * -1,
            "System.Single" => (float)value * -1,
            "System.Int64" => (long)value * -1,
            "System.Int32" => (int)value * -1,
            "System.Int16" => (short)value * -1,
            _ => value,
        }
    };
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Value To Boolean Converter.
/// </summary>
public class InverseValueToBoolConverter : IValueConverter
{
    /// <summary>
    /// Converts the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Converts the value to a boolean - true if below zero.</returns>
    /// <exception cref="Exception">An Exception.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch
    {
        int => (int)value <= 0,
        float => (float)value <= 0,
        double => (double)value <= 0,
        _ => throw new Exception("The value bounded is not of type int, float or double")
    };

    /// <summary>
    /// Converts the back.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Convert Back.</returns>
    /// <exception cref="Exception">An Exception.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is bool x ? (object)(x ? 0 : 1) : throw new Exception("The bounded type is not of type boolean");
}

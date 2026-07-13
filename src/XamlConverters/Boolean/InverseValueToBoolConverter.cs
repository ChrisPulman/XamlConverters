// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Value To Boolean Converter.</summary>
public class InverseValueToBoolConverter : IValueConverter
{
    /// <summary>Converts the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Converts the value to a boolean - true if below zero.</returns>
    /// <exception cref="InvalidCastException">The bound value is not numeric.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch
    {
        int => (int)value <= 0,
        float => (float)value <= 0,
        double => (double)value <= 0,
        _ => throw new InvalidCastException("The bound value is not an integer, float, or double."),
    };

    /// <summary>Converts the back.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Convert Back.</returns>
    /// <exception cref="InvalidCastException">The bound value is not Boolean.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is bool x ? (object)(x ? 0 : 1) : throw new InvalidCastException("The bound value is not Boolean.");
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Boolean To String Tick Cross Converter.
/// </summary>
public class BoolToStringTickCrossConverter : IValueConverter
{
    /// <summary>
    /// Converts the specified values.
    /// </summary>
    /// <param name="value">The values.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Boolean to string Tick or Cross.</returns>
    /// <exception cref="Exception">An Exception.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is bool x ? (object)(x ? "P" : "O") : throw new Exception("The binding value type is not a type of bool");

    /// <summary>
    /// Converts the back.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Convert Back.</returns>
    /// <exception cref="Exception">The bounded value is not of type string.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is string ? (object)(value.ToString() == "P") : throw new Exception("The bounded value is not of type string");
}

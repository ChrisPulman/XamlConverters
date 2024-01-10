// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Boolean to Visibility Converter.
/// </summary>
public class BoolToVisibilityConverterNegate : IValueConverter
{
    /// <summary>
    /// Enable the visibility of the UI element in base of a boolean value (true---&gt;Collapsed//false--&gt;Visible).
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="Exception">The bound value is not of type Boolean.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool val)
        {
            var parm = parameter?.ToString()?.ToLower();
            return parm != null
                ? val ? parm == "hidden" ? Visibility.Hidden : Visibility.Collapsed : Visibility.Visible
                : (object)(val ? Visibility.Collapsed : Visibility.Visible);
        }

        throw new Exception("The bound value is not of type Boolean");
    }

    /// <summary>
    /// Converts the back.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="Exception">The bound value back is not of type Visibility.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value switch
    {
        Visibility val => (object)(val != Visibility.Visible),
        _ => throw new Exception("The bound value back is not of type Visibility"),
    };
}

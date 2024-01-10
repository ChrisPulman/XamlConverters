// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Integer To Visibility Converter.
/// </summary>
/// <seealso cref="System.Windows.Data.IValueConverter"/>
public class IntToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Enable the visibility of the UI element in base of a boolean value (true---&gt;visible//false--&gt;Collapsed).
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="Exception">An Exception.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int val)
        {
            var parm = parameter?.ToString()?.ToLower();
            return parm != null
                ? val > 0 ? Visibility.Visible : parm == "hidden" ? Visibility.Hidden : Visibility.Collapsed
                : (object)(val > 0 ? Visibility.Visible : Visibility.Collapsed);
        }

        throw new Exception("The bound value is not of type Integer");
    }

    /// <summary>
    /// Converts the back.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="Exception">An Exception.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is Visibility val
            ? val == Visibility.Visible ? 1 : 0
            : throw new Exception("The bound value back is not of type Visibility");
}

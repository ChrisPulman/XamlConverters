// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Value Compare Visibility Converter.</summary>
public class ValueCompareVisibilityConverter : IValueConverter
{
    /// <summary>Converts the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="InvalidCastException">The bound value is not numeric.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch
    {
        short vals => vals <= 0 ? Visibility.Hidden : Visibility.Visible,
        int val => val <= 0 ? Visibility.Hidden : Visibility.Visible,
        float valf => valf <= 0 ? Visibility.Hidden : Visibility.Visible,
        double vald => vald <= 0 ? Visibility.Hidden : Visibility.Visible,
        _ => throw new InvalidCastException("The bound value is not numeric."),
    };

    /// <summary>Converts the back.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="InvalidCastException">The bound value is not a visibility value.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value switch
    {
        Visibility val => val == Visibility.Hidden ? 0 : 1,
        _ => throw new InvalidCastException("The bound value is not a visibility value."),
    };
}

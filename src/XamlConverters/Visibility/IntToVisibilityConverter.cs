// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Integer To Visibility Converter.</summary>
/// <seealso cref="System.Windows.Data.IValueConverter"/>
public class IntToVisibilityConverter : IValueConverter
{
    /// <summary>Converts a positive integer to visible and other values to collapsed.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="InvalidCastException">The bound value is not an integer.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not int integerValue)
        {
            throw new InvalidCastException("The bound value is not an integer.");
        }

        if (integerValue > 0)
        {
            return Visibility.Visible;
        }

        return string.Equals(parameter?.ToString(), "hidden", StringComparison.OrdinalIgnoreCase)
            ? Visibility.Hidden
            : Visibility.Collapsed;
    }

    /// <summary>Converts the back.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="InvalidCastException">The bound value is not a visibility value.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Visibility visibility)
        {
            throw new InvalidCastException("The bound value is not a visibility value.");
        }

        return visibility == Visibility.Visible ? 1 : 0;
    }
}

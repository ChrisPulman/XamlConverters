// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Boolean to Visibility Converter.</summary>
public class BoolToVisibilityConverterNegate : IValueConverter
{
    /// <summary>Converts true to collapsed and false to visible.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="InvalidCastException">The bound value is not of type Boolean.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool val)
        {
            throw new InvalidCastException("The bound value is not of type Boolean");
        }

        var parameterText = parameter?.ToString();
        if (parameterText is null)
        {
            return val ? Visibility.Collapsed : Visibility.Visible;
        }

        if (!val)
        {
            return Visibility.Visible;
        }

        return string.Equals(parameterText, "hidden", StringComparison.OrdinalIgnoreCase)
            ? Visibility.Hidden
            : Visibility.Collapsed;
    }

    /// <summary>Converts the back.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="InvalidCastException">The bound value back is not of type Visibility.</exception>
    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture) =>
        value switch
        {
            Visibility val => (object)(val != Visibility.Visible),
            _ => throw new InvalidCastException("The bound value back is not of type Visibility"),
        };
}

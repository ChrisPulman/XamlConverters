// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Converts double to Currency string.</summary>
public class DoubleToCurrencyStringConverter : IValueConverter
{
    /// <summary>Converts the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">The type.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A string formatted as a Currency.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch
    {
        double amount => $"{amount:C}",
        decimal amountDec => $"{amountDec:C}",
        _ => $"{0:C}",
    };

    /// <summary>Converts Currency string to double.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">The type.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A value represented as a double.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
        {
            return doubleValue;
        }

        return value is decimal decimalValue ? decimalValue : new object();
    }
}

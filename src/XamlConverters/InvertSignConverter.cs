// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Invert Sign Converter.</summary>
/// <seealso cref="IValueConverter"/>
public class InvertSignConverter : IValueConverter
{
    /// <summary>The message used when a value is not a supported numeric type.</summary>
    private const string InvalidValueMessage = "The bound value is not a short, integer, float, double, or decimal.";

    /// <summary>The message used when a target is not a supported numeric type.</summary>
    private const string InvalidTargetMessage = "The target type is not short, integer, float, double, decimal, or string.";

    /// <summary>Converts the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>inverted value.</returns>
    /// <exception cref="InvalidCastException">
    /// The value bounded is not of a integer, float or double.
    /// </exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Invert(value, targetType, culture);

    /// <summary>Converts the back.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>original value.</returns>
    /// <exception cref="InvalidCastException">The value or target type is unsupported.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Invert(value, targetType, culture);

    /// <summary>Inverts a supported numeric value and converts it to the requested target type.</summary>
    /// <param name="value">The value to invert.</param>
    /// <param name="targetType">The requested result type.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The inverted value.</returns>
    private static object Invert(object value, Type targetType, CultureInfo culture)
    {
        if (value is not short and not int and not float and not double and not decimal)
        {
            throw new InvalidCastException(InvalidValueMessage);
        }

        var inverted = -System.Convert.ToDecimal(value, culture);
        if (targetType == typeof(string))
        {
            return inverted.ToString(culture);
        }

        try
        {
            return System.Convert.ChangeType(inverted, targetType, culture);
        }
        catch (InvalidCastException exception)
        {
            throw new InvalidCastException(InvalidTargetMessage, exception);
        }
    }
}

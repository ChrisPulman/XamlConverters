// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Bool To Opacity Converter.</summary>
/// <seealso cref="IValueConverter"/>
public class BoolToOpacityConverter : IValueConverter
{
    /// <summary>The opacity representing a fully visible element.</summary>
    private const double FullyOpaque = 1d;

    /// <summary>The integral opacity representing a fully visible element.</summary>
    private const int FullyOpaqueInteger = 1;

    /// <summary>The tolerance used when comparing double opacity values.</summary>
    private const double DoubleTolerance = 1e-9;

    /// <summary>The tolerance used when comparing single-precision opacity values.</summary>
    private const float FloatTolerance = 1e-6f;

    /// <summary>Converts the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="ArgumentException">The parameter is missing or is not a valid opacity.</exception>
    /// <exception cref="InvalidCastException">The bound value is not Boolean.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool condition)
        {
            throw new InvalidCastException("The value bound is not Boolean.");
        }

        if (parameter is not string parameterText)
        {
            throw new ArgumentException("An opacity parameter is required.", nameof(parameter));
        }

        var opacity = double.Parse(parameterText, culture);
        if (opacity > FullyOpaque)
        {
            throw new ArgumentOutOfRangeException(nameof(parameter), "The opacity parameter must not exceed 1.");
        }

        return condition ? FullyOpaque : opacity;
    }

    /// <summary>Converts the back.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    /// <exception cref="ArgumentException">The parameter is missing.</exception>
    /// <exception cref="InvalidCastException">The bound value is not numeric.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not string)
        {
            throw new ArgumentException("An opacity parameter is required.", nameof(parameter));
        }

        return value switch
        {
            double doubleValue => Math.Abs(doubleValue - FullyOpaque) <= DoubleTolerance,
            float floatValue => Math.Abs(floatValue - FullyOpaque) <= FloatTolerance,
            int integerValue => (object)(integerValue == FullyOpaqueInteger),
            _ => throw new InvalidCastException("The bound value is not a double, float, or integer."),
        };
    }
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Allows a simple mathematics operator to be executed.
/// </summary>
public class ArithmeticConverter : IValueConverter
{
    /// <summary>
    /// REGEX arithmetic expression.
    /// </summary>
    private const string ArithmeticParseExpression = "([+\\-*/]{1,1})\\s{0,}(\\-?[\\d\\.]+)";

    /// <summary>
    /// REGEX arithmetic.
    /// </summary>
    private readonly Regex _arithmeticRegex = new(ArithmeticParseExpression);

    /// <summary>
    /// Carries out arithmetic on the value based on the parameter.
    /// </summary>
    /// <param name="value">The value used as base.</param>
    /// <param name="targetType">Integer and Double.</param>
    /// <param name="parameter">A math operator (+,-,*,/) plus a value.</param>
    /// <param name="culture">Not used.</param>
    /// <returns>Integer or Double based on Math.</returns>
    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((value is double || value is int) && parameter != null)
        {
            var param = parameter?.ToString();
            if (param?.Length > 0)
            {
                var match = _arithmeticRegex.Match(param);
                if (match.Groups.Count == 3)
                {
                    var operation = match.Groups[1].Value.Trim();
                    var numericValue = match.Groups[2].Value;
                    if (value is double x2)
                    {
                        // this should always succeed or our REGEX is broken
                        if (double.TryParse(numericValue, out var number))
                        {
                            var valueAsDouble = x2;
                            return operation switch
                            {
                                "+" => valueAsDouble + number,
                                "-" => valueAsDouble - number,
                                "*" => valueAsDouble * number,
                                "/" => valueAsDouble / number,
                                _ => 0.0,
                            };
                        }
                    }

                    if (value is int x)
                    {
                        // this should always succeed or our REGEX is broken
                        if (int.TryParse(numericValue, out var number))
                        {
                            var valueAsDouble = x;
                            return operation switch
                            {
                                "+" => valueAsDouble + number,
                                "-" => valueAsDouble - number,
                                "*" => valueAsDouble * number,
                                "/" => valueAsDouble / number,
                                _ => 0.0,
                            };
                        }
                    }
                }
            }
        }

        return new InvalidCastException("Binding must be of Type int OR double and Parameter must contain a math operator (+,-,*,/) plus a value");
    }

    /// <summary>
    /// Convert Back Not used.
    /// </summary>
    /// <param name="value">The parameter is not used.</param>
    /// <param name="targetType">The parameter is not used.</param>
    /// <param name="parameter">The parameter is not used.</param>
    /// <param name="culture">The parameter is not used.</param>
    /// <returns>The parameter is not used.</returns>
    /// <exception cref="Exception">An Exception.</exception>
    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new Exception("The method or operation is not implemented.");
}

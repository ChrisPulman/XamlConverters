// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Allows a simple mathematics operator to be executed.</summary>
public class ArithmeticConverter : IValueConverter
{
    /// <summary>The expected number of regular-expression capture groups.</summary>
    private const int ExpectedCaptureGroupCount = 3;

    /// <summary>The index of the operand capture group.</summary>
    private const int OperandGroupIndex = 2;

#if NET7_0_OR_GREATER
    /// <summary>Compiled arithmetic expression.</summary>
    private static readonly Regex ArithmeticRegex = WpfArithmeticRegexProvider.Create();
#else
    /// <summary>REGEX arithmetic expression.</summary>
    private const string ArithmeticParseExpression = "([+\\-*/]{1,1})\\s{0,}(\\-?[\\d\\.]+)";

    /// <summary>Compiled arithmetic expression.</summary>
    private static readonly Regex ArithmeticRegex = new(
        ArithmeticParseExpression,
        RegexOptions.Compiled);
#endif

    /// <summary>Carries out arithmetic on the value based on the parameter.</summary>
    /// <param name="value">The value used as base.</param>
    /// <param name="targetType">Integer and Double.</param>
    /// <param name="parameter">A math operator (+,-,*,/) plus a value.</param>
    /// <param name="culture">Not used.</param>
    /// <returns>Integer or Double based on Math.</returns>
    object IValueConverter.Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        var parameterText = parameter?.ToString();
        if (string.IsNullOrEmpty(parameterText))
        {
            return CreateConversionError();
        }

        var match = ArithmeticRegex.Match(parameterText);
        if (match.Groups.Count != ExpectedCaptureGroupCount)
        {
            return CreateConversionError();
        }

        var operation = match.Groups[1].Value.Trim();
        var numericValue = match.Groups[OperandGroupIndex].Value;
        return value switch
        {
            double doubleValue when double.TryParse(numericValue, out var operand) => Apply(
                doubleValue,
                operand,
                operation),
            int integerValue when int.TryParse(numericValue, out var operand) => Apply(
                integerValue,
                operand,
                operation),
            _ => CreateConversionError(),
        };
    }

    /// <summary>Convert Back Not used.</summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The requested source type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The WPF do-nothing binding sentinel.</returns>
    object IValueConverter.ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture) => Binding.DoNothing;

    /// <summary>Applies an arithmetic operation to double values.</summary>
    /// <param name="value">The left operand.</param>
    /// <param name="operand">The right operand.</param>
    /// <param name="operation">The operation to apply.</param>
    /// <returns>The operation result.</returns>
    private static double Apply(double value, double operand, string operation) =>
        operation switch
        {
            "+" => value + operand,
            "-" => value - operand,
            "*" => value * operand,
            "/" => value / operand,
            _ => 0D,
        };

    /// <summary>Applies an arithmetic operation to integer values.</summary>
    /// <param name="value">The left operand.</param>
    /// <param name="operand">The right operand.</param>
    /// <param name="operation">The operation to apply.</param>
    /// <returns>The operation result.</returns>
    private static double Apply(int value, int operand, string operation) =>
        operation switch
        {
            "+" => value + operand,
            "-" => value - operand,
            "*" => value * operand,
            "/" => (double)value / operand,
            _ => 0D,
        };

    /// <summary>Creates the converter's invalid-input result.</summary>
    /// <returns>An exception describing the invalid input.</returns>
    private static InvalidCastException CreateConversionError() =>
        new(
            "Binding must be an integer or double, and the parameter must contain "
                + "an arithmetic operator (+, -, *, or /) followed by a value.");
}

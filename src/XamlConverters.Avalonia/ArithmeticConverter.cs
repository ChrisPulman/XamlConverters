// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Applies a simple arithmetic parameter such as +5, -2, *1.5, or /10.</summary>
public sealed partial class ArithmeticConverter : IValueConverter
{
    /// <summary>The capture-group name for the arithmetic operator.</summary>
    private const string OperatorGroupName = "op";

    /// <summary>The capture-group name for the numeric operand.</summary>
    private const string NumberGroupName = "number";

    /// <summary>The compiled arithmetic parameter expression.</summary>
    private static readonly Regex Expression = MyRegex();

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!ConversionHelpers.TryDecimal(value, culture, out var left))
        {
            return ConversionHelpers.UnsetValue;
        }

        var match = Expression.Match(parameter?.ToString() ?? string.Empty);
        if (
            !match.Success
            || !decimal.TryParse(
                match.Groups[NumberGroupName].Value,
                NumberStyles.Any,
                culture,
                out var right))
        {
            return ConversionHelpers.UnsetValue;
        }

        var result = match.Groups[OperatorGroupName].Value switch
        {
            "+" => left + right,
            "-" => left - right,
            "*" => left * right,
            "/" when right != 0 => left / right,
            _ => (decimal?)null,
        };
        return result is { } number
            ? ConversionHelpers.ConvertDecimal(number, targetType, culture)
            : ConversionHelpers.UnsetValue;
    }

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.DoNothing;

    [GeneratedRegex(
        "^\\s*(?<"
            + OperatorGroupName
            + ">[+\\-*/])\\s*(?<"
            + NumberGroupName
            + ">[+\\-]?(?:\\d+(?:[.,]\\d*)?|[.,]\\d+))\\s*$",
        RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}

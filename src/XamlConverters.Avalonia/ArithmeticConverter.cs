// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Applies a simple arithmetic parameter such as +5, -2, *1.5, or /10.</summary>
public sealed class ArithmeticConverter : IValueConverter
{
    private static readonly Regex Expression = new("^\\s*(?<op>[+\\-*/])\\s*(?<number>[+\\-]?(?:\\d+(?:[.,]\\d*)?|[.,]\\d+))\\s*$", RegexOptions.Compiled);

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!ConversionHelpers.TryDecimal(value, culture, out var left))
        {
            return ConversionHelpers.UnsetValue;
        }

        var match = Expression.Match(parameter?.ToString() ?? string.Empty);
        if (!match.Success || !decimal.TryParse(match.Groups["number"].Value, NumberStyles.Any, culture, out var right))
        {
            return ConversionHelpers.UnsetValue;
        }

        var result = match.Groups["op"].Value switch
        {
            "+" => left + right,
            "-" => left - right,
            "*" => left * right,
            "/" when right != 0 => left / right,
            _ => (decimal?)null,
        };
        return result is { } number ? ConversionHelpers.ConvertDecimal(number, targetType, culture) : ConversionHelpers.UnsetValue;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Compares a numeric or comparable value with a parameter expression such as &gt;= 10.</summary>
public sealed partial class ComparisonConverter : IValueConverter
{
    /// <summary>The compiled comparison expression.</summary>
    private static readonly Regex Pattern = MyRegex();

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var match = Pattern.Match(parameter?.ToString()?.Trim() ?? string.Empty);
        if (!match.Success || value is null)
        {
            return false;
        }

        var comparison = Compare(value, match.Groups["rhs"].Value, culture);
        var result = match.Groups["op"].Value switch
        {
            ">" => comparison > 0,
            ">=" => comparison >= 0,
            "<" => comparison < 0,
            "<=" => comparison <= 0,
            "==" => comparison == 0,
            "!=" => comparison != 0,
            _ => false,
        };
        return match.Groups["invert"].Value == "!" ? !result : result;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;

    /// <summary>Compares a value with the textual right operand.</summary>
    /// <param name="value">The left operand.</param>
    /// <param name="rightText">The textual right operand.</param>
    /// <param name="culture">The parsing culture.</param>
    /// <returns>A value indicating the operands' relative order.</returns>
    private static int Compare(object value, string rightText, CultureInfo culture)
    {
        return ConversionHelpers.TryDecimal(value, culture, out var left) &&
            decimal.TryParse(rightText, NumberStyles.Any, culture, out var right) ? left.CompareTo(right) : string.Compare(value.ToString(), rightText, StringComparison.OrdinalIgnoreCase);
    }

    [GeneratedRegex("^(?<invert>!?)(?<op>>=|<=|==|!=|>|<)\\s*(?<rhs>.+)$", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Compares a numeric or comparable value with a parameter expression such as &gt;= 10.</summary>
public sealed class ComparisonConverter : IValueConverter
{
    private static readonly Regex Pattern = new("^(?<invert>!?)(?<op>>=|<=|==|!=|>|<)\\s*(?<rhs>.+)$", RegexOptions.Compiled);

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

    private static int Compare(object value, string rightText, CultureInfo culture)
    {
        if (ConversionHelpers.TryDecimal(value, culture, out var left) &&
            decimal.TryParse(rightText, NumberStyles.Any, culture, out var right))
        {
            return left.CompareTo(right);
        }

        return string.Compare(value.ToString(), rightText, StringComparison.OrdinalIgnoreCase);
    }
}

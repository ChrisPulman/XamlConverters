// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Compares the bound numeric (or IComparable) value against a parameter using the supplied operator.
/// Parameter examples: ">5", ">= 10", "!=42", "&lt; -2", "==OFF" (string compare). Prefix with ! to invert result.
/// </summary>
public sealed class ComparisonConverter : IValueConverter
{
    private static readonly Regex _pattern = new("^(?<invert>!{0,1})(?<op>>=|<=|==|!=|>|<)\\s{0,}(?<rhs>.+)$", RegexOptions.Compiled);

    /// <summary>
    /// Executes the comparison.
    /// </summary>
    /// <param name="value">Left side value.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">Comparison expression.</param>
    /// <param name="culture">Culture info.</param>
    /// <returns>True if comparison holds.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter == null)
        {
            return false;
        }

        var paramText = parameter.ToString()!.Trim();
        var m = _pattern.Match(paramText);
        if (!m.Success)
        {
            return false; // invalid parameter
        }

        var invert = m.Groups["invert"].Value == "!";
        var op = m.Groups["op"].Value;
        var rhsText = m.Groups["rhs"].Value;

        int comparison;
        if (value is IComparable)
        {
            // Try to coerce numeric types.
            if (double.TryParse(rhsText, NumberStyles.Any, culture, out var rhsDouble) && value is not string)
            {
                var lhsDouble = System.Convert.ToDouble(value, culture);
                comparison = lhsDouble.CompareTo(rhsDouble);
            }
            else
            {
                comparison = string.Compare(value?.ToString(), rhsText, StringComparison.OrdinalIgnoreCase);
            }
        }
        else
        {
            return false;
        }

        var result = op switch
        {
            ">" => comparison > 0,
            ">=" => comparison >= 0,
            "<" => comparison < 0,
            "<=" => comparison <= 0,
            "==" => comparison == 0,
            "!=" => comparison != 0,
            _ => false,
        };

        return invert ? !result : result;
    }

    /// <summary>
    /// Convert back not supported.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>Binding.DoNothing.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}

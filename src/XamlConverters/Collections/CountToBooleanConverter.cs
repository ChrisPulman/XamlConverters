// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Returns true if collection count satisfies comparison in parameter. Parameter forms: ">0", "==0", "!= 1" etc.
/// Default is >0.
/// </summary>
public sealed class CountToBooleanConverter : IValueConverter
{
    /// <summary>Converts a value.</summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not IEnumerable enumerable)
        {
            return false;
        }

        var count = 0;
        foreach (var a in enumerable)
        {
            count++;
        }

        var parm = parameter?.ToString();
        if (string.IsNullOrWhiteSpace(parm))
        {
            return count > 0;
        }

        // reuse ComparisonConverter logic minimally
        return !CountComparison.TryParse(parm!, out var cmp) ? count > 0 : cmp.Evaluate(count);
    }

    /// <summary>Converts a value.</summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture) => Binding.DoNothing;

    /// <summary>Represents a parsed count comparison.</summary>
    /// <param name="Op">The comparison operator.</param>
    /// <param name="Rhs">The right-hand operand.</param>
    private sealed record CountComparison(string Op, int Rhs)
    {
        /// <summary>Attempts to parse a count comparison.</summary>
        /// <param name="text">The comparison text.</param>
        /// <param name="cmp">The parsed comparison.</param>
        /// <returns><see langword="true"/> when parsing succeeds; otherwise, <see langword="false"/>.</returns>
        public static bool TryParse(string text, out CountComparison cmp)
        {
            text = text.Replace(" ", string.Empty);
            foreach (var op in new[] { ">=", "<=", "==", "!=", ">", "<" })
            {
                var idx = text.IndexOf(op, StringComparison.Ordinal);
                if (idx >= 0 && int.TryParse(text.Remove(0, idx + op.Length), out var rhs))
                {
                    cmp = new(op, rhs);
                    return true;
                }
            }

            cmp = null!;
            return false;
        }

        /// <summary>Evaluates the comparison.</summary>
        /// <param name="lhs">The left-hand operand.</param>
        /// <returns>The comparison result.</returns>
        public bool Evaluate(int lhs) =>
            Op switch
            {
                ">" => lhs > Rhs,
                ">=" => lhs >= Rhs,
                "<" => lhs < Rhs,
                "<=" => lhs <= Rhs,
                "==" => lhs == Rhs,
                "!=" => lhs != Rhs,
                _ => false,
            };
    }
}

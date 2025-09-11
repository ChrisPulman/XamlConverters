// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Returns true if collection count satisfies comparison in parameter. Parameter forms: ">0", "==0", "!= 1" etc. Default is >0.
/// </summary>
public sealed class CountToBooleanConverter : IValueConverter
{
    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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
        if (!CountComparison.TryParse(parm!, out var cmp))
        {
            return count > 0;
        }

        return cmp.Evaluate(count);
    }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;

    private record CountComparison(string Op, int Rhs)
    {
        public bool Evaluate(int lhs) => Op switch
        {
            ">" => lhs > Rhs,
            ">=" => lhs >= Rhs,
            "<" => lhs < Rhs,
            "<=" => lhs <= Rhs,
            "==" => lhs == Rhs,
            "!=" => lhs != Rhs,
            _ => false
        };

        public static bool TryParse(string text, out CountComparison cmp)
        {
            text = text.Replace(" ", string.Empty);
            var ops = new[] { ">=", "<=", "==", "!=", ">", "<" };
            foreach (var op in ops)
            {
                var idx = text.IndexOf(op, StringComparison.Ordinal);
                if (idx >= 0)
                {
                    if (int.TryParse(text[(idx + op.Length)..], out var rhs))
                    {
                        cmp = new CountComparison(op, rhs);
                        return true;
                    }
                }
            }

            cmp = null!;
            return false;
        }
    }
}

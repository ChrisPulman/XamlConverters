// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Value Less Than X To Bool Converter.</summary>
/// <seealso cref="IValueConverter"/>
public class ValueLessThanXToBoolConverter : IValueConverter
{
    /// <summary>Converts a value.</summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    /// <exception cref="InvalidCastException">
    /// Value bound is not a type of value that can be compared to 0.
    /// </exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var parm = parameter?.ToString();
        var val = 0;
        if (!string.IsNullOrWhiteSpace(parm))
        {
            _ = int.TryParse(parm, out val);
        }

        return value switch
        {
            int iValue => iValue < val,
            float fValue => fValue < val,
            double pValue => (object)(pValue < val),
            _ => throw new InvalidCastException("The bound value is not numeric."),
        };
    }

    /// <summary>Converts a value.</summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    /// <exception cref="InvalidCastException">The bound value is not Boolean.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var parm = parameter?.ToString();
        var val = 0;
        if (!string.IsNullOrWhiteSpace(parm))
        {
            _ = int.TryParse(parm, out val);
        }

        return value switch
        {
            bool pValue => pValue ? val - 1 : val,
            _ => throw new InvalidCastException("The bound value is not Boolean."),
        };
    }
}

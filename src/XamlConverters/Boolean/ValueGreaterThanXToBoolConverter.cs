// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Integer To IsEnable.
/// </summary>
/// <seealso cref="IValueConverter"/>
public class ValueGreaterThanXToBoolConverter : IValueConverter
{
    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    /// <exception cref="Exception">An Exception.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var parm = parameter?.ToString();
        var val = 0;
        if (!string.IsNullOrWhiteSpace(parm))
        {
            int.TryParse(parm, out val);
        }

        return value switch
        {
            int pValue => pValue > val,
            float pfValue => pfValue > val,
            double pdValue => (object)(pdValue > val),
            _ => throw new Exception("Value bound is not a type of value that can be compared to > 0"),
        };
    }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    /// <exception cref="Exception">An Exception.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var parm = parameter?.ToString();
        var val = 0;
        if (!string.IsNullOrWhiteSpace(parm))
        {
            int.TryParse(parm, out val);
        }

        return value switch
        {
            bool pValue => pValue ? val + 1 : val,
            _ => throw new Exception("Value bound is not a type of boolean")
        };
    }
}

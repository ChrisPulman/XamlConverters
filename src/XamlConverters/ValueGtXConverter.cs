// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Invert Boolean from one value to another.
/// </summary>
public class ValueGtXConverter : IValueConverter
{
    /// <summary>
    /// Converts a value greater that 40 to use 1 decimal place otherwise 2.
    /// </summary>
    /// <param name="value">takes the binding value.</param>
    /// <param name="targetType">Boolean value.</param>
    /// <param name="parameter">The parameter is not used.</param>
    /// <param name="culture">The parameter is not used.</param>
    /// <returns>Inverted Boolean.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            throw new Exception("The target value is NULL and can not be typeof a double.");
        }

        if (parameter == null)
        {
            throw new Exception("The target parameter is NULL and can not be typeof a double.");
        }

        var val = (double)value;
        var comparator = parameter is string ? double.Parse(parameter.ToString()!) : (double)parameter!;
        return Math.Round(val, val > comparator ? 1 : 2);
    }

    /// <summary>
    /// Not enabled.
    /// </summary>
    /// <param name="value">The parameter is not used.</param>
    /// <param name="targetType">The parameter is not used.</param>
    /// <param name="parameter">The parameter is not used.</param>
    /// <param name="culture">The parameter is not used.</param>
    /// <returns>The parameter is not used.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}

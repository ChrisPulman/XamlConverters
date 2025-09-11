// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Compares value to parameter using Equals. Optional parameter prefix '!' to invert result.
/// </summary>
public sealed class EqualityConverter : IValueConverter
{
    /// <summary>
    /// Performs the equality comparison.
    /// </summary>
    /// <param name="value">Value from binding.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">Comparison parameter (optional '!' prefix).</param>
    /// <param name="culture">Culture info.</param>
    /// <returns>True if equal (with optional inversion).</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter == null)
        {
            return value == null;
        }

        var parmStr = parameter.ToString();
        var invert = false;
        if (!string.IsNullOrEmpty(parmStr) && parmStr![0] == '!')
        {
            invert = true;
            parmStr = parmStr.Substring(1);
        }

        var equals = value?.ToString()?.Equals(parmStr) == true;
        return invert ? !equals : equals;
    }

    /// <summary>
    /// Convert back not supported.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}

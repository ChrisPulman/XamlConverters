// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Multi-binding converter that returns true only if ALL inputs are true. Parameter "invert" to invert final result.
/// </summary>
public sealed class MultiBooleanAndConverter : IMultiValueConverter
{
    /// <summary>
    /// Aggregates values using logical AND.
    /// </summary>
    /// <param name="values">Source values.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">Optional parameter (invert).</param>
    /// <param name="culture">Culture info.</param>
    /// <returns>True when all are true (optionally inverted).</returns>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null)
        {
            throw new ArgumentNullException(nameof(values));
        }

        var result = true;
        foreach (var v in values)
        {
            if (v == DependencyProperty.UnsetValue)
            {
                continue; // skip uninitialized
            }

            if (v is bool b)
            {
                if (!b)
                {
                    result = false;
                    break;
                }
            }
            else if (v == null)
            {
                result = false;
                break;
            }
        }

        var invert = parameter?.ToString()?.Equals("invert", StringComparison.OrdinalIgnoreCase) == true;
        return invert ? !result : result;
    }

    /// <summary>
    /// Convert back not supported.
    /// </summary>
    /// <param name="value">Ignored.</param>
    /// <param name="targetTypes">The array of types to convert to. The array length indicates the number and types of values that are suggested for the method to return.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// Array of DoNothing.
    /// </returns>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => targetTypes.Select(t => Binding.DoNothing).ToArray();
}

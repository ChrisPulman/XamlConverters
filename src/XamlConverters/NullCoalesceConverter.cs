// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Returns the bound value unless it is null/empty (string) in which case returns the parameter.
/// </summary>
public sealed class NullCoalesceConverter : IValueConverter
{
    /// <summary>
    /// Provides fallback for null or empty values.
    /// </summary>
    /// <param name="value">The bound value.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">Fallback value.</param>
    /// <param name="culture">Culture info.</param>
    /// <returns>Original or fallback value.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return parameter ?? string.Empty;
            }

            return s;
        }

        return value ?? parameter ?? string.Empty;
    }

    /// <summary>
    /// Convert back not supported.
    /// </summary>
    /// <param name="value">Ignored.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// Binding.DoNothing.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}

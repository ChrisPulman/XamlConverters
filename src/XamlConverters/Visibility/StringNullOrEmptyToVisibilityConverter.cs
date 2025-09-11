// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Converts a string to Visibility.Collapsed when null or empty, else Visible.
/// Optional parameter: "invert" to invert logic, "hidden" to use Hidden instead of Collapsed.
/// </summary>
public sealed class StringNullOrEmptyToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Converts the bound string to a Visibility.
    /// </summary>
    /// <param name="value">The bound value.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">Parameter tokens (invert, hidden, hiddeninvert).</param>
    /// <param name="culture">Culture info.</param>
    /// <returns>Visibility based on rules.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var str = value as string;
        var isNullOrEmpty = string.IsNullOrEmpty(str);
        var parm = parameter?.ToString()?.ToLowerInvariant();
        var invert = parm == "invert" || parm == "true";
        var useHidden = parm == "hidden" || parm == "hiddeninvert";

        if (parm == "hiddeninvert")
        {
            invert = true;
            useHidden = true;
        }

        var show = invert ? isNullOrEmpty : !isNullOrEmpty;
        if (show)
        {
            return Visibility.Visible;
        }

        return useHidden ? Visibility.Hidden : Visibility.Collapsed;
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

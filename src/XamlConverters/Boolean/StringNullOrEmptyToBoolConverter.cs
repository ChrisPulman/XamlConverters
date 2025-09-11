// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Converts a string to a boolean indicating whether it is (or is not) null/empty.
/// Optional parameter: "invert" (or "true") to invert the result.
/// </summary>
public sealed class StringNullOrEmptyToBoolConverter : IValueConverter
{
    /// <summary>
    /// Converts the string value to a boolean indicating IsNullOrEmpty.
    /// </summary>
    /// <param name="value">The bound value.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="parameter">Optional parameter: invert.</param>
    /// <param name="culture">Culture info.</param>
    /// <returns>True if (possibly inverted) condition met.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isNullOrEmpty = string.IsNullOrEmpty(value as string);
        var invert = parameter != null && (parameter.ToString()?.Equals("invert", StringComparison.OrdinalIgnoreCase) == true || parameter.ToString()?.Equals("true", StringComparison.OrdinalIgnoreCase) == true);
        return invert ? !isNullOrEmpty : isNullOrEmpty;
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

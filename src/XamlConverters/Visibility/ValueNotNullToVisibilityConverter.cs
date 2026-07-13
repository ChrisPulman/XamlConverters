// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Converts a non-null value to visible and a null value to collapsed.</summary>
public sealed class ValueNotNullToVisibilityConverter : IValueConverter
{
    /// <summary>Converts a value.</summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var reverse = "reverse".Equals(parameter) || "true".Equals(parameter);
        var valueIsTrue = value is not null;
        if (reverse)
        {
            valueIsTrue = !valueIsTrue;
        }

        return valueIsTrue ? Visibility.Visible : Visibility.Collapsed;
    }

    /// <summary>Converts a value.</summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    /// <exception cref="NotImplementedException">
    /// The method or operation is not implemented.
    /// </exception>
    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture) => Binding.DoNothing;
}

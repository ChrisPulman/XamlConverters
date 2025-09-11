// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Multi binding string formatter: parameter supplies a composite format string e.g. "{0} - {1:0.00} ({2})".
/// Unset values are treated as empty strings.
/// </summary>
public sealed class MultiStringFormatConverter : IMultiValueConverter
{
    /// <summary>
    /// Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.
    /// </summary>
    /// <param name="values">The array of values that the source bindings in the <see cref="T:System.Windows.Data.MultiBinding" /> produces. The value <see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the source binding has no value to provide for conversion.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value.If the method returns null, the valid null value is used.A return value of <see cref="T:System.Windows.DependencyProperty" />.<see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the converter did not produce a value, and that the binding will use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> if it is available, or else will use the default value.A return value of <see cref="T:System.Windows.Data.Binding" />.<see cref="F:System.Windows.Data.Binding.DoNothing" /> indicates that the binding does not transfer the value or use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> or the default value.
    /// </returns>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var format = parameter as string;
        if (string.IsNullOrEmpty(format))
        {
            return string.Join(" ", values.Where(v => v != DependencyProperty.UnsetValue && v != null));
        }

        var safeValues = values.Select(v => v == DependencyProperty.UnsetValue ? null : v).ToArray();
        try
        {
            return string.Format(culture, format, safeValues);
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Converts a binding target value to the source binding values.
    /// </summary>
    /// <param name="value">The value that the binding target produces.</param>
    /// <param name="targetTypes">The array of types to convert to. The array length indicates the number and types of values that are suggested for the method to return.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// An array of values that have been converted from the target value back to the source values.
    /// </returns>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => targetTypes.Select(_ => Binding.DoNothing).ToArray();
}

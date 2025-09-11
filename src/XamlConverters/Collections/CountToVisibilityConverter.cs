// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Converts collection Count to Visibility using comparison parameter (see CountToBooleanConverter). Optional token 'H' to use Hidden.
/// </summary>
public sealed class CountToVisibilityConverter : IValueConverter
{
    private readonly CountToBooleanConverter _bool = new();

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var parm = parameter?.ToString() ?? string.Empty;
        var useHidden = parm.Contains('H');
        var comparisonPart = parm.Replace("H", string.Empty);
        var result = (bool)_bool.Convert(value, targetType, string.IsNullOrWhiteSpace(comparisonPart) ? null : comparisonPart, culture);
        return result ? Visibility.Visible : useHidden ? Visibility.Hidden : Visibility.Collapsed;
    }

    /// <summary>
    /// Converts a value.
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

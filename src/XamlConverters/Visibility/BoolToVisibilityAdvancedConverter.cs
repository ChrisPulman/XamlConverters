// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Advanced Bool->Visibility converter. Parameter tokens: ! to invert, H to use Hidden for false, C to force Collapsed.
/// Examples: "!H" invert and Hidden; "!" invert with Collapsed; "H" true=Visible false=Hidden.
/// </summary>
public sealed class BoolToVisibilityAdvancedConverter : IValueConverter
{
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
        var b = value is bool x && x;
        var parm = parameter?.ToString() ?? string.Empty;
        var invert = parm.Contains('!');
        var useHidden = parm.Contains('H');
        if (invert)
        {
            b = !b;
        }

        if (b)
        {
            return Visibility.Visible;
        }

        return useHidden ? Visibility.Hidden : Visibility.Collapsed;
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
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility v)
        {
            var parm = parameter?.ToString() ?? string.Empty;
            var invert = parm.Contains('!');
            var isVisible = v == Visibility.Visible;
            return invert ? !isVisible : isVisible;
        }

        return false;
    }
}

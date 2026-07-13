// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Compares an object's text with the parameter, using a leading ! to invert the result.</summary>
public sealed class ObjectEqualsParameterConverter : IValueConverter
{
    /// <summary>Compares the bound value string representation with the parameter.</summary>
    /// <param name="value">The bound value.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="parameter">Comparison string (optional leading ! to invert result).</param>
    /// <param name="culture">The culture.</param>
    /// <returns>True if equal (with optional inversion).</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is null)
        {
            return false;
        }

        var parm = parameter.ToString()!;
        var invert = false;
        if (parm.StartsWith("!", StringComparison.Ordinal))
        {
            invert = true;
            parm = parm.Length > 1 ? parm.Remove(0, 1) : string.Empty;
        }

        var equals = string.Equals(value?.ToString(), parm, StringComparison.OrdinalIgnoreCase);
        return invert ? !equals : equals;
    }

    /// <summary>Convert back not supported.</summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// Binding.DoNothing.
    /// </returns>
    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture) => Binding.DoNothing;
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Multiplies a numeric value by the percentage supplied in the parameter. Parameter may be '50%' or '0.5'.
/// </summary>
public sealed class PercentageConverter : IValueConverter
{
    /// <summary>
    /// Applies the percentage factor.
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
        if (value == null || parameter == null)
        {
            return 0d;
        }

        var baseVal = System.Convert.ToDouble(value, culture);
        var parmText = parameter.ToString()!.Trim();
        double factor;
        if (parmText.EndsWith("%", StringComparison.Ordinal))
        {
            if (!double.TryParse(parmText.TrimEnd('%'), out var pct))
            {
                return baseVal;
            }

            factor = pct / 100d;
        }
        else
        {
            factor = double.TryParse(parmText, out var raw) ? raw : 1d;
        }

        return baseVal * factor;
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

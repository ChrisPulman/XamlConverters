// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CP.Xaml.Converters;

/// <summary>Converts the value to a colour.</summary>
public class ValueToBrushConverter : IValueConverter
{
    /// <summary>Converts the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Gets the brush colour from a value greater than zero.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var val = 0D;
        switch (value)
        {
            case float x:
            {
                val = x;
                break;
            }

            case double x2:
            {
                val = x2;
                break;
            }

            case bool x3:
            {
                val = x3 ? 0 : 1;
                break;
            }
        }

        var parameterText = parameter?.ToString();
        if (!string.IsNullOrWhiteSpace(parameterText))
        {
            if (parameterText!.Contains("BackPressure"))
            {
                return val > 0D ? Brushes.DarkBlue : Brushes.LightYellow;
            }

            if (parameterText.Contains("Pressure"))
            {
                return val > 0D ? Brushes.Black : Brushes.LightYellow;
            }
        }

        return val > 0D ? Brushes.DarkGreen : Brushes.LightYellow;
    }

    /// <summary>Converts the back.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture) => Binding.DoNothing;
}

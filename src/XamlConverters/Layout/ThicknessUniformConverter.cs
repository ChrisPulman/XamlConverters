// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Creates a uniform (or partially uniform) Thickness from a numeric value. Parameter tokens: L,R,T,B,H,V to enable specific sides.
/// Examples: parameter="LRT" sets Left/Right/Top only. Empty or null parameter sets all sides.
/// </summary>
public sealed class ThicknessUniformConverter : IValueConverter
{
    /// <summary>
    /// Creates thickness from a numeric value and parameter tokens.
    /// </summary>
    /// <param name="value">Numeric value.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">Tokens controlling sides.</param>
    /// <param name="culture">Culture info.</param>
    /// <returns>A <see cref="Thickness"/> where only the specified sides are set to the provided value.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var d = System.Convert.ToDouble(value, culture);
        if (parameter == null)
        {
            return new Thickness(d);
        }

        var parm = parameter.ToString()!.ToUpperInvariant();
        var t = default(Thickness); // avoid StyleCop warning SA1129
        if (parm.Length == 0)
        {
            return new Thickness(d);
        }

        if (parm.Contains('L'))
        {
            t.Left = d;
        }

        if (parm.Contains('R'))
        {
            t.Right = d;
        }

        if (parm.Contains('T'))
        {
            t.Top = d;
        }

        if (parm.Contains('B'))
        {
            t.Bottom = d;
        }

        if (parm.Contains('H'))
        {
            t.Left = d;
            t.Right = d;
        }

        if (parm.Contains('V'))
        {
            t.Top = d;
            t.Bottom = d;
        }

        return t;
    }

    /// <summary>
    /// Convert back is not supported for this converter.
    /// </summary>
    /// <param name="value">The value produced by the binding target (unused).</param>
    /// <param name="targetType">The type to convert to (unused).</param>
    /// <param name="parameter">The converter parameter to use (unused).</param>
    /// <param name="culture">The culture to use in the converter (unused).</param>
    /// <returns><see cref="Binding.DoNothing"/>.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}

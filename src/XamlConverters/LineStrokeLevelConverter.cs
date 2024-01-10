// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CP.Xaml.Converters;

/// <summary>
/// Line Stroke Level Converter.
/// </summary>
public class LineStrokeLevelConverter : IValueConverter
{
    /// <summary>
    /// Converts the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var val = default(int);
        if (value is int type)
        {
            val = type;
        }

        var par = parameter?.ToString()!.Split('-');
        if (par?.Length >= 2)
        {
            var highVal = int.Parse(par[1]);
            var lowVal = int.Parse(par[0]);
            return val >= highVal ? Brushes.Red : val >= lowVal ? Brushes.Yellow : Brushes.Lime;
        }

        return Brushes.Red;
    }

    /// <summary>
    /// Converts the back.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
}

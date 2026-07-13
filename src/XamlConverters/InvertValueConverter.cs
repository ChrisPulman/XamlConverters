// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Invert Boolean from one value to another.</summary>
public class InvertValueConverter : IValueConverter
{
    /// <summary>Converts a boolean to the inverse value.</summary>
    /// <param name="value">takes the binding value.</param>
    /// <param name="targetType">Boolean value.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>Inverted Boolean.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch
    {
        null => throw new ArgumentNullException(nameof(value)),
        _ => targetType?.FullName switch
        {
            "System.Double" => (double)value * -1,
            "System.Single" => (float)value * -1,
            "System.Int64" => (long)value * -1,
            "System.Int32" => (int)value * -1,
            "System.Int16" => (short)value * -1,
            _ => value,
        }
    };

    /// <summary>Not enabled.</summary>
    /// <param name="value">The target value to invert.</param>
    /// <param name="targetType">The requested source type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The sign-inverted source value.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value switch
    {
        null => throw new ArgumentNullException(nameof(value)),
        _ => targetType?.FullName switch
        {
            "System.Double" => (double)value * -1,
            "System.Single" => (float)value * -1,
            "System.Int64" => (long)value * -1,
            "System.Int32" => (int)value * -1,
            "System.Int16" => (short)value * -1,
            _ => value,
        }
    };
}

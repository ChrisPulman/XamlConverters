// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Enumerator Converter.</summary>
public class EnumConverter : IValueConverter
{
    /// <summary>Converts the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Convert to required.</returns>
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var enumValue = default(Enum);
        if (parameter is Type type && value is not null)
        {
            enumValue = (Enum)Enum.Parse(type, value!.ToString()!);
        }

        return enumValue;
    }

    /// <summary>Converts the back.</summary>
    /// <param name="value">The value.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>A Value.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var returnValue = 0;
        if (parameter is Type type && value is not null)
        {
            returnValue = (int)Enum.Parse(type, value.ToString()!);
        }

        return returnValue;
    }
}

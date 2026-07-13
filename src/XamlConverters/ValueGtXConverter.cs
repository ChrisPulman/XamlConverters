// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Invert Boolean from one value to another.</summary>
public class ValueGtXConverter : IValueConverter
{
    /// <summary>The precision used at or below the threshold.</summary>
    private const int DefaultPrecision = 2;

    /// <summary>Converts a value greater that 40 to use 1 decimal place otherwise 2.</summary>
    /// <param name="value">takes the binding value.</param>
    /// <param name="targetType">Boolean value.</param>
    /// <param name="parameter">The comparison threshold.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>Inverted Boolean.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (parameter is null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        var val = (double)value;
        var comparator = parameter is string ? double.Parse(parameter.ToString()!) : (double)parameter!;
        return Math.Round(val, val > comparator ? 1 : DefaultPrecision);
    }

    /// <summary>Not enabled.</summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The requested source type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The WPF do-nothing binding sentinel.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}

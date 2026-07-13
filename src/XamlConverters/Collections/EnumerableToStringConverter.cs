// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Joins the formatted items of an enumerable into one string.</summary>
public sealed class EnumerableToStringConverter : IValueConverter
{
    /// <summary>Joins collection items using the converter parameter as a separator.</summary>
    /// <param name="value">The enumerable to join.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">An optional separator; the default is <c>, </c>.</param>
    /// <param name="culture">The culture used to format individual items.</param>
    /// <returns>The joined string, an empty string for null, or <see cref="Binding.DoNothing"/> for a non-enumerable
    /// value.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return string.Empty;
        }

        if (value is string text)
        {
            return text;
        }

        if (value is not IEnumerable enumerable)
        {
            return Binding.DoNothing;
        }

        var separator = parameter?.ToString() ?? ", ";
        var values = new List<string>();
        foreach (var item in enumerable)
        {
            values.Add(
                item is IFormattable formattable
                    ? formattable.ToString(null, culture)
                    : item?.ToString() ?? string.Empty);
        }

        return string.Join(separator, values);
    }

    /// <summary>Reverse conversion is unsupported because element types and escaping rules are ambiguous.</summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns><see cref="Binding.DoNothing"/>.</returns>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => Binding.DoNothing;
}

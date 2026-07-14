// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Joins the culture-aware text of enumerable items into one string.</summary>
public sealed class EnumerableToStringConverter : IValueConverter
{
    /// <inheritdoc/>
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
            return ConversionHelpers.DoNothing;
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

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.DoNothing;
}

// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Checks whether an enumerable or string contains the converter parameter.</summary>
public sealed class CollectionContainsConverter : IValueConverter
{
    /// <summary>Searches the source collection for the requested value.</summary>
    /// <param name="value">The source enumerable or string.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">The item or substring to find.</param>
    /// <param name="culture">The culture used for string comparison and parameter coercion.</param>
    /// <returns><see langword="true"/> when the source contains the requested value.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (culture is null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        if (value is string text)
        {
            var searchText = parameter?.ToString();
            return searchText is not null
                && culture.CompareInfo.IndexOf(text, searchText, CompareOptions.None) >= 0;
        }

        if (value is not IEnumerable enumerable)
        {
            return false;
        }

        foreach (var item in enumerable)
        {
            if (ItemsEqual(item, parameter, culture))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>Reverse conversion is not supported.</summary>
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

    /// <summary>Determines whether a collection item equals the requested value directly or after conversion.</summary>
    /// <param name="item">The collection item.</param>
    /// <param name="requestedValue">The requested value.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns><see langword="true"/> when the values are equal; otherwise, <see langword="false"/>.</returns>
    private static bool ItemsEqual(object? item, object? requestedValue, CultureInfo culture) =>
        Equals(item, requestedValue)
        || (
            item is not null
            && requestedValue is not null
            && BclConversion.TryChangeType(
                requestedValue,
                item.GetType(),
                culture,
                out var converted)
            && Equals(item, converted));
}

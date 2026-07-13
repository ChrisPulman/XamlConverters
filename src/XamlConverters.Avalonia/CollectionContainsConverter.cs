// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Determines whether an enumerable or string contains a requested value.</summary>
public sealed class CollectionContainsConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(culture);

        if (value is string text)
        {
            var searchText = parameter?.ToString();
            return searchText is not null && culture.CompareInfo.IndexOf(text, searchText, CompareOptions.None) >= 0;
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

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;

    /// <summary>Determines whether a collection item equals the requested value directly or after conversion.</summary>
    /// <param name="item">The collection item.</param>
    /// <param name="requestedValue">The requested value.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns><see langword="true"/> when the values are equal; otherwise, <see langword="false"/>.</returns>
    private static bool ItemsEqual(object? item, object? requestedValue, CultureInfo culture) =>
        Equals(item, requestedValue)
        || (item is not null
            && requestedValue is not null
            && ConversionHelpers.TryConvert(requestedValue, item.GetType(), culture, out var converted)
            && Equals(item, converted));
}

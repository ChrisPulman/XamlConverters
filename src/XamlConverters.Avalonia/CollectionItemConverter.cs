// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Selects an item from a dictionary by key or an enumerable by zero-based index.</summary>
public sealed class CollectionItemConverter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is IDictionary dictionary)
        {
            return parameter is not null && dictionary.Contains(parameter)
                ? dictionary[parameter]
                : ConversionHelpers.DoNothing;
        }

        if (
            !ConversionHelpers.TryConvert(parameter, typeof(int), culture, out var convertedIndex)
            || convertedIndex is not int index
            || index < 0)
        {
            return ConversionHelpers.DoNothing;
        }

        if (value is IList list)
        {
            return index < list.Count ? list[index] : ConversionHelpers.DoNothing;
        }

        return value is not IEnumerable enumerable
            ? ConversionHelpers.DoNothing
            : GetItem(enumerable, index);
    }

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.DoNothing;

    /// <summary>Gets an item from an enumerable by index.</summary>
    /// <param name="enumerable">The enumerable source.</param>
    /// <param name="index">The zero-based index.</param>
    /// <returns>The selected item, or the do-nothing sentinel when the index is unavailable.</returns>
    private static object? GetItem(IEnumerable enumerable, int index)
    {
        var currentIndex = 0;
        foreach (var item in enumerable)
        {
            if (currentIndex == index)
            {
                return item;
            }

            currentIndex++;
        }

        return ConversionHelpers.DoNothing;
    }
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Selects an item from a dictionary by key or from another enumerable by zero-based index.
/// </summary>
public sealed class CollectionItemConverter : IValueConverter
{
    /// <summary>
    /// Selects the requested collection item.
    /// </summary>
    /// <param name="value">A dictionary or enumerable source.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">A dictionary key or zero-based integer index.</param>
    /// <param name="culture">The culture used to parse an index.</param>
    /// <returns>The selected item, or <see cref="Binding.DoNothing"/> when the key or index is invalid.</returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is IDictionary dictionary)
        {
            return parameter != null && dictionary.Contains(parameter)
                ? dictionary[parameter]
                : Binding.DoNothing;
        }

        if (!BclConversion.TryChangeType(parameter, typeof(int), culture, out var convertedIndex)
            || convertedIndex is not int index
            || index < 0)
        {
            return Binding.DoNothing;
        }

        if (value is IList list)
        {
            return index < list.Count ? list[index] : Binding.DoNothing;
        }

        if (value is not IEnumerable enumerable)
        {
            return Binding.DoNothing;
        }

        var currentIndex = 0;
        foreach (var item in enumerable)
        {
            if (currentIndex == index)
            {
                return item;
            }

            currentIndex++;
        }

        return Binding.DoNothing;
    }

    /// <summary>
    /// Reverse conversion is not supported.
    /// </summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns><see cref="Binding.DoNothing"/>.</returns>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Binding.DoNothing;
}

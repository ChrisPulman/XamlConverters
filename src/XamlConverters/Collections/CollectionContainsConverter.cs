// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Determines whether an enumerable or string contains the value supplied as the converter parameter.
/// </summary>
public sealed class CollectionContainsConverter : IValueConverter
{
    /// <summary>
    /// Searches the source collection for the requested value.
    /// </summary>
    /// <param name="value">The source enumerable or string.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">The item or substring to find.</param>
    /// <param name="culture">The culture used for string comparison and parameter coercion.</param>
    /// <returns><see langword="true"/> when the source contains the requested value.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (culture == null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        if (value is string text)
        {
            var searchText = parameter?.ToString();
            return searchText != null
                && culture.CompareInfo.IndexOf(text, searchText, CompareOptions.None) >= 0;
        }

        if (value is not IEnumerable enumerable)
        {
            return false;
        }

        foreach (var item in enumerable)
        {
            if (Equals(item, parameter))
            {
                return true;
            }

            if (item != null
                && parameter != null
                && BclConversion.TryChangeType(parameter, item.GetType(), culture, out var converted)
                && Equals(item, converted))
            {
                return true;
            }
        }

        return false;
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

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            if (Equals(item, parameter)
                || (item is not null
                    && parameter is not null
                    && ConversionHelpers.TryConvert(parameter, item.GetType(), culture, out var converted)
                    && Equals(item, converted)))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

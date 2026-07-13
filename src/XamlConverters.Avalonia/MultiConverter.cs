// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Metadata;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Composes value converters into a forward-only conversion pipeline.</summary>
public sealed class MultiConverter : IValueConverter
{
    /// <summary>Gets the ordered converter pipeline.</summary>
    [Content]
    public IList<IValueConverter> Converters { get; } = new List<IValueConverter>();

    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var current = value;
        foreach (var converter in Converters)
        {
            current = converter.Convert(current, targetType, parameter, culture);
            if (ConversionHelpers.IsUnset(current))
            {
                break;
            }
        }

        return current;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

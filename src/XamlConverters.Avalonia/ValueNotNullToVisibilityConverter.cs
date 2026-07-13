// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Shows non-null values and collapses null values, optionally inverted.</summary>
public sealed class ValueNotNullToVisibilityConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var show = !ConversionHelpers.IsNullLike(value);
        if (parameter is true || string.Equals(parameter?.ToString(), "reverse", StringComparison.OrdinalIgnoreCase))
        {
            show = !show;
        }

        return show;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

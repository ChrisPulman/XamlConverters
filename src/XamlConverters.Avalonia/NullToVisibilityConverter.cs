// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Shows null values and collapses non-null values, optionally inverted.</summary>
public sealed class NullToVisibilityConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var show = ConversionHelpers.IsNullLike(value);
        if (string.Equals(parameter?.ToString(), "inverse", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(parameter?.ToString(), "invert", StringComparison.OrdinalIgnoreCase))
        {
            show = !show;
        }

        return show;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

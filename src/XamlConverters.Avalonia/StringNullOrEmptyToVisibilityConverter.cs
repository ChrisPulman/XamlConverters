// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Shows non-empty strings and collapses empty strings, with invert and hidden options.</summary>
public sealed class StringNullOrEmptyToVisibilityConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var tokens = parameter?.ToString() ?? string.Empty;
        var show = !string.IsNullOrEmpty(value as string);
        if (
            tokens.Contains("invert", StringComparison.OrdinalIgnoreCase)
            || string.Equals(tokens, "true", StringComparison.OrdinalIgnoreCase))
        {
            show = !show;
        }

        return show;
    }

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.DoNothing;
}

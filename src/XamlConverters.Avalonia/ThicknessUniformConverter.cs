// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Creates an Avalonia Thickness with selected sides set to a numeric value.</summary>
public sealed class ThicknessUniformConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!ConversionHelpers.TryDecimal(value, culture, out var number))
        {
            return ConversionHelpers.UnsetValue;
        }

        var amount = (double)number;
        var tokens = parameter?.ToString()?.ToUpperInvariant();
        if (string.IsNullOrEmpty(tokens))
        {
            return new Thickness(amount);
        }

        var left = tokens.Contains('L') || tokens.Contains('H') ? amount : 0;
        var right = tokens.Contains('R') || tokens.Contains('H') ? amount : 0;
        var top = tokens.Contains('T') || tokens.Contains('V') ? amount : 0;
        var bottom = tokens.Contains('B') || tokens.Contains('V') ? amount : 0;
        return new Thickness(left, top, right, bottom);
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

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

        var left = ContainsEither(tokens, 'L', 'H') ? amount : 0;
        var right = ContainsEither(tokens, 'R', 'H') ? amount : 0;
        var top = ContainsEither(tokens, 'T', 'V') ? amount : 0;
        var bottom = ContainsEither(tokens, 'B', 'V') ? amount : 0;
        return new Thickness(left, top, right, bottom);
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;

    /// <summary>Determines whether text contains either of two characters.</summary>
    /// <param name="text">The text to search.</param>
    /// <param name="first">The first character.</param>
    /// <param name="second">The second character.</param>
    /// <returns><see langword="true"/> when either character is present; otherwise, <see langword="false"/>.</returns>
    private static bool ContainsEither(string text, char first, char second) => text.Contains(first) || text.Contains(second);
}

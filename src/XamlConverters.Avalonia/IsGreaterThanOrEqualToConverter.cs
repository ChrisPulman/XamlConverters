// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Tests whether a value is greater than or equal to a parameter or second multi-binding value.</summary>
public sealed class IsGreaterThanOrEqualToConverter : IValueConverter, IMultiValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        Compare(value, parameter, culture);

    /// <inheritdoc/>
    object IMultiValueConverter.Convert(
        IList<object?> values,
        Type targetType,
        object? parameter,
        CultureInfo culture) => values.Count >= 2 && Compare(values[0], values[1], culture);

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.DoNothing;

    /// <summary>Compares two numeric values.</summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns><see langword="true"/> when the left operand is greater than or equal to the right operand.</returns>
    private static bool Compare(object? left, object? right, CultureInfo culture) =>
        ConversionHelpers.TryDecimal(left, culture, out var first)
        && ConversionHelpers.TryDecimal(right, culture, out var second)
        && first >= second;
}

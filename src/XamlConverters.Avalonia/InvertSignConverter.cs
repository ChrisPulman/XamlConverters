// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Negates any BCL numeric value.</summary>
public sealed class InvertSignConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        Negate(value, targetType, culture);

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => Negate(value, targetType, culture);

    /// <summary>Negates a numeric value.</summary>
    /// <param name="value">The value to negate.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The negated value or an Avalonia binding sentinel.</returns>
    private static object Negate(object? value, Type targetType, CultureInfo culture) =>
        ConversionHelpers.TryDecimal(value, culture, out var number)
            ? ConversionHelpers.ConvertDecimal(-number, targetType, culture)
            : ConversionHelpers.UnsetValue;
}

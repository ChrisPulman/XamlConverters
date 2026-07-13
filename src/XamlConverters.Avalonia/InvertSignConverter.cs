// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Negates any BCL numeric value.</summary>
public sealed class InvertSignConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => Negate(value, targetType, culture);

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Negate(value, targetType, culture);

    private static object Negate(object? value, Type targetType, CultureInfo culture) =>
        ConversionHelpers.TryDecimal(value, culture, out var number)
            ? ConversionHelpers.ConvertDecimal(-number, targetType, culture)
            : ConversionHelpers.UnsetValue;
}

// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Rounds with one decimal above the threshold parameter and two decimals otherwise.</summary>
public sealed class ValueGtXConverter : IValueConverter
{
    /// <summary>The precision used at or below the threshold.</summary>
    private const int DefaultPrecision = 2;

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        ConversionHelpers.TryDecimal(value, culture, out var number) && ConversionHelpers.TryDecimal(parameter, culture, out var threshold)
            ? Math.Round(number, number > threshold ? 1 : DefaultPrecision)
            : ConversionHelpers.UnsetValue;

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

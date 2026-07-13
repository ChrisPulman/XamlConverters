// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Maps positive numeric values to visible and non-positive values to hidden.</summary>
public sealed class ValueCompareVisibilityConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        ConversionHelpers.TryDecimal(value, culture, out var number) && number > 0;

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.IsTrue(value, culture) ? 1 : 0;
}

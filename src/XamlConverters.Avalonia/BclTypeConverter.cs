// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Converts any supported BCL value to the binding target type.</summary>
public sealed class BclTypeConverter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) =>
        ConversionHelpers.TryConvert(value, targetType, culture, out var result)
            ? result
            : ConversionHelpers.UnsetValue;

    /// <inheritdoc/>
    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) =>
        ConversionHelpers.TryConvert(value, targetType, culture, out var result)
            ? result
            : ConversionHelpers.UnsetValue;
}

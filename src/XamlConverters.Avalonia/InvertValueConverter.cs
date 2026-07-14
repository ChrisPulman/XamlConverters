// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Compatibility name for numeric sign inversion.</summary>
public sealed class InvertValueConverter : IValueConverter
{
    /// <summary>The converter that negates numeric values.</summary>
    private readonly InvertSignConverter _inner = new();

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        _inner.Convert(value, targetType, parameter, culture);

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => _inner.ConvertBack(value, targetType, parameter, culture);
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Compatibility name for numeric sign inversion.</summary>
public sealed class InvertValueConverter : IValueConverter
{
    private readonly InvertSignConverter _inner = new();

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => _inner.Convert(value, targetType, parameter, culture);

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => _inner.ConvertBack(value, targetType, parameter, culture);
}

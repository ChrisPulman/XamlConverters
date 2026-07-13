// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Inverts visible and non-visible states.</summary>
public sealed class InvertVisibilityConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => Invert(value);

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Invert(value);

    /// <summary>Inverts a Boolean visibility state.</summary>
    /// <param name="value">The value to invert.</param>
    /// <returns>The inverted value or an Avalonia binding sentinel.</returns>
    private static object Invert(object? value) => value is bool visible ? !visible : ConversionHelpers.UnsetValue;
}

// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Selects between configurable values for true and false.</summary>
public sealed class BooleanToValueConverter : IValueConverter
{
    /// <summary>Gets or sets a value used by the converter.</summary>
    public object? TrueValue { get; set; } = true;

    /// <summary>Gets or sets a value used by the converter.</summary>
    public object? FalseValue { get; set; }

    /// <inheritdoc/>
    public object? Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.IsTrue(value, culture) ? TrueValue : FalseValue;

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => Equals(value, TrueValue);
}

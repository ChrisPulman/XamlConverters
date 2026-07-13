// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Selects between configurable values for true and false.</summary>
public sealed class BooleanToValueConverter : IValueConverter
{
    /// <summary>
    /// Gets or sets a value used by the converter.
    /// </summary>
    public object? TrueValue { get; set; } = true;

    /// <summary>
    /// Gets or sets a value used by the converter.
    /// </summary>
    public object? FalseValue { get; set; } = false;

    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        ConversionHelpers.IsTrue(value, culture) ? TrueValue : FalseValue;

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Equals(value, TrueValue);
}

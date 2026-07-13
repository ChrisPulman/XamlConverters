// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Returns whether the bound value is null.</summary>
public sealed class NullToBoolConverter : IValueConverter
{
    /// <summary>Gets a reusable converter value.</summary>
    public static NullToBoolConverter IsNull { get; } = new() { ReturnTrueIfNull = true };

    /// <summary>Gets a reusable converter value.</summary>
    public static NullToBoolConverter NotNull { get; } = new() { ReturnTrueIfNull = false };

    /// <summary>Gets or sets a value indicating whether null produces <see langword="true"/>.</summary>
    public bool ReturnTrueIfNull { get; set; }

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var isNull = ConversionHelpers.IsNullLike(value);
        return ReturnTrueIfNull ? isNull : !isNull;
    }

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.DoNothing;
}

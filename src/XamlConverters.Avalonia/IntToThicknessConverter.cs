// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Sets selected sides of a base Thickness to an integer input.</summary>
public sealed class IntToThicknessConverter : IValueConverter
{
    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static IntToThicknessConverter BottomOnly { get; } = new() { Bottom = true };

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static IntToThicknessConverter LeftOnly { get; } = new() { Left = true };

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static IntToThicknessConverter RightOnly { get; } = new() { Right = true };

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static IntToThicknessConverter TopOnly { get; } = new() { Top = true };

    /// <summary>
    /// Gets or sets a value indicating whether the bottom side is replaced.
    /// </summary>
    public bool Bottom { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the left side is replaced.
    /// </summary>
    public bool Left { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the right side is replaced.
    /// </summary>
    public bool Right { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the top side is replaced.
    /// </summary>
    public bool Top { get; set; }

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!ConversionHelpers.TryDecimal(value, culture, out var number))
        {
            return ConversionHelpers.UnsetValue;
        }

        var input = (double)number;
        var basis = parameter is Thickness thickness ? thickness : default;
        return new Thickness(Left ? input : basis.Left, Top ? input : basis.Top, Right ? input : basis.Right, Bottom ? input : basis.Bottom);
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

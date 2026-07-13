// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Returns true for values which are neither null nor an Avalonia unset sentinel.</summary>
public sealed class ValueNotNullToBoolConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var result = !ConversionHelpers.IsNullLike(value);
        return IsInverted(parameter) ? !result : result;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;

    private static bool IsInverted(object? parameter) =>
        parameter is true || string.Equals(parameter?.ToString(), "reverse", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(parameter?.ToString(), "invert", StringComparison.OrdinalIgnoreCase);
}

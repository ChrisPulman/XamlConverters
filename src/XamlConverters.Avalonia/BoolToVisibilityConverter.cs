// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Maps Boolean values to Avalonia visibility.</summary>
public sealed class BoolToVisibilityConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var result = ConversionHelpers.IsTrue(value, culture);
        if (IsInverted(parameter))
        {
            result = !result;
        }

        return result;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var result = ConversionHelpers.IsTrue(value, culture);
        return IsInverted(parameter) ? !result : result;
    }

    private static bool IsInverted(object? parameter) =>
        parameter is true || string.Equals(parameter?.ToString(), "reverse", StringComparison.OrdinalIgnoreCase);
}

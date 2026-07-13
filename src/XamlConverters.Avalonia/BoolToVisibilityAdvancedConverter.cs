// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Maps Boolean values to Avalonia visibility with inversion and hidden/collapsed tokens.</summary>
public sealed class BoolToVisibilityAdvancedConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var tokens = parameter?.ToString() ?? string.Empty;
        var result = ConversionHelpers.IsTrue(value, culture);
        if (tokens.Contains('!'))
        {
            result = !result;
        }

        return result;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var result = ConversionHelpers.IsTrue(value, culture);
        return parameter?.ToString()?.Contains('!') == true ? !result : result;
    }
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Maps true to full opacity and false to a configurable opacity parameter.</summary>
public sealed class BoolToOpacityConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var falseOpacity = ConversionHelpers.TryDecimal(parameter, culture, out var opacity)
            ? Math.Clamp((double)opacity, 0d, 1d)
            : 0d;
        return ConversionHelpers.IsTrue(value, culture) ? 1d : falseOpacity;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        ConversionHelpers.TryDecimal(value, culture, out var opacity) && opacity >= 1m;
}

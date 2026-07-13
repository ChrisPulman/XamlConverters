// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Tests whether an enumerable has the requested exact size.</summary>
public sealed class CollectionSizeToBoolConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var reverse = string.Equals(parameter?.ToString(), "reverse", StringComparison.OrdinalIgnoreCase);
        var expected = !reverse && int.TryParse(parameter?.ToString(), NumberStyles.Integer, culture, out var parsed) ? parsed : 0;
        var matches = ConversionHelpers.TryCount(value, out var count) && count == expected;
        return reverse ? !matches : matches;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

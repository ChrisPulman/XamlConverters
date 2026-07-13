// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Formats a single value using the parameter as a composite or standard format string.</summary>
public sealed class StringFormatConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var format = parameter?.ToString();
        if (string.IsNullOrEmpty(format))
        {
            return System.Convert.ToString(value, culture) ?? string.Empty;
        }

        return format.Contains("{0", StringComparison.Ordinal)
            ? string.Format(culture, format, value)
            : value is IFormattable formattable ? formattable.ToString(format, culture) ?? string.Empty : value?.ToString() ?? string.Empty;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

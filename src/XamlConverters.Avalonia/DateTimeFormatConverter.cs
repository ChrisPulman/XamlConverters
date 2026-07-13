// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Formats a date/time value with a standard or custom format string.</summary>
public sealed class DateTimeFormatConverter : IValueConverter
{
    /// <summary>Gets or sets a value used by the converter.</summary>
    public string Format { get; set; } = "g";

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var format = parameter?.ToString() ?? Format;
        return value switch
        {
            DateTime dateTime => dateTime.ToString(format, culture),
            DateTimeOffset dateTimeOffset => dateTimeOffset.ToString(format, culture),
            DateOnly dateOnly => dateOnly.ToString(format, culture),
            _ => string.Empty,
        };
    }

    /// <inheritdoc/>
    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) =>
        ConversionHelpers.TryConvert(value, targetType, culture, out var result)
            ? result
            : ConversionHelpers.UnsetValue;
}

// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Formats a TimeSpan using a standard or custom format string.</summary>
public sealed class TimeSpanFormatConverter : IValueConverter
{
    /// <summary>Gets or sets a value used by the converter.</summary>
    public string Format { get; set; } = "c";

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is TimeSpan timeSpan
            ? timeSpan.ToString(parameter?.ToString() ?? Format, culture)
            : string.Empty;

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

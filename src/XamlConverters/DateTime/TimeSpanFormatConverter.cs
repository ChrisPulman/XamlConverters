// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Formats and parses <see cref="TimeSpan"/> values.
/// </summary>
public sealed class TimeSpanFormatConverter : IValueConverter
{
    /// <summary>
    /// Formats a duration using the optional converter parameter as its format string.
    /// </summary>
    /// <param name="value">The duration to format.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">An optional standard or custom <see cref="TimeSpan"/> format.</param>
    /// <param name="culture">The culture used for formatting.</param>
    /// <returns>The formatted duration, or <see cref="Binding.DoNothing"/> for another value type.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TimeSpan timeSpan)
        {
            return Binding.DoNothing;
        }

        try
        {
            return timeSpan.ToString(parameter as string, culture);
        }
        catch (FormatException)
        {
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// Parses duration text back to <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="value">The target text.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">An optional exact <see cref="TimeSpan"/> format.</param>
    /// <param name="culture">The culture used for parsing.</param>
    /// <returns>The parsed duration, or <see cref="Binding.DoNothing"/> when parsing fails.</returns>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var text = value as string;
        if (string.IsNullOrWhiteSpace(text))
        {
            return Nullable.GetUnderlyingType(targetType) != null ? null! : Binding.DoNothing;
        }

        var format = parameter as string;
        var parsed = string.IsNullOrWhiteSpace(format)
            ? TimeSpan.TryParse(text, culture, out var timeSpan)
            : TimeSpan.TryParseExact(text, format, culture, out timeSpan);
        return parsed ? timeSpan : Binding.DoNothing;
    }
}

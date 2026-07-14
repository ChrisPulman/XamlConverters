// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Formats and parses <see cref="System.DateTime"/> and <see cref="DateTimeOffset"/> values.</summary>
public sealed class DateTimeFormatConverter : IValueConverter
{
    /// <summary>Formats a date/time value using the optional converter parameter as its format string.</summary>
    /// <param name="value">A <see cref="System.DateTime"/> or <see cref="DateTimeOffset"/>.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">An optional standard or custom date/time format.</param>
    /// <param name="culture">The culture used for formatting.</param>
    /// <returns>The formatted text, or <see cref="Binding.DoNothing"/> for another value type.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var format = parameter as string;
        if (value is DateTime dateTime)
        {
            return dateTime.ToString(format, culture);
        }

        return value is DateTimeOffset dateTimeOffset
            ? dateTimeOffset.ToString(format, culture)
            : Binding.DoNothing;
    }

    /// <summary>Parses formatted text back to <see cref="System.DateTime"/> or <see cref="DateTimeOffset"/>.</summary>
    /// <param name="value">The target text.</param>
    /// <param name="targetType">The requested date/time type.</param>
    /// <param name="parameter">An optional exact date/time format.</param>
    /// <param name="culture">The culture used for parsing.</param>
    /// <returns>The parsed date/time value, or <see cref="Binding.DoNothing"/> when parsing fails.</returns>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        var text = value as string;
        if (string.IsNullOrWhiteSpace(text))
        {
            return Nullable.GetUnderlyingType(targetType) is not null ? null! : Binding.DoNothing;
        }

        var destinationType = Nullable.GetUnderlyingType(targetType) ?? targetType;
        var format = parameter as string;
        if (destinationType == typeof(DateTime))
        {
            var parsed = string.IsNullOrWhiteSpace(format)
                ? System.DateTime.TryParse(
                    text,
                    culture,
                    DateTimeStyles.AllowWhiteSpaces,
                    out var dateTime)
                : System.DateTime.TryParseExact(
                    text,
                    format,
                    culture,
                    DateTimeStyles.AllowWhiteSpaces,
                    out dateTime);
            return parsed ? dateTime : Binding.DoNothing;
        }

        if (destinationType == typeof(DateTimeOffset))
        {
            var parsed = string.IsNullOrWhiteSpace(format)
                ? DateTimeOffset.TryParse(
                    text,
                    culture,
                    DateTimeStyles.AllowWhiteSpaces,
                    out var dateTimeOffset)
                : DateTimeOffset.TryParseExact(
                    text,
                    format,
                    culture,
                    DateTimeStyles.AllowWhiteSpaces,
                    out dateTimeOffset);
            return parsed ? dateTimeOffset : Binding.DoNothing;
        }

        return Binding.DoNothing;
    }
}

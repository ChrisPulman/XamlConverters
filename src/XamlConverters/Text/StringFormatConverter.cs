// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Formats one value using either a standard/custom format specifier or a composite format string.
/// </summary>
public sealed class StringFormatConverter : IValueConverter
{
    /// <summary>
    /// Formats the supplied value.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">A format such as <c>N2</c>, <c>yyyy-MM-dd</c>, or <c>Value: {0}</c>.</param>
    /// <param name="culture">The culture used for formatting.</param>
    /// <returns>The formatted text, or <see cref="Binding.DoNothing"/> for an invalid format.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null || value == DependencyProperty.UnsetValue)
        {
            return string.Empty;
        }

        var format = parameter?.ToString();
        try
        {
            if (string.IsNullOrEmpty(format))
            {
                return value is IFormattable defaultFormattable
                    ? defaultFormattable.ToString(null, culture)
                    : value.ToString() ?? string.Empty;
            }

            if (format!.IndexOf("{0", StringComparison.Ordinal) >= 0)
            {
                return string.Format(culture, format, value);
            }

            return value is IFormattable formattable
                ? formattable.ToString(format, culture)
                : value.ToString() ?? string.Empty;
        }
        catch (FormatException)
        {
            return Binding.DoNothing;
        }
    }

    /// <summary>
    /// Parses the text back to the requested source type using the common BCL conversion rules.
    /// </summary>
    /// <param name="value">The formatted target value.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture used for parsing.</param>
    /// <returns>The parsed value, or <see cref="Binding.DoNothing"/> when parsing fails.</returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        BclConversion.TryChangeType(value, targetType, culture, out var result)
            ? result
            : Binding.DoNothing;
}

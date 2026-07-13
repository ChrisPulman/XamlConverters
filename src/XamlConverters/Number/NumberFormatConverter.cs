// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Formats any built-in numeric value and parses the result back to the requested numeric type.</summary>
public sealed class NumberFormatConverter : IValueConverter
{
    /// <summary>Formats a number using the optional standard or custom numeric format in the converter parameter.</summary>
    /// <param name="value">The numeric value.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">An optional format such as <c>N2</c>, <c>C</c>, or <c>0.###</c>.</param>
    /// <param name="culture">The culture used for formatting.</param>
    /// <returns>The formatted number, or <see cref="Binding.DoNothing"/> for a nonnumeric value.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null || !BclConversion.IsNumericType(value.GetType()) || value is not IFormattable formattable)
        {
            return Binding.DoNothing;
        }

        try
        {
            return formattable.ToString(parameter as string, culture);
        }
        catch (FormatException)
        {
            return Binding.DoNothing;
        }
    }

    /// <summary>Parses formatted text back to the requested numeric source type.</summary>
    /// <param name="value">The formatted target value.</param>
    /// <param name="targetType">The numeric binding source type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture used for parsing.</param>
    /// <returns>The parsed number, or <see cref="Binding.DoNothing"/> when parsing fails.</returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        BclConversion.IsNumericType(targetType)
        && BclConversion.TryChangeType(value, targetType, culture, out var result)
            ? result
            : Binding.DoNothing;
}

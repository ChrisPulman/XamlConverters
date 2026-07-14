// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Converts between a byte array and Base64 text.</summary>
public sealed class ByteArrayToBase64Converter : IValueConverter
{
    /// <summary>Encodes a byte array as Base64 text, or decodes Base64 text when the target is a byte array.</summary>
    /// <param name="value">The byte array or Base64 text.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns>The converted value, or <see cref="Binding.DoNothing"/> for invalid input.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        ConvertCore(value, targetType);

    /// <summary>Decodes Base64 text to bytes, or encodes bytes when the source is a string.</summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns>The converted value, or <see cref="Binding.DoNothing"/> for invalid input.</returns>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConvertCore(value, targetType);

    /// <summary>Converts between byte arrays and Base64 text.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <returns>The converted value or a WPF binding sentinel.</returns>
    private static object ConvertCore(object? value, Type targetType)
    {
        if (value is byte[] bytes)
        {
            return System.Convert.ToBase64String(bytes);
        }

        if (
            value is not string text
            || (targetType != typeof(byte[]) && targetType != typeof(object)))
        {
            return Binding.DoNothing;
        }

        try
        {
            return System.Convert.FromBase64String(text);
        }
        catch (FormatException)
        {
            return Binding.DoNothing;
        }
    }
}

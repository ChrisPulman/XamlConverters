// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Converts between a byte array and Base64 text.</summary>
public sealed class ByteArrayToBase64Converter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        ConvertCore(value, targetType);

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConvertCore(value, targetType);

    /// <summary>Converts between byte arrays and Base64 text.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <returns>The converted value or an Avalonia binding sentinel.</returns>
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
            return ConversionHelpers.DoNothing;
        }

        try
        {
            return System.Convert.FromBase64String(text);
        }
        catch (FormatException)
        {
            return ConversionHelpers.DoNothing;
        }
    }
}

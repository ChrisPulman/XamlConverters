// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Converts between a byte array and Base64 text.</summary>
public sealed class ByteArrayToBase64Converter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => ConvertCore(value, targetType);

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConvertCore(value, targetType);

    private static object ConvertCore(object? value, Type targetType)
    {
        if (value is byte[] bytes)
        {
            return System.Convert.ToBase64String(bytes);
        }

        if (value is not string text || (targetType != typeof(byte[]) && targetType != typeof(object)))
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

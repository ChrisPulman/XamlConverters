// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Converts between <see cref="Guid"/>, text, and a 16-byte array.</summary>
public sealed class GuidConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => ConvertCore(value, targetType, parameter, culture);

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConvertCore(value, targetType, parameter, culture);

    private static object ConvertCore(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Guid guid)
        {
            if (targetType == typeof(byte[]))
            {
                return guid.ToByteArray();
            }

            if (targetType == typeof(Guid))
            {
                return guid;
            }

            try
            {
                return guid.ToString(parameter as string ?? "D");
            }
            catch (FormatException)
            {
                return ConversionHelpers.DoNothing;
            }
        }

        if (value is byte[] bytes && bytes.Length == 16)
        {
            return new Guid(bytes);
        }

        return ConversionHelpers.TryConvert(value, targetType == typeof(string) ? typeof(Guid) : targetType, culture, out var converted)
            ? converted!
            : ConversionHelpers.DoNothing;
    }
}

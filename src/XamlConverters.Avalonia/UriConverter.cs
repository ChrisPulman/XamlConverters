// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Converts between <see cref="Uri"/> and its original string representation.</summary>
public sealed class UriConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => ConvertCore(value, targetType, parameter);

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConvertCore(value, targetType, parameter);

    private static object ConvertCore(object? value, Type targetType, object? parameter)
    {
        if (value is Uri uri)
        {
            return targetType == typeof(Uri) ? uri : uri.OriginalString;
        }

        if (value is not string text)
        {
            return ConversionHelpers.DoNothing;
        }

        if (targetType == typeof(string))
        {
            return text;
        }

        return Uri.TryCreate(text, GetUriKind(parameter), out var convertedUri)
            ? convertedUri
            : ConversionHelpers.DoNothing;
    }

    private static UriKind GetUriKind(object? parameter)
    {
        if (parameter is UriKind uriKind)
        {
            return uriKind;
        }

        return Enum.TryParse(parameter?.ToString(), ignoreCase: true, out UriKind parsedKind)
            ? parsedKind
            : UriKind.RelativeOrAbsolute;
    }
}

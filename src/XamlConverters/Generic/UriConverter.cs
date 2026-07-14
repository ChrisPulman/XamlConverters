// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Converts between <see cref="Uri"/> and its original string representation.</summary>
/// <remarks>
/// The converter parameter may be a <see cref="UriKind"/> or one of <c>Absolute</c>, <c>Relative</c>,
/// or <c>RelativeOrAbsolute</c>. The default is <see cref="UriKind.RelativeOrAbsolute"/>.
/// </remarks>
public sealed class UriConverter : IValueConverter
{
    /// <summary>Converts a URI to text or text to a URI according to the binding target type.</summary>
    /// <param name="value">A URI or URI string.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">An optional <see cref="UriKind"/>.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns>The converted value, or <see cref="Binding.DoNothing"/> when the URI is invalid.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        ConvertCore(value, targetType, parameter);

    /// <summary>Converts the target URI representation back to the requested source type.</summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">An optional <see cref="UriKind"/>.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns>The converted value, or <see cref="Binding.DoNothing"/> when the URI is invalid.</returns>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConvertCore(value, targetType, parameter);

    /// <summary>Converts between URI values and text.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <param name="parameter">The requested <see cref="UriKind"/>.</param>
    /// <returns>The converted value or a WPF binding sentinel.</returns>
    private static object ConvertCore(object? value, Type targetType, object? parameter)
    {
        if (value is Uri uri)
        {
            return targetType == typeof(Uri) ? uri : uri.OriginalString;
        }

        if (value is not string text)
        {
            return Binding.DoNothing;
        }

        if (targetType == typeof(string))
        {
            return text;
        }

        return Uri.TryCreate(text, GetUriKind(parameter), out var convertedUri)
            ? convertedUri
            : Binding.DoNothing;
    }

    /// <summary>Gets the requested URI kind from a converter parameter.</summary>
    /// <param name="parameter">The converter parameter.</param>
    /// <returns>The requested URI kind, or <see cref="UriKind.RelativeOrAbsolute"/> by default.</returns>
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

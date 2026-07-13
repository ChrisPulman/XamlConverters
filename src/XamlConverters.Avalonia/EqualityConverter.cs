// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Compares a bound value with the converter parameter, coercing the parameter when possible.</summary>
public sealed class EqualityConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return parameter is null;
        }

        var text = parameter?.ToString() ?? string.Empty;
        var invert = text.StartsWith("!", StringComparison.Ordinal);
        var candidate = invert ? text[1..] : parameter;
        var result = ConversionHelpers.TryConvert(candidate, value.GetType(), culture, out var converted)
            ? Equals(value, converted)
            : string.Equals(value.ToString(), candidate?.ToString(), StringComparison.OrdinalIgnoreCase);
        return invert ? !result : result;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

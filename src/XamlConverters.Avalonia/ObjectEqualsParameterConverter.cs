// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Compares a bound value with the converter parameter.</summary>
public sealed class ObjectEqualsParameterConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var text = parameter?.ToString() ?? string.Empty;
        var invert = text.StartsWith("!", StringComparison.Ordinal);
        if (invert)
        {
            text = text.Substring(1);
        }

        var result = string.Equals(value?.ToString(), text, StringComparison.OrdinalIgnoreCase);
        return invert ? !result : result;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Rounds numeric values to the number of digits supplied as parameter.</summary>
public sealed class RoundingConverter : IValueConverter
{
    /// <summary>
    /// Gets or sets a value used by the converter.
    /// </summary>
    public MidpointRounding Mode { get; set; } = MidpointRounding.ToEven;

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!ConversionHelpers.TryDecimal(value, culture, out var number))
        {
            return ConversionHelpers.UnsetValue;
        }

        var digits = int.TryParse(parameter?.ToString(), NumberStyles.Integer, culture, out var parsed) ? Math.Clamp(parsed, 0, 28) : 0;
        return ConversionHelpers.ConvertDecimal(Math.Round(number, digits, Mode), targetType, culture);
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

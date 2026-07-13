// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Rounds numeric values to the number of digits supplied as parameter.</summary>
public sealed class RoundingConverter : IValueConverter
{
    /// <summary>The maximum scale supported by <see cref="decimal"/>.</summary>
    private const int MaximumDecimalDigits = 28;

    /// <summary>Gets or sets a value used by the converter.</summary>
    public MidpointRounding Mode { get; set; } = MidpointRounding.ToEven;

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!ConversionHelpers.TryDecimal(value, culture, out var number))
        {
            return ConversionHelpers.UnsetValue;
        }

        var digits = int.TryParse(parameter?.ToString(), NumberStyles.Integer, culture, out var parsed) ? Math.Clamp(parsed, 0, MaximumDecimalDigits) : 0;
        return ConversionHelpers.ConvertDecimal(Math.Round(number, digits, Mode), targetType, culture);
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

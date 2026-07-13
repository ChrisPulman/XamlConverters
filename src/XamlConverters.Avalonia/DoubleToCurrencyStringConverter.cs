// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Formats numeric values as currency and parses currency on conversion back.</summary>
public sealed class DoubleToCurrencyStringConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        ConversionHelpers.TryDecimal(value, culture, out var amount) ? amount.ToString(parameter?.ToString() ?? "C", culture) : 0m.ToString("C", culture);

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        decimal.TryParse(value?.ToString(), NumberStyles.Currency, culture, out var amount)
            ? ConversionHelpers.ConvertDecimal(amount, targetType, culture)
            : ConversionHelpers.UnsetValue;
}

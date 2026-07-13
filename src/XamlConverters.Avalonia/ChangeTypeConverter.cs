// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Converts between compatible BCL types using culture-aware conversion semantics.</summary>
public sealed class ChangeTypeConverter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var conversionType = (parameter as Type) ?? targetType;
        return ConversionHelpers.TryConvert(value, conversionType, culture, out var result)
            ? result
            : ConversionHelpers.DoNothing;
    }

    /// <inheritdoc/>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        ConversionHelpers.TryConvert(value, targetType, culture, out var result)
            ? result
            : ConversionHelpers.DoNothing;
}

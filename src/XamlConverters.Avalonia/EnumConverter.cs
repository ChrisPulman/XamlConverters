// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Converts values to and from the enum type supplied as parameter, or the target type.</summary>
public sealed class EnumConverter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(targetType);
        var enumType = parameter as Type ?? targetType;
        return enumType.IsEnum && ConversionHelpers.TryConvert(value, enumType, culture, out var result)
            ? result
            : ConversionHelpers.UnsetValue;
    }

    /// <inheritdoc/>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        ConversionHelpers.TryConvert(value, targetType, culture, out var result) ? result : ConversionHelpers.UnsetValue;
}

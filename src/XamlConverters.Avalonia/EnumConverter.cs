// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
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
        var enumType = (parameter as Type) ?? targetType;
        return enumType.IsEnum && ConversionHelpers.TryConvert(value, enumType, culture, out var result)
            ? result
            : ConversionHelpers.UnsetValue;
    }

    /// <inheritdoc/>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        ConversionHelpers.TryConvert(value, targetType, culture, out var result) ? result : ConversionHelpers.UnsetValue;
}

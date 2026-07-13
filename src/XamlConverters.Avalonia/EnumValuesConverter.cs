// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Returns all declared values for an enum type.</summary>
public sealed class EnumValuesConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var enumType = (parameter as Type) ?? (value as Type) ?? value?.GetType();
        enumType = enumType is null ? null : Nullable.GetUnderlyingType(enumType) ?? enumType;
        return enumType?.IsEnum == true ? Enum.GetValues(enumType) : ConversionHelpers.DoNothing;
    }

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.DoNothing;
}

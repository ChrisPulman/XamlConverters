// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Returns all declared values for an enum type, suitable for an items source.
/// </summary>
public sealed class EnumValuesConverter : IValueConverter
{
    /// <summary>
    /// Gets the declared enum values.
    /// </summary>
    /// <param name="value">An enum value or enum <see cref="Type"/>.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">An optional enum <see cref="Type"/>.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns>An array of enum values, or <see cref="Binding.DoNothing"/> when no enum type is supplied.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var enumType = parameter as Type ?? value as Type ?? value?.GetType();
        enumType = enumType == null ? null : Nullable.GetUnderlyingType(enumType) ?? enumType;
        return enumType != null && enumType.IsEnum
            ? Enum.GetValues(enumType)
            : Binding.DoNothing;
    }

    /// <summary>
    /// Reverse conversion is not supported.
    /// </summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns><see cref="Binding.DoNothing"/>.</returns>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Binding.DoNothing;
}

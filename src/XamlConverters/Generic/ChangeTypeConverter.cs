// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Converts between compatible BCL types using standard conversion semantics.</summary>
/// <remarks>
/// Supply a <see cref="Type"/> as the converter parameter to override the binding target type.
/// Conversion failures return <see cref="Binding.DoNothing"/>.
/// </remarks>
public sealed class ChangeTypeConverter : IValueConverter
{
    /// <summary>Converts the source value to the target or parameter type.</summary>
    /// <param name="value">The source value.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">An optional <see cref="Type"/> that overrides <paramref name="targetType"/>.</param>
    /// <param name="culture">The culture used for conversion.</param>
    /// <returns>The converted value, or <see cref="Binding.DoNothing"/> when conversion fails.</returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == DependencyProperty.UnsetValue)
        {
            return Binding.DoNothing;
        }

        var conversionType = (parameter as Type) ?? targetType;
        return BclConversion.TryChangeType(value, conversionType, culture, out var result)
            ? result
            : Binding.DoNothing;
    }

    /// <summary>Converts a target value back to the binding source type.</summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">The converter parameter; it is not needed for reverse conversion.</param>
    /// <param name="culture">The culture used for conversion.</param>
    /// <returns>The converted value, or <see cref="Binding.DoNothing"/> when conversion fails.</returns>
    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) =>
        BclConversion.TryChangeType(value, targetType, culture, out var result)
            ? result
            : Binding.DoNothing;
}

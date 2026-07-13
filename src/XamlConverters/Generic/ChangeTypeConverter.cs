// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Converts between compatible BCL types by using enum, nullable, parsing, type-converter, and
/// <see cref="System.Convert.ChangeType(object, Type, IFormatProvider)"/> semantics.
/// </summary>
/// <remarks>
/// Supply a <see cref="Type"/> as the converter parameter to override the binding target type.
/// Conversion failures return <see cref="Binding.DoNothing"/>.
/// </remarks>
public sealed class ChangeTypeConverter : IValueConverter
{
    /// <summary>
    /// Converts the source value to the binding target type, or to the type supplied as the parameter.
    /// </summary>
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

        var conversionType = parameter as Type ?? targetType;
        return BclConversion.TryChangeType(value, conversionType, culture, out var result)
            ? result
            : Binding.DoNothing;
    }

    /// <summary>
    /// Converts a target value back to the binding source type.
    /// </summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">The converter parameter; it is not needed for reverse conversion.</param>
    /// <param name="culture">The culture used for conversion.</param>
    /// <returns>The converted value, or <see cref="Binding.DoNothing"/> when conversion fails.</returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        BclConversion.TryChangeType(value, targetType, culture, out var result)
            ? result
            : Binding.DoNothing;
}

// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Maps a Boolean value to configurable true and false values and supports reverse mapping.
/// </summary>
/// <remarks>
/// This converter is intentionally configurable rather than registered as a singleton. It can map a Boolean
/// to any BCL value that is assignable or convertible to the binding target type.
/// </remarks>
public sealed class BooleanToValueConverter : IValueConverter
{
    /// <summary>
    /// Gets or sets the value returned when the Boolean condition is <see langword="false"/>.
    /// </summary>
    public object? FalseValue { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether the Boolean condition is inverted before mapping.
    /// </summary>
    public bool Invert { get; set; }

    /// <summary>
    /// Gets or sets the value returned when the Boolean condition is <see langword="true"/>.
    /// </summary>
    public object? TrueValue { get; set; } = true;

    /// <summary>
    /// Maps a Boolean source value to <see cref="TrueValue"/> or <see cref="FalseValue"/>.
    /// </summary>
    /// <param name="value">The Boolean source value.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture used when coercing the configured value.</param>
    /// <returns>The configured and, when needed, coerced result.</returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (targetType == null)
        {
            throw new ArgumentNullException(nameof(targetType));
        }

        if (value is not bool condition)
        {
            return Binding.DoNothing;
        }

        if (Invert)
        {
            condition = !condition;
        }

        var selectedValue = condition ? TrueValue : FalseValue;
        if (targetType == typeof(object) || selectedValue == null || targetType.IsInstanceOfType(selectedValue))
        {
            return selectedValue;
        }

        return BclConversion.TryChangeType(selectedValue, targetType, culture, out var converted)
            ? converted
            : Binding.DoNothing;
    }

    /// <summary>
    /// Maps a target value back to a Boolean by comparing it with <see cref="TrueValue"/> and <see cref="FalseValue"/>.
    /// </summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns>The corresponding Boolean, or <see cref="Binding.DoNothing"/> when no configured value matches.</returns>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        bool? result = ValuesEqual(value, TrueValue)
            ? true
            : ValuesEqual(value, FalseValue) ? false : null;

        if (!result.HasValue)
        {
            return Binding.DoNothing;
        }

        return Invert ? !result.Value : result.Value;
    }

    private static bool ValuesEqual(object? left, object? right) =>
        Equals(left, right)
        || (left != null
            && right != null
            && string.Equals(left.ToString(), right.ToString(), StringComparison.Ordinal));
}

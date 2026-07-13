// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Returns the first item in an enumerable, or the converter parameter when the sequence is empty.
/// </summary>
public sealed class CollectionFirstOrDefaultConverter : IValueConverter
{
    /// <summary>
    /// Selects the first collection item.
    /// </summary>
    /// <param name="value">The source enumerable.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">The fallback returned for a null or empty sequence.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns>The first item, the configured fallback, or <see cref="Binding.DoNothing"/> for a non-enumerable source.</returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null || value == DependencyProperty.UnsetValue)
        {
            return parameter;
        }

        if (value is not IEnumerable enumerable)
        {
            return Binding.DoNothing;
        }

        var enumerator = enumerable.GetEnumerator();
        try
        {
            return enumerator.MoveNext() ? enumerator.Current : parameter;
        }
        finally
        {
            (enumerator as IDisposable)?.Dispose();
        }
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

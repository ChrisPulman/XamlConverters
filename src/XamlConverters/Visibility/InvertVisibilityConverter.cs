// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Inverts Visibility between Visible and Collapsed.</summary>
public class InvertVisibilityConverter : IValueConverter
{
    /// <summary>Converts a Visibility.</summary>
    /// <param name="value">Used to determine the current <c>Visibility</c> state.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <param name="parameter">An optional request to use <see cref="Visibility.Hidden"/>.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>Visibility of Visible or Collapsed.</returns>
    /// <exception cref="InvalidOperationException">An Exception.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not null && targetType == typeof(Visibility))
        {
            bool? useHidden = parameter?.ToString()!.IndexOf("hidden", StringComparison.OrdinalIgnoreCase) >= 0;
            var vis = (Visibility)value;
            if (vis == Visibility.Collapsed || vis == Visibility.Hidden)
            {
                return Visibility.Visible;
            }

            return useHidden == true ? Visibility.Hidden : Visibility.Collapsed;
        }

        throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
    }

    /// <summary>Not enabled.</summary>
    /// <param name="value">The target visibility value.</param>
    /// <param name="targetType">The requested source type.</param>
    /// <param name="parameter">An optional request to use <see cref="Visibility.Hidden"/>.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>Invalid call - one way only.</returns>
    /// <exception cref="InvalidOperationException">An Invalid Operation Exception.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not null && targetType == typeof(Visibility))
        {
            var useHidden = parameter?.ToString()?.IndexOf("hidden", StringComparison.OrdinalIgnoreCase) >= 0;
            var vis = (Visibility)value;
            if (vis == Visibility.Collapsed || vis == Visibility.Hidden)
            {
                return Visibility.Visible;
            }

            return useHidden ? Visibility.Hidden : Visibility.Collapsed;
        }

        throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
    }
}

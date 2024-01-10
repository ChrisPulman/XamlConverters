// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Inverts Visibility between Visible and Collapsed.
/// </summary>
public class InvertVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Converts a Visibility.
    /// </summary>
    /// <param name="value">Used to determine the current <c>Visibility</c> state.</param>
    /// <param name="targetType">The parameter is not used.</param>
    /// <param name="parameter">The parameter is not used.</param>
    /// <param name="culture">The parameter is not used.</param>
    /// <returns>Visibility of Visible or Collapsed.</returns>
    /// <exception cref="InvalidOperationException">An Exception.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null && targetType == typeof(Visibility))
        {
            bool? useHidden = parameter?.ToString()!.IndexOf("hidden", StringComparison.OrdinalIgnoreCase) >= 0;
            var vis = (Visibility)value;
            return vis == Visibility.Collapsed || vis == Visibility.Hidden ?
                Visibility.Visible :
                useHidden == true ?
                Visibility.Hidden :
                Visibility.Collapsed;
        }

        throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
    }

    /// <summary>
    /// Not enabled.
    /// </summary>
    /// <param name="value">The parameter is not used.</param>
    /// <param name="targetType">The parameter is not used.</param>
    /// <param name="parameter">The parameter is not used.</param>
    /// <param name="culture">The parameter is not used.</param>
    /// <returns>Invalid call - one way only.</returns>
    /// <exception cref="InvalidOperationException">An Invalid Operation Exception.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null && targetType == typeof(Visibility))
        {
            var useHidden = parameter?.ToString()?.IndexOf("hidden", StringComparison.OrdinalIgnoreCase) >= 0;
            var vis = (Visibility)value;
            return vis == Visibility.Collapsed || vis == Visibility.Hidden ?
                Visibility.Visible :
                useHidden ?
                Visibility.Hidden :
                Visibility.Collapsed;
        }

        throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
    }
}

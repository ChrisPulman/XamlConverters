// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// A simple converter for determining if the specified value is null.
/// </summary>
public class NullToBoolConverter : IValueConverter
{
    /// <summary>
    /// Gets an instance of <see cref="NullToBoolConverter"/> that returns true if the specified
    /// value is null.
    /// </summary>
    public static NullToBoolConverter IsNull { get; } = new() { ReturnTrueIfNull = true };

    /// <summary>
    /// Gets an instance of <see cref="NullToBoolConverter"/> that returns true if the specified
    /// value is not null.
    /// </summary>
    public static NullToBoolConverter NotNull { get; } = new() { ReturnTrueIfNull = false };

    /// <summary>
    /// Gets or sets a value indicating whether [return true if null].
    /// </summary>
    /// <value><c>true</c> if [return true if null]; otherwise, <c>false</c>.</value>
    public bool ReturnTrueIfNull { get; set; }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ReturnTrueIfNull ? value == null : value != null;

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
}

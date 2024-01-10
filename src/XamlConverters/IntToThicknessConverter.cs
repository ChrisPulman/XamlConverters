// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Converts an integer into a thickness value, building on top of the converter parameter if specified.
/// </summary>
public class IntToThicknessConverter : IValueConverter
{
    /// <summary>
    /// Gets an instance of <see cref="IntToThicknessConverter"/> that only sets the bottom side of
    /// the thickness.
    /// </summary>
    public static IntToThicknessConverter BottomOnly { get; } = new() { Bottom = true };

    /// <summary>
    /// Gets an instance of <see cref="IntToThicknessConverter"/> that only sets the left side of the thickness.
    /// </summary>
    public static IntToThicknessConverter LeftOnly { get; } = new() { Left = true };

    /// <summary>
    /// Gets an instance of <see cref="IntToThicknessConverter"/> that only sets the right side of the thickness.
    /// </summary>
    public static IntToThicknessConverter RightOnly { get; } = new() { Right = true };

    /// <summary>
    /// Gets an instance of <see cref="IntToThicknessConverter"/> that only sets the top side of the thickness.
    /// </summary>
    public static IntToThicknessConverter TopOnly { get; } = new() { Top = true };

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="IntToThicknessConverter"/> is bottom.
    /// </summary>
    /// <value><c>true</c> if bottom; otherwise, <c>false</c>.</value>
    public bool Bottom { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="IntToThicknessConverter"/> is left.
    /// </summary>
    /// <value><c>true</c> if left; otherwise, <c>false</c>.</value>
    public bool Left { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="IntToThicknessConverter"/> is right.
    /// </summary>
    /// <value><c>true</c> if right; otherwise, <c>false</c>.</value>
    public bool Right { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="IntToThicknessConverter"/> is top.
    /// </summary>
    /// <value><c>true</c> if top; otherwise, <c>false</c>.</value>
    public bool Top { get; set; }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var thickness = parameter is Thickness thickness1 ? thickness1 : default;
        var input = System.Convert.ToInt32(value);

        if (Left)
        {
            thickness.Left = input;
        }

        if (Top)
        {
            thickness.Top = input;
        }

        if (Right)
        {
            thickness.Right = input;
        }

        if (Bottom)
        {
            thickness.Bottom = input;
        }

        return thickness;
    }

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

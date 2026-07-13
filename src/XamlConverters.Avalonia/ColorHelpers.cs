// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Avalonia.Media;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Provides color conversion and formatting helpers.</summary>
internal static class ColorHelpers
{
    /// <summary>Attempts to extract an Avalonia color from a color or solid-color brush value.</summary>
    /// <param name="value">The value to inspect.</param>
    /// <param name="color">When this method returns, the extracted color.</param>
    /// <returns><see langword="true"/> when a color was extracted; otherwise, <see langword="false"/>.</returns>
    public static bool TryColor(object? value, out Color color)
    {
        if (value is Color direct)
        {
            color = direct;
            return true;
        }

        if (value is SolidColorBrush brush)
        {
            color = brush.Color;
            return true;
        }

        try
        {
            color = Color.Parse(value?.ToString() ?? string.Empty);
            return true;
        }
        catch (FormatException)
        {
            color = default;
            return false;
        }
    }

    /// <summary>Formats a color as an RGB or ARGB hexadecimal string.</summary>
    /// <param name="color">The color to format.</param>
    /// <returns>The hexadecimal representation of <paramref name="color"/>.</returns>
    public static string ToHex(Color color) => color.A == byte.MaxValue
        ? $"#{color.R:X2}{color.G:X2}{color.B:X2}"
        : $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
}

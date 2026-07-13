// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

internal static class ColorHelpers
{
    /// <summary>
    /// Provides converter behavior.
    /// </summary>
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

    /// <summary>
    /// Provides converter behavior.
    /// </summary>
    public static string ToHex(Color color) => color.A == byte.MaxValue
        ? $"#{color.R:X2}{color.G:X2}{color.B:X2}"
        : $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
}

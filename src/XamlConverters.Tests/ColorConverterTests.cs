// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Media;

namespace CP.Xaml.Converters.Tests;

/// <summary>Tests WPF color and brush conversions.</summary>
public class ColorConverterTests
{
    /// <summary>The invariant culture used by converter tests.</summary>
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

    /// <summary>Converts hexadecimal RGB strings to colors and back.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsHexStringsToColorsAndBack()
    {
        var converter = new HexStringToColorConverter();
        var color = (Color)converter.Convert("#123456", typeof(Color), null!, InvariantCulture)!;
        var text = converter.ConvertBack(color, typeof(string), null!, InvariantCulture);

        await Assert.That(color).IsEqualTo(Color.FromRgb(0x12, 0x34, 0x56));
        await Assert.That(text).IsEqualTo("123456");
    }

    /// <summary>Converts hexadecimal RGB strings to solid brushes and back.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsHexStringsToBrushesAndBack()
    {
        var converter = new HexStringToSolidColorBrushConverter();
        var brush = (SolidColorBrush)converter.Convert("ABCDEF", typeof(SolidColorBrush), null!, InvariantCulture);
        var text = converter.ConvertBack(brush, typeof(string), null!, InvariantCulture);

        await Assert.That(brush.Color).IsEqualTo(Color.FromRgb(0xAB, 0xCD, 0xEF));
        await Assert.That(text).IsEqualTo("ABCDEF");
    }

    /// <summary>Converts solid brushes to colors.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsSolidBrushesToColors()
    {
        var brush = new SolidColorBrush(Colors.CornflowerBlue);
        var color = new BrushToColorConverter().Convert(brush, typeof(Color), null!, InvariantCulture);

        await Assert.That(color).IsEqualTo(Colors.CornflowerBlue);
    }

    /// <summary>Converts RGB strings to brushes and colors back to values.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsColorValuesAndBrushesInBothDirections()
    {
        var converter = new ColorToBrushConverter();
        var brush = (SolidColorBrush)converter.Convert("#102030", typeof(SolidColorBrush), null!, InvariantCulture);
        var color = converter.ConvertBack(brush, typeof(Color), null!, InvariantCulture);

        await Assert.That(brush.Color).IsEqualTo(Color.FromRgb(0x10, 0x20, 0x30));
        await Assert.That(color).IsEqualTo(Color.FromRgb(0x10, 0x20, 0x30));
    }
}

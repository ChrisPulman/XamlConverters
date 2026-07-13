// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>Tests uniform thickness converter behavior.</summary>
public class ThicknessUniformConverterTests
{
    /// <summary>Alls the sides.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task AllSides()
    {
        var c = new ThicknessUniformConverter();
        var t = (Thickness)c.Convert(TestValues.UniformThickness, typeof(Thickness), null!, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(t).IsEqualTo(new Thickness(TestValues.UniformThickness));
    }

    /// <summary>Horizontals the only.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task HorizontalOnly()
    {
        var c = new ThicknessUniformConverter();
        var t = (Thickness)c.Convert(TestValues.HorizontalThickness, typeof(Thickness), "H", System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(t.Left).IsEqualTo(TestValues.HorizontalThickness);
        await Assert.That(t.Right).IsEqualTo(TestValues.HorizontalThickness);
        await Assert.That(t.Top).IsEqualTo(0d);
        await Assert.That(t.Bottom).IsEqualTo(0d);
    }
}

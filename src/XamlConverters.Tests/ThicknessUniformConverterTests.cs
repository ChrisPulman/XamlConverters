// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>
/// ThicknessUniformConverterTests.
/// </summary>
public class ThicknessUniformConverterTests
{
    /// <summary>
    /// Alls the sides.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task AllSides()
    {
        var c = new ThicknessUniformConverter();
        var t = (Thickness)c.Convert(5, typeof(Thickness), null!, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(t).IsEqualTo(new Thickness(5));
    }

    /// <summary>
    /// Horizontals the only.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task HorizontalOnly()
    {
        var c = new ThicknessUniformConverter();
        var t = (Thickness)c.Convert(3, typeof(Thickness), "H", System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(t.Left).IsEqualTo(3d);
        await Assert.That(t.Right).IsEqualTo(3d);
        await Assert.That(t.Top).IsEqualTo(0d);
        await Assert.That(t.Bottom).IsEqualTo(0d);
    }
}

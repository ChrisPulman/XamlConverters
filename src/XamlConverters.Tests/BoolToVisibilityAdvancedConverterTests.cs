// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>Tests advanced Boolean-to-visibility conversion.</summary>
public class BoolToVisibilityAdvancedConverterTests
{
    /// <summary>Trues the visible.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task TrueVisible()
    {
        var c = new BoolToVisibilityAdvancedConverter();
        var result = c.Convert(true, typeof(Visibility), null!, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo(Visibility.Visible);
    }

    /// <summary>Falses the collapsed.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task FalseCollapsed()
    {
        var c = new BoolToVisibilityAdvancedConverter();
        var result = c.Convert(false, typeof(Visibility), null!, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo(Visibility.Collapsed);
    }

    /// <summary>Falses the hidden when h.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task FalseHiddenWhenH()
    {
        var c = new BoolToVisibilityAdvancedConverter();
        var result = c.Convert(false, typeof(Visibility), "H", System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo(Visibility.Hidden);
    }
}

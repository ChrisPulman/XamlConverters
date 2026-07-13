// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>
/// MultiBooleanConvertersTests.
/// </summary>
public class MultiBooleanConvertersTests
{
    /// <summary>
    /// Ands all true returns true.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task And_AllTrue_ReturnsTrue()
    {
        var c = new MultiBooleanAndConverter();
        var result = c.Convert(new object[] { true, true, true }, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That((bool)result).IsTrue();
    }

    /// <summary>
    /// Ands the one false returns false.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task And_OneFalse_ReturnsFalse()
    {
        var c = new MultiBooleanAndConverter();
        var result = c.Convert(new object[] { true, false, true }, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That((bool)result).IsFalse();
    }

    /// <summary>
    /// Ors any true returns true.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task Or_AnyTrue_ReturnsTrue()
    {
        var c = new MultiBooleanOrConverter();
        var result = c.Convert(new object[] { false, false, true }, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That((bool)result).IsTrue();
    }

    /// <summary>
    /// Ors all false returns false.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task Or_AllFalse_ReturnsFalse()
    {
        var c = new MultiBooleanOrConverter();
        var result = c.Convert(new object[] { false, false }, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That((bool)result).IsFalse();
    }

    /// <summary>
    /// Xors the odd true returns true.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task Xor_OddTrue_ReturnsTrue()
    {
        var c = new BooleanXorConverter();
        var result = c.Convert(new object[] { true, false, true }, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That((bool)result).IsFalse(); // two trues -> even -> false
    }

    /// <summary>
    /// Xors the single true returns true.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task Xor_SingleTrue_ReturnsTrue()
    {
        var c = new BooleanXorConverter();
        var result = c.Convert(new object[] { true, false, false }, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That((bool)result).IsTrue();
    }
}

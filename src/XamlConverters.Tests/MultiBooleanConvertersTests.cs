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
    [Fact]
    public void And_AllTrue_ReturnsTrue()
    {
        var c = new MultiBooleanAndConverter();
        var result = c.Convert(new object[] { true, true, true }, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        Assert.True((bool)result);
    }

    /// <summary>
    /// Ands the one false returns false.
    /// </summary>
    [Fact]
    public void And_OneFalse_ReturnsFalse()
    {
        var c = new MultiBooleanAndConverter();
        var result = c.Convert(new object[] { true, false, true }, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        Assert.False((bool)result);
    }

    /// <summary>
    /// Ors any true returns true.
    /// </summary>
    [Fact]
    public void Or_AnyTrue_ReturnsTrue()
    {
        var c = new MultiBooleanOrConverter();
        var result = c.Convert(new object[] { false, false, true }, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        Assert.True((bool)result);
    }

    /// <summary>
    /// Ors all false returns false.
    /// </summary>
    [Fact]
    public void Or_AllFalse_ReturnsFalse()
    {
        var c = new MultiBooleanOrConverter();
        var result = c.Convert(new object[] { false, false }, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        Assert.False((bool)result);
    }

    /// <summary>
    /// Xors the odd true returns true.
    /// </summary>
    [Fact]
    public void Xor_OddTrue_ReturnsTrue()
    {
        var c = new BooleanXorConverter();
        var result = c.Convert(new object[] { true, false, true }, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        Assert.False((bool)result); // two trues -> even -> false
    }

    /// <summary>
    /// Xors the single true returns true.
    /// </summary>
    [Fact]
    public void Xor_SingleTrue_ReturnsTrue()
    {
        var c = new BooleanXorConverter();
        var result = c.Convert(new object[] { true, false, false }, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        Assert.True((bool)result);
    }
}

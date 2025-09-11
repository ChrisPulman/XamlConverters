// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>
/// StringNullOrEmptyToBoolConverterTests.
/// </summary>
public class StringNullOrEmptyToBoolConverterTests
{
    /// <summary>
    /// Detectses the null or empty.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData("abc", false)]
    public void DetectsNullOrEmpty(string? input, bool expected)
    {
        var c = new StringNullOrEmptyToBoolConverter();
        var result = c.Convert(input!, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Invertses the specified input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("abc", true)]
    public void Inverts(string? input, bool expected)
    {
        var c = new StringNullOrEmptyToBoolConverter();
        var result = c.Convert(input!, typeof(bool), "invert", System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(expected, result);
    }
}

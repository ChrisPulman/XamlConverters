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
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    [Arguments(null, true)]
    [Arguments("", true)]
    [Arguments("abc", false)]
    public async Task DetectsNullOrEmpty(string? input, bool expected)
    {
        var c = new StringNullOrEmptyToBoolConverter();
        var result = c.Convert(input!, typeof(bool), null!, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo(expected);
    }

    /// <summary>
    /// Invertses the specified input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    [Arguments(null, false)]
    [Arguments("", false)]
    [Arguments("abc", true)]
    public async Task Inverts(string? input, bool expected)
    {
        var c = new StringNullOrEmptyToBoolConverter();
        var result = c.Convert(input!, typeof(bool), "invert", System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo(expected);
    }
}

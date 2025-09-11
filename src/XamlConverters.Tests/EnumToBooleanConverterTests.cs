// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>
/// EnumToBooleanConverterTests.
/// </summary>
public class EnumToBooleanConverterTests
{
    /// <summary>
    /// Enums the matches.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <param name="param">The parameter.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    [Theory]
    [InlineData(TestState.One, "One", true)]
    [InlineData(TestState.One, "Two", false)]
    public void EnumMatches(TestState state, string param, bool expected)
    {
        var c = new EnumToBooleanConverter();
        var result = c.Convert(state, typeof(bool), param, System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(expected, result);
    }
}

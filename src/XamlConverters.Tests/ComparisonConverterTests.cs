// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>
/// ComparisonConverterTests.
/// </summary>
public class ComparisonConverterTests
{
    /// <summary>
    /// Compareses the values.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="param">The parameter.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    [Theory]
    [InlineData(5, ">3", true)]
    [InlineData(3, ">3", false)]
    [InlineData(3, ">=3", true)]
    [InlineData(2, "<3", true)]
    [InlineData(2, "!=3", true)]
    [InlineData(3, "!=3", false)]
    [InlineData(3, "!>=3", false)] // inverted of >=3
    public void ComparesValues(object value, string param, bool expected)
    {
        var c = new ComparisonConverter();
        var result = c.Convert(value, typeof(bool), param, System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(expected, result);
    }
}

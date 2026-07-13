// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>Tests comparison converter behavior.</summary>
public class ComparisonConverterTests
{
    /// <summary>Compareses the values.</summary>
    /// <param name="value">The value.</param>
    /// <param name="param">The parameter.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    [Arguments(5, ">3", true)]
    [Arguments(3, ">3", false)]
    [Arguments(3, ">=3", true)]
    [Arguments(2, "<3", true)]
    [Arguments(2, "!=3", true)]
    [Arguments(3, "!=3", false)]
    [Arguments(3, "!>=3", false)] // inverted of >=3
    public async Task ComparesValues(object value, string param, bool expected)
    {
        var c = new ComparisonConverter();
        var result = c.Convert(value, typeof(bool), param, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo(expected);
    }
}

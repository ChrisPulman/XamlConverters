// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>Tests enum-to-Boolean conversion.</summary>
public class EnumToBooleanConverterTests
{
    /// <summary>Enums the matches.</summary>
    /// <param name="state">The state.</param>
    /// <param name="param">The parameter.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    [Arguments(TestState.One, "One", true)]
    [Arguments(TestState.One, "Two", false)]
    public async Task EnumMatches(TestState state, string param, bool expected)
    {
        var c = new EnumToBooleanConverter();
        var result = c.Convert(state, typeof(bool), param, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo(expected);
    }
}

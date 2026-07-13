// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>Tests percentage converter behavior.</summary>
public class PercentageConverterTests
{
    /// <summary>Applieses the percentage.</summary>
    /// <param name="input">The input.</param>
    /// <param name="param">The parameter.</param>
    /// <param name="expected">The expected.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    [Arguments(200d, "50%", 100d)]
    [Arguments(10d, "0.5", 5d)]
    public async Task AppliesPercentage(double input, string param, double expected)
    {
        var c = new PercentageConverter();
        var result = c.Convert(input, typeof(double), param, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That((double)result).IsEqualTo(expected).Within(TestValues.FloatingPointTolerance);
    }
}

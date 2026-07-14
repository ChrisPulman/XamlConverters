// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Windows.Data;

namespace CP.Xaml.Converters.Tests;

/// <summary>Tests arithmetic converter operations.</summary>
public class ArithmeticConverterTests
{
    /// <summary>Performses the arithmetic.</summary>
    /// <param name="input">The input.</param>
    /// <param name="param">The parameter.</param>
    /// <param name="expected">The expected.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    [Arguments(10, "+5", 15D)]
    [Arguments(10, "- 3", 7D)]
    [Arguments(10, "*2", 20D)]
    [Arguments(10, "/2", 5D)]
    public async Task PerformsArithmetic(int input, string param, double expected)
    {
        var c = new ArithmeticConverter();
        var result = ((IValueConverter)c).Convert(
            input,
            typeof(double),
            param,
            System.Globalization.CultureInfo.InvariantCulture);
        await Assert
            .That((double)result)
            .IsEqualTo(expected)
            .Within(TestValues.FloatingPointTolerance);
    }
}

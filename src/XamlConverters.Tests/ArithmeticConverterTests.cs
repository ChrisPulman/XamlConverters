// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows.Data;

namespace CP.Xaml.Converters.Tests;

/// <summary>
/// ArithmeticConverterTests.
/// </summary>
public class ArithmeticConverterTests
{
    /// <summary>
    /// Performses the arithmetic.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="param">The parameter.</param>
    /// <param name="expected">The expected.</param>
    [Theory]
    [InlineData(10, "+5", 15)]
    [InlineData(10, "- 3", 7)]
    [InlineData(10, "*2", 20)]
    [InlineData(10, "/2", 5)]
    public void PerformsArithmetic(int input, string param, double expected)
    {
        var c = new ArithmeticConverter();
        var result = ((IValueConverter)c).Convert(input, typeof(double), param, System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(expected, (double)result, 5);
    }
}

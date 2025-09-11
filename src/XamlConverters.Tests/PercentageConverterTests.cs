// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>
/// PercentageConverterTests.
/// </summary>
public class PercentageConverterTests
{
    /// <summary>
    /// Applieses the percentage.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="param">The parameter.</param>
    /// <param name="expected">The expected.</param>
    [Theory]
    [InlineData(200, "50%", 100)]
    [InlineData(10, "0.5", 5)]
    public void AppliesPercentage(double input, string param, double expected)
    {
        var c = new PercentageConverter();
        var result = c.Convert(input, typeof(double), param, System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(expected, (double)result, 5);
    }
}

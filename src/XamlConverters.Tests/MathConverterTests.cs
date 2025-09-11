// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>
/// MathConverterTests.
/// </summary>
public class MathConverterTests
{
    /// <summary>
    /// Evaluateses the expression.
    /// </summary>
    [Fact]
    public void EvaluatesExpression()
    {
        var c = new MathConverter();
        var result = c.Convert(new object[] { 5, 3 }, typeof(double), "{0}+{1}*2", System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(11d, (double)result, 5);
    }
}

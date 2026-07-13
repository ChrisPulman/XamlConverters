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
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task EvaluatesExpression()
    {
        var c = new MathConverter();
        var result = c.Convert(new object[] { 5, 3 }, typeof(double), "{0}+{1}*2", System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That((double)result).IsEqualTo(11d).Within(0.00001d);
    }
}

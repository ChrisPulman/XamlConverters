// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>Tests math expression converter behavior.</summary>
public class MathConverterTests
{
    /// <summary>Evaluateses the expression.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task EvaluatesExpression()
    {
        var c = new MathConverter();
        var result = c.Convert(
            [TestValues.MathLeftOperand, TestValues.MathRightOperand],
            typeof(double),
            "{0}+{1}*2",
            System.Globalization.CultureInfo.InvariantCulture);
        await Assert
            .That((double)result)
            .IsEqualTo(TestValues.MathExpectedResult)
            .Within(TestValues.FloatingPointTolerance);
    }
}

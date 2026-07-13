// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>
/// BoolToVisibilityConverterTests.
/// </summary>
public class BoolToVisibilityConverterTests
{
    /// <summary>
    /// Convertses the bool to visibility.
    /// </summary>
    /// <param name="input">if set to <c>true</c> [input].</param>
    /// <param name="expected">The expected.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    [Arguments(true, Visibility.Visible)]
    [Arguments(false, Visibility.Collapsed)]
    public async Task ConvertsBoolToVisibility(bool input, Visibility expected)
    {
        var c = new BoolToVisibilityConverter();
        var result = c.Convert(input, typeof(Visibility), null!, System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo(expected);
    }

    /// <summary>
    /// Convertses the bool to visibility inverted.
    /// </summary>
    /// <param name="input">if set to <c>true</c> [input].</param>
    /// <param name="expected">The expected.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    [Arguments(true, Visibility.Collapsed)]
    [Arguments(false, Visibility.Visible)]
    public async Task ConvertsBoolToVisibility_Inverted(bool input, Visibility expected)
    {
        var c = new BoolToVisibilityConverter();
        var result = c.Convert(input, typeof(Visibility), "reverse", System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo(expected);
    }
}

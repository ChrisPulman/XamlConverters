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
    [Theory]
    [InlineData(true, Visibility.Visible)]
    [InlineData(false, Visibility.Collapsed)]
    public void ConvertsBoolToVisibility(bool input, Visibility expected)
    {
        var c = new BoolToVisibilityConverter();
        var result = c.Convert(input, typeof(Visibility), null!, System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Convertses the bool to visibility inverted.
    /// </summary>
    /// <param name="input">if set to <c>true</c> [input].</param>
    /// <param name="expected">The expected.</param>
    [Theory]
    [InlineData(true, Visibility.Collapsed)]
    [InlineData(false, Visibility.Visible)]
    public void ConvertsBoolToVisibility_Inverted(bool input, Visibility expected)
    {
        var c = new BoolToVisibilityConverter();
        var result = c.Convert(input, typeof(Visibility), "reverse", System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(expected, result);
    }
}

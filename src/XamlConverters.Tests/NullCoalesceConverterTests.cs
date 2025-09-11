// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>
/// NullCoalesceConverterTests.
/// </summary>
public class NullCoalesceConverterTests
{
    /// <summary>
    /// Returnses the parameter if null.
    /// </summary>
    [Fact]
    public void ReturnsParameterIfNull()
    {
        var c = new NullCoalesceConverter();
        var result = c.Convert(null!, typeof(string), "fallback", System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal("fallback", result);
    }

    /// <summary>
    /// Returnses the value if not null.
    /// </summary>
    [Fact]
    public void ReturnsValueIfNotNull()
    {
        var c = new NullCoalesceConverter();
        var result = c.Convert("value", typeof(string), "fallback", System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal("value", result);
    }
}

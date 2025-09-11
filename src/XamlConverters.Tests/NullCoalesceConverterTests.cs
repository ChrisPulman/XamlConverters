// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

public class NullCoalesceConverterTests
{
    [Fact]
    public void ReturnsParameterIfNull()
    {
        var c = new NullCoalesceConverter();
        var result = c.Convert(null!, typeof(string), "fallback", System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal("fallback", result);
    }

    [Fact]
    public void ReturnsValueIfNotNull()
    {
        var c = new NullCoalesceConverter();
        var result = c.Convert("value", typeof(string), "fallback", System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal("value", result);
    }
}

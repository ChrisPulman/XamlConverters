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
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ReturnsParameterIfNull()
    {
        var c = new NullCoalesceConverter();
        var result = c.Convert(null!, typeof(string), "fallback", System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo("fallback");
    }

    /// <summary>
    /// Returnses the value if not null.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ReturnsValueIfNotNull()
    {
        var c = new NullCoalesceConverter();
        var result = c.Convert("value", typeof(string), "fallback", System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo("value");
    }
}

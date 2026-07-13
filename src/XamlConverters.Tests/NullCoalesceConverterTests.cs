// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>Tests null-coalescing converter behavior.</summary>
public class NullCoalesceConverterTests
{
    /// <summary>The fallback value used by null-coalescing tests.</summary>
    private const string FallbackValue = "fallback";

    /// <summary>Returnses the parameter if null.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ReturnsParameterIfNull()
    {
        var c = new NullCoalesceConverter();
        var result = c.Convert(
            null!,
            typeof(string),
            FallbackValue,
            System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo(FallbackValue);
    }

    /// <summary>Returnses the value if not null.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ReturnsValueIfNotNull()
    {
        var c = new NullCoalesceConverter();
        var result = c.Convert(
            "value",
            typeof(string),
            FallbackValue,
            System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo("value");
    }
}

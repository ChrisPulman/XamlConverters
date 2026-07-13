// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

namespace CP.Xaml.Converters.Tests;

/// <summary>
/// Tests common WPF visibility conversions.
/// </summary>
public class VisibilityConverterTests
{
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

    /// <summary>
    /// Inverts visible, collapsed, and hidden values.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task InvertsVisibilityValues()
    {
        var converter = new InvertVisibilityConverter();

        await Assert.That(converter.Convert(Visibility.Visible, typeof(Visibility), null!, InvariantCulture)).IsEqualTo(Visibility.Collapsed);
        await Assert.That(converter.Convert(Visibility.Visible, typeof(Visibility), "hidden", InvariantCulture)).IsEqualTo(Visibility.Hidden);
        await Assert.That(converter.Convert(Visibility.Collapsed, typeof(Visibility), null!, InvariantCulture)).IsEqualTo(Visibility.Visible);
    }

    /// <summary>
    /// Maps empty and populated strings to visibility values.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsStringPresenceToVisibility()
    {
        var converter = new StringNullOrEmptyToVisibilityConverter();

        await Assert.That(converter.Convert(string.Empty, typeof(Visibility), null!, InvariantCulture)).IsEqualTo(Visibility.Collapsed);
        await Assert.That(converter.Convert("value", typeof(Visibility), null!, InvariantCulture)).IsEqualTo(Visibility.Visible);
        await Assert.That(converter.Convert(string.Empty, typeof(Visibility), "hidden", InvariantCulture)).IsEqualTo(Visibility.Hidden);
        await Assert.That(converter.Convert(string.Empty, typeof(Visibility), "invert", InvariantCulture)).IsEqualTo(Visibility.Visible);
    }

    /// <summary>
    /// Maps zero and non-zero values to visibility.
    /// </summary>
    /// <param name="input">The numeric input.</param>
    /// <param name="expected">The expected visibility.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    [Arguments(0, Visibility.Visible)]
    [Arguments(1, Visibility.Collapsed)]
    [Arguments(-1, Visibility.Collapsed)]
    public async Task ConvertsZeroToVisibility(int input, Visibility expected)
    {
        var result = new ZeroToVisibilityConverter().Convert(input, typeof(Visibility), null!, InvariantCulture);

        await Assert.That(result).IsEqualTo(expected);
    }

    /// <summary>
    /// Maps nullability to visibility with optional reversal.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsNullabilityToVisibility()
    {
        var converter = new ValueNotNullToVisibilityConverter();

        await Assert.That(converter.Convert(new object(), typeof(Visibility), null!, InvariantCulture)).IsEqualTo(Visibility.Visible);
        await Assert.That(converter.Convert(null!, typeof(Visibility), null!, InvariantCulture)).IsEqualTo(Visibility.Collapsed);
        await Assert.That(converter.Convert(null!, typeof(Visibility), "reverse", InvariantCulture)).IsEqualTo(Visibility.Visible);
    }
}

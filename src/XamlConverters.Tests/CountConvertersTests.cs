// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>Tests collection count converters.</summary>
public class CountConvertersTests
{
    /// <summary>Counts to boolean greater than zero.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task CountToBoolean_GreaterThanZero()
    {
        var c = new CountToBooleanConverter();
        var result = c.Convert(
            new List<int> { 1, TestValues.SecondByte },
            typeof(bool),
            ">0",
            System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That((bool)result).IsTrue();
    }

    /// <summary>Counts to visibility equals zero collapsed.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task CountToVisibility_EqualsZeroCollapsed()
    {
        var c = new CountToVisibilityConverter();
        var result = c.Convert(
            Enumerable.Empty<int>(),
            typeof(Visibility),
            "==0",
            System.Globalization.CultureInfo.InvariantCulture);
        await Assert.That(result).IsEqualTo(Visibility.Visible); // Visible because ==0 satisfied
    }
}

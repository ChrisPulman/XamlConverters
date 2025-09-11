// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

public class CountConvertersTests
{
    [Fact]
    public void CountToBoolean_GreaterThanZero()
    {
        var c = new CountToBooleanConverter();
        var result = c.Convert(new List<int> { 1, 2 }, typeof(bool), ">0", System.Globalization.CultureInfo.InvariantCulture);
        Assert.True((bool)result);
    }

    [Fact]
    public void CountToVisibility_EqualsZeroCollapsed()
    {
        var c = new CountToVisibilityConverter();
        var result = c.Convert(Enumerable.Empty<int>(), typeof(Visibility), "==0", System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(Visibility.Visible, result); // Visible because ==0 satisfied
    }
}

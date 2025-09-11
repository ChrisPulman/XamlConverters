// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

public class BoolToVisibilityAdvancedConverterTests
{
    [Fact]
    public void TrueVisible()
    {
        var c = new BoolToVisibilityAdvancedConverter();
        var result = c.Convert(true, typeof(Visibility), null!, System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(Visibility.Visible, result);
    }

    [Fact]
    public void FalseCollapsed()
    {
        var c = new BoolToVisibilityAdvancedConverter();
        var result = c.Convert(false, typeof(Visibility), null!, System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(Visibility.Collapsed, result);
    }

    [Fact]
    public void FalseHiddenWhenH()
    {
        var c = new BoolToVisibilityAdvancedConverter();
        var result = c.Convert(false, typeof(Visibility), "H", System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(Visibility.Hidden, result);
    }
}

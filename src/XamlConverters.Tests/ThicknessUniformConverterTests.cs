// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

public class ThicknessUniformConverterTests
{
    [Fact]
    public void AllSides()
    {
        var c = new ThicknessUniformConverter();
        var t = (Thickness)c.Convert(5, typeof(Thickness), null!, System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(new Thickness(5), t);
    }

    [Fact]
    public void HorizontalOnly()
    {
        var c = new ThicknessUniformConverter();
        var t = (Thickness)c.Convert(3, typeof(Thickness), "H", System.Globalization.CultureInfo.InvariantCulture);
        Assert.Equal(3, t.Left);
        Assert.Equal(3, t.Right);
        Assert.Equal(0, t.Top);
        Assert.Equal(0, t.Bottom);
    }
}

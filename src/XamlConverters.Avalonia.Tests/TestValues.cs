// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Avalonia.Tests;

/// <summary>Provides named numeric values shared by Avalonia converter tests.</summary>
internal static class TestValues
{
    /// <summary>The canonical integer conversion value.</summary>
    public const int Answer = 42;

    /// <summary>The second byte in the sample byte sequence.</summary>
    public const byte SecondByte = 2;

    /// <summary>The third byte in the sample byte sequence.</summary>
    public const byte ThirdByte = 3;

    /// <summary>The index of the third byte in the sample byte sequence.</summary>
    public const int ThirdByteIndex = 2;

    /// <summary>The number of values in the sample collections.</summary>
    public const int SampleValueCount = 3;

    /// <summary>The first sample collection value.</summary>
    public const int FirstSampleValue = 10;

    /// <summary>The second sample collection value.</summary>
    public const int SecondSampleValue = 20;

    /// <summary>The third sample collection value.</summary>
    public const int ThirdSampleValue = 30;

    /// <summary>The fallback value absent from the sample collection.</summary>
    public const int MissingSampleValue = 99;
}

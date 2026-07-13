// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters.Tests;

/// <summary>Provides named numeric values shared by converter tests.</summary>
internal static class TestValues
{
    /// <summary>The canonical integer conversion value.</summary>
    public const int Answer = 42;

    /// <summary>The expected byte count of a GUID.</summary>
    public const int GuidByteCount = 16;

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

    /// <summary>The tolerance used for floating-point assertions.</summary>
    public const double FloatingPointTolerance = 0.00001d;

    /// <summary>The input used by multiplier tests.</summary>
    public const int MultiplierInput = 6;

    /// <summary>The expected multiplied value.</summary>
    public const double MultipliedValue = 15d;

    /// <summary>The comparison boundary used by comparison tests.</summary>
    public const int ComparisonBoundary = 5;

    /// <summary>A value immediately below the comparison boundary.</summary>
    public const int BelowComparisonBoundary = 4;

    /// <summary>The left operand in the math-expression test.</summary>
    public const int MathLeftOperand = 5;

    /// <summary>The right operand in the math-expression test.</summary>
    public const int MathRightOperand = 3;

    /// <summary>The expected math-expression result.</summary>
    public const double MathExpectedResult = 11d;

    /// <summary>The uniform thickness used by the all-sides test.</summary>
    public const int UniformThickness = 5;

    /// <summary>The thickness used by the horizontal-only test.</summary>
    public const int HorizontalThickness = 3;
}

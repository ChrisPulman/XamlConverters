// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows;

namespace CP.Xaml.Converters;

/// <summary>
/// Static registry exposing singleton instances for commonly reused stateless converters to reduce XAML noise.
/// </summary>
public static class ConvertersRegistry
{
    /// <summary>
    /// The not.
    /// </summary>
    public static readonly BoolNegationConverter Not = new();
    /// <summary>
    /// The bool to visibility.
    /// </summary>
    public static readonly BoolToVisibilityConverter BoolToVisibility = new();
    /// <summary>
    /// The bool to visibility adv.
    /// </summary>
    public static readonly BoolToVisibilityAdvancedConverter BoolToVisibilityAdv = new();
    /// <summary>
    /// The not null to visibility.
    /// </summary>
    public static readonly ValueNotNullToVisibilityConverter NotNullToVisibility = new();
    /// <summary>
    /// The not null to bool.
    /// </summary>
    public static readonly ValueNotNullToBoolConverter NotNullToBool = new();
    /// <summary>
    /// The null to bool.
    /// </summary>
    public static readonly NullToBoolConverter NullToBool = NullToBoolConverter.NotNull;
    /// <summary>
    /// The null coalesce.
    /// </summary>
    public static readonly NullCoalesceConverter NullCoalesce = new();
    /// <summary>
    /// The invert visibility.
    /// </summary>
    public static readonly InvertVisibilityConverter InvertVisibility = new();
    /// <summary>
    /// The bg to readable.
    /// </summary>
    public static readonly BackgroundColorToBwForegroundConverter BgToReadable = new();
    /// <summary>
    /// The percentage.
    /// </summary>
    public static readonly PercentageConverter Percentage = new();
    /// <summary>
    /// The arithmetic.
    /// </summary>
    public static readonly ArithmeticConverter Arithmetic = new();
    /// <summary>
    /// The math.
    /// </summary>
    public static readonly MathConverter Math = new();
    /// <summary>
    /// The equality.
    /// </summary>
    public static readonly EqualityConverter Equality = new();
    /// <summary>
    /// The comparison.
    /// </summary>
    public static readonly ComparisonConverter Comparison = new();
    /// <summary>
    /// The and.
    /// </summary>
    public static readonly MultiBooleanAndConverter And = new();
    /// <summary>
    /// The or.
    /// </summary>
    public static readonly MultiBooleanOrConverter Or = new();
    /// <summary>
    /// The xor.
    /// </summary>
    public static readonly BooleanXorConverter Xor = new();
    /// <summary>
    /// The multi format.
    /// </summary>
    public static readonly MultiStringFormatConverter MultiFormat = new();
}

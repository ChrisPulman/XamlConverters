// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Metadata;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Singleton instances for common stateless converters.</summary>
public static class ConvertersRegistry
{
    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static BoolNegationConverter Not { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static BoolToVisibilityConverter BoolToVisibility { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static BoolToVisibilityAdvancedConverter BoolToVisibilityAdv { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static ValueNotNullToVisibilityConverter NotNullToVisibility { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static ValueNotNullToBoolConverter NotNullToBool { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static NullToBoolConverter NullToBool { get; } = NullToBoolConverter.NotNull;

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static NullCoalesceConverter NullCoalesce { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static InvertVisibilityConverter InvertVisibility { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static BackgroundColorToBwForegroundConverter BgToReadable { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static PercentageConverter Percentage { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static ArithmeticConverter Arithmetic { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static MathConverter Math { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static EqualityConverter Equality { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static ComparisonConverter Comparison { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static MultiBooleanAndConverter And { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static MultiBooleanOrConverter Or { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static BooleanXorConverter Xor { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static MultiStringFormatConverter MultiFormat { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static BclTypeConverter BclType { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static StringFormatConverter StringFormat { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static CountToBooleanConverter HasItems { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static ChangeTypeConverter ChangeType { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static TypeMatchConverter TypeMatch { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static ByteArrayToBase64Converter Base64 { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static GuidConverter Guid { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static UriConverter Uri { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static NumberFormatConverter NumberFormat { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static CollectionContainsConverter Contains { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static CollectionFirstOrDefaultConverter FirstOrDefault { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static CollectionItemConverter Item { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static EnumerableToStringConverter Join { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static EnumDescriptionConverter EnumDescription { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static EnumHasFlagConverter EnumHasFlag { get; } = new();

    /// <summary>
    /// Gets a reusable converter value.
    /// </summary>
    public static EnumValuesConverter EnumValues { get; } = new();
}

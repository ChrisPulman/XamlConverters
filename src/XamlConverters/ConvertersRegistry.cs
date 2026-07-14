// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace CP.Xaml.Converters;

/// <summary>Provides singleton instances of commonly reused stateless converters.</summary>
public static class ConvertersRegistry
{
    /// <summary>Converts between compatible BCL types.</summary>
    public static readonly ChangeTypeConverter ChangeType = new();

    /// <summary>Tests whether a value is assignable to a requested type.</summary>
    public static readonly TypeMatchConverter TypeMatch = new();

    /// <summary>Formats a single value as text.</summary>
    public static readonly StringFormatConverter StringFormat = new();

    /// <summary>Formats and parses numeric values.</summary>
    public static readonly NumberFormatConverter NumberFormat = new();

    /// <summary>Formats and parses date/time values.</summary>
    public static readonly DateTimeFormatConverter DateTimeFormat = new();

    /// <summary>Formats and parses durations.</summary>
    public static readonly TimeSpanFormatConverter TimeSpanFormat = new();

    /// <summary>Tests whether an enumerable contains a parameter value.</summary>
    public static readonly CollectionContainsConverter Contains = new();

    /// <summary>Selects the first item from an enumerable.</summary>
    public static readonly CollectionFirstOrDefaultConverter FirstOrDefault = new();

    /// <summary>Selects an item by key or index.</summary>
    public static readonly CollectionItemConverter Item = new();

    /// <summary>Joins an enumerable into text.</summary>
    public static readonly EnumerableToStringConverter Join = new();

    /// <summary>Converts enum members to descriptions.</summary>
    public static readonly EnumDescriptionConverter EnumDescription = new();

    /// <summary>Tests a flags enum for a requested flag.</summary>
    public static readonly EnumHasFlagConverter EnumHasFlag = new();

    /// <summary>Returns the declared values for an enum type.</summary>
    public static readonly EnumValuesConverter EnumValues = new();

    /// <summary>Converts between URI values and strings.</summary>
    public static readonly UriConverter Uri = new();

    /// <summary>Converts between GUID representations.</summary>
    public static readonly GuidConverter Guid = new();

    /// <summary>Converts between byte arrays and Base64 text.</summary>
    public static readonly ByteArrayToBase64Converter Base64 = new();

    /// <summary>The not.</summary>
    public static readonly BoolNegationConverter Not = new();

    /// <summary>The bool to visibility.</summary>
    public static readonly BoolToVisibilityConverter BoolToVisibility = new();

    /// <summary>The bool to visibility adv.</summary>
    public static readonly BoolToVisibilityAdvancedConverter BoolToVisibilityAdv = new();

    /// <summary>The not null to visibility.</summary>
    public static readonly ValueNotNullToVisibilityConverter NotNullToVisibility = new();

    /// <summary>The not null to bool.</summary>
    public static readonly ValueNotNullToBoolConverter NotNullToBool = new();

    /// <summary>The null to bool.</summary>
    public static readonly NullToBoolConverter NullToBool = NullToBoolConverter.NotNull;

    /// <summary>The null coalesce.</summary>
    public static readonly NullCoalesceConverter NullCoalesce = new();

    /// <summary>The invert visibility.</summary>
    public static readonly InvertVisibilityConverter InvertVisibility = new();

    /// <summary>The bg to readable.</summary>
    public static readonly BackgroundColorToBwForegroundConverter BgToReadable = new();

    /// <summary>The percentage.</summary>
    public static readonly PercentageConverter Percentage = new();

    /// <summary>The arithmetic.</summary>
    public static readonly ArithmeticConverter Arithmetic = new();

    /// <summary>The math.</summary>
    public static readonly MathConverter Math = new();

    /// <summary>The equality.</summary>
    public static readonly EqualityConverter Equality = new();

    /// <summary>The comparison.</summary>
    public static readonly ComparisonConverter Comparison = new();

    /// <summary>The and.</summary>
    public static readonly MultiBooleanAndConverter And = new();

    /// <summary>The or.</summary>
    public static readonly MultiBooleanOrConverter Or = new();

    /// <summary>The xor.</summary>
    public static readonly BooleanXorConverter Xor = new();

    /// <summary>The multi format.</summary>
    public static readonly MultiStringFormatConverter MultiFormat = new();
}

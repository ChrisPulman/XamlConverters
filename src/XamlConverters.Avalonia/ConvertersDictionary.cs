// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Avalonia.Controls;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>A ready-to-merge Avalonia resource dictionary containing common converter instances.</summary>
public sealed class ConvertersDictionary : ResourceDictionary
{
    /// <summary>Initializes a new instance of the <see cref="ConvertersDictionary"/> class.</summary>
    public ConvertersDictionary()
    {
        Add("Not", ConvertersRegistry.Not);
        Add("BoolToVisibility", ConvertersRegistry.BoolToVisibility);
        Add("BoolToVisibilityAdvanced", ConvertersRegistry.BoolToVisibilityAdv);
        Add("NotNullToVisibility", ConvertersRegistry.NotNullToVisibility);
        Add("NotNullToBool", ConvertersRegistry.NotNullToBool);
        Add("NullToBool", ConvertersRegistry.NullToBool);
        Add("NullCoalesce", ConvertersRegistry.NullCoalesce);
        Add("InvertVisibility", ConvertersRegistry.InvertVisibility);
        Add("BackgroundToReadableForeground", ConvertersRegistry.BgToReadable);
        Add("Percentage", ConvertersRegistry.Percentage);
        Add("Arithmetic", ConvertersRegistry.Arithmetic);
        Add("Math", ConvertersRegistry.Math);
        Add("Equality", ConvertersRegistry.Equality);
        Add("Comparison", ConvertersRegistry.Comparison);
        Add("BooleanAnd", ConvertersRegistry.And);
        Add("BooleanOr", ConvertersRegistry.Or);
        Add("BooleanXor", ConvertersRegistry.Xor);
        Add("MultiStringFormat", ConvertersRegistry.MultiFormat);
        Add("BclType", ConvertersRegistry.BclType);
        Add("StringFormat", ConvertersRegistry.StringFormat);
        Add("HasItems", ConvertersRegistry.HasItems);
        Add("ChangeType", ConvertersRegistry.ChangeType);
        Add("TypeMatch", ConvertersRegistry.TypeMatch);
        Add("Base64", ConvertersRegistry.Base64);
        Add("Guid", ConvertersRegistry.Guid);
        Add("Uri", ConvertersRegistry.Uri);
        Add("NumberFormat", ConvertersRegistry.NumberFormat);
        Add("Contains", ConvertersRegistry.Contains);
        Add("FirstOrDefault", ConvertersRegistry.FirstOrDefault);
        Add("Item", ConvertersRegistry.Item);
        Add("Join", ConvertersRegistry.Join);
        Add("EnumDescription", ConvertersRegistry.EnumDescription);
        Add("EnumHasFlag", ConvertersRegistry.EnumHasFlag);
        Add("EnumValues", ConvertersRegistry.EnumValues);
    }
}

// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Maps an exact collection-size test to Avalonia visibility.</summary>
public sealed class CollectionSizeToVisibilityConverter : IValueConverter
{
    /// <summary>The count-to-Boolean converter used to evaluate collection size.</summary>
    private readonly CollectionSizeToBoolConverter _countConverter = new();

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        Equals(_countConverter.Convert(value, typeof(bool), parameter, culture), true);

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

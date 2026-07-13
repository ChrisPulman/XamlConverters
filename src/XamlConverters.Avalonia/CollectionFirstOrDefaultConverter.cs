// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Returns the first item in an enumerable, or a supplied fallback when empty.</summary>
public sealed class CollectionFirstOrDefaultConverter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (ConversionHelpers.IsNullLike(value))
        {
            return parameter;
        }

        if (value is not IEnumerable enumerable)
        {
            return ConversionHelpers.DoNothing;
        }

        var enumerator = enumerable.GetEnumerator();
        try
        {
            return enumerator.MoveNext() ? enumerator.Current : parameter;
        }
        finally
        {
            (enumerator as IDisposable)?.Dispose();
        }
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

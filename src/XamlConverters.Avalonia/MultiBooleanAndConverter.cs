// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Logical AND over all Boolean multi-binding inputs.</summary>
public sealed class MultiBooleanAndConverter : IMultiValueConverter
{
    /// <inheritdoc/>
    public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(values);
        return values.Count > 0 && values.Where(value => !ConversionHelpers.IsUnset(value)).All(value => ConversionHelpers.IsTrue(value, culture));
    }
}

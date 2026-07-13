// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Logical OR over all Boolean multi-binding inputs.</summary>
public sealed class MultiBooleanOrConverter : IMultiValueConverter
{
    /// <inheritdoc/>
    public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(values);
        return values.Any(value => !ConversionHelpers.IsUnset(value) && ConversionHelpers.IsTrue(value, culture));
    }
}

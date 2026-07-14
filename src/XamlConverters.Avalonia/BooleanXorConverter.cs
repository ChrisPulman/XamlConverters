// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Logical XOR over all Boolean multi-binding inputs.</summary>
public sealed class BooleanXorConverter : IMultiValueConverter
{
    /// <summary>The divisor used to determine odd parity.</summary>
    private const int ParityDivisor = 2;

    /// <inheritdoc/>
    public object Convert(
        IList<object?> values,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(values);
        return values.Count(value =>
                !ConversionHelpers.IsUnset(value) && ConversionHelpers.IsTrue(value, culture)) % ParityDivisor
            == 1;
    }
}

// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Formats values from a multi-binding with a composite format parameter.</summary>
public sealed class MultiStringFormatConverter : IMultiValueConverter
{
    /// <inheritdoc/>
    public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        var format = parameter?.ToString();
        return string.IsNullOrEmpty(format) ? string.Concat(values) : string.Format(culture, format, values.ToArray());
    }
}

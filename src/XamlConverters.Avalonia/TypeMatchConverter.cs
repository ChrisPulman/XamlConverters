// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Determines whether a bound value is assignable to a requested type.</summary>
public sealed class TypeMatchConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter is Type expectedType)
        {
            return value is not null && expectedType.IsInstanceOfType(value);
        }

        var expectedName = parameter as string;
        if (string.IsNullOrWhiteSpace(expectedName))
        {
            return false;
        }

        var invert = expectedName![0] == '!';
        if (invert)
        {
            expectedName = expectedName.Substring(1);
        }

        var actualType = value as Type ?? value?.GetType();
        var matches = actualType is not null
            && (string.Equals(actualType.Name, expectedName, StringComparison.OrdinalIgnoreCase)
                || string.Equals(actualType.FullName, expectedName, StringComparison.OrdinalIgnoreCase)
                || string.Equals(actualType.AssemblyQualifiedName, expectedName, StringComparison.OrdinalIgnoreCase));

        return invert ? !matches : matches;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;
}

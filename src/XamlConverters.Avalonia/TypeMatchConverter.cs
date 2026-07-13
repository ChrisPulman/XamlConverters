// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

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
            expectedName = expectedName[1..];
        }

        var actualType = (value as Type) ?? value?.GetType();
        var matches = actualType is not null && MatchesName(actualType, expectedName);

        return invert ? !matches : matches;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;

    /// <summary>Determines whether any supported type name matches the requested name.</summary>
    /// <param name="type">The type to inspect.</param>
    /// <param name="expectedName">The requested type name.</param>
    /// <returns><see langword="true"/> when a name matches; otherwise, <see langword="false"/>.</returns>
    private static bool MatchesName(Type type, string expectedName) =>
        string.Equals(type.Name, expectedName, StringComparison.OrdinalIgnoreCase)
        || string.Equals(type.FullName, expectedName, StringComparison.OrdinalIgnoreCase)
        || string.Equals(type.AssemblyQualifiedName, expectedName, StringComparison.OrdinalIgnoreCase);
}

// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Determines whether the bound value is assignable to a requested type.</summary>
/// <remarks>
/// The parameter may be a <see cref="Type"/> or a simple, full, or assembly-qualified type name.
/// Prefix a type name with <c>!</c> to invert the result.
/// </remarks>
public sealed class TypeMatchConverter : IValueConverter
{
    /// <summary>Tests the runtime type of the supplied value.</summary>
    /// <param name="value">The value whose runtime type is inspected.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">The expected <see cref="Type"/> or type name.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns><see langword="true"/> when the value matches the requested type.</returns>
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
            expectedName = expectedName.Remove(0, 1);
        }

        var actualType = (value as Type) ?? value?.GetType();
        var matches = actualType is not null && MatchesName(actualType, expectedName);

        return invert ? !matches : matches;
    }

    /// <summary>Reverse conversion is not supported.</summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns><see cref="Binding.DoNothing"/>.</returns>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Binding.DoNothing;

    /// <summary>Determines whether any supported type name matches the requested name.</summary>
    /// <param name="type">The type to inspect.</param>
    /// <param name="expectedName">The requested type name.</param>
    /// <returns><see langword="true"/> when a name matches; otherwise, <see langword="false"/>.</returns>
    private static bool MatchesName(Type type, string expectedName) =>
        string.Equals(type.Name, expectedName, StringComparison.OrdinalIgnoreCase)
        || string.Equals(type.FullName, expectedName, StringComparison.OrdinalIgnoreCase)
        || string.Equals(type.AssemblyQualifiedName, expectedName, StringComparison.OrdinalIgnoreCase);
}

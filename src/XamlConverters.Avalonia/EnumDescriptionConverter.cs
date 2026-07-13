// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Converts enum members to and from their <see cref="DescriptionAttribute.Description"/> text.</summary>
public sealed class EnumDescriptionConverter : IValueConverter
{
    /// <summary>The converter used for enum-to-description conversion.</summary>
    private readonly EnumToDescriptionConverter _forwardConverter = new();

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is Enum
            ? _forwardConverter.Convert(value, targetType, parameter, culture)
            : ConversionHelpers.DoNothing;

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(targetType);

        var enumType = (parameter as Type) ?? Nullable.GetUnderlyingType(targetType) ?? targetType;
        if (!enumType.IsEnum || value is null)
        {
            return ConversionHelpers.DoNothing;
        }

        var text = value.ToString();
        foreach (var enumValue in Enum.GetValues(enumType).Cast<Enum>())
        {
            var description = _forwardConverter.Convert(enumValue, typeof(string), null, culture)?.ToString();
            if (string.Equals(enumValue.ToString(), text, StringComparison.OrdinalIgnoreCase)
                || string.Equals(description, text, StringComparison.CurrentCultureIgnoreCase))
            {
                return enumValue;
            }
        }

        return ConversionHelpers.TryConvert(value, enumType, culture, out var converted)
            ? converted!
            : ConversionHelpers.DoNothing;
    }
}

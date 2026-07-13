// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Converts enum members to and from their <see cref="DescriptionAttribute.Description"/> text.</summary>
public sealed class EnumDescriptionConverter : IValueConverter
{
    /// <summary>Returns an enum member's description, falling back to its member name.</summary>
    /// <param name="value">The enum value.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">The converter parameter.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns>The description, member name, or <see cref="Binding.DoNothing"/> for a non-enum value.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not Enum enumValue ? Binding.DoNothing : GetDescription(enumValue.GetType(), enumValue.ToString());
    }

    /// <summary>Finds an enum member by description or member name.</summary>
    /// <param name="value">The description or member name.</param>
    /// <param name="targetType">The enum binding source type.</param>
    /// <param name="parameter">An optional enum <see cref="Type"/> when the source type is not available.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns>The matching enum value, or <see cref="Binding.DoNothing"/> when no member matches.</returns>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (targetType is null)
        {
            throw new ArgumentNullException(nameof(targetType));
        }

        var enumType = (parameter as Type) ?? Nullable.GetUnderlyingType(targetType) ?? targetType;
        if (!enumType.IsEnum || value is null)
        {
            return Binding.DoNothing;
        }

        var text = value.ToString();
        foreach (var name in Enum.GetNames(enumType))
        {
            if (string.Equals(name, text, StringComparison.OrdinalIgnoreCase)
                || string.Equals(GetDescription(enumType, name), text, StringComparison.CurrentCultureIgnoreCase))
            {
                return Enum.Parse(enumType, name, ignoreCase: true);
            }
        }

        return BclConversion.TryChangeType(value, enumType, culture, out var converted)
            ? converted!
            : Binding.DoNothing;
    }

    /// <summary>Gets an enum member's description.</summary>
    /// <param name="enumType">The enum type.</param>
    /// <param name="memberName">The member name.</param>
    /// <returns>The description text, or the member name when no description exists.</returns>
    private static string GetDescription(Type enumType, string memberName)
    {
        var member = enumType.GetMember(memberName, BindingFlags.Public | BindingFlags.Static).FirstOrDefault();
        var description = member is null
            ? null
            : Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return description?.Description ?? memberName;
    }
}

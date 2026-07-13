// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>Converts between <see cref="Guid"/>, its textual representation, and a 16-byte array.</summary>
public sealed class GuidConverter : IValueConverter
{
    /// <summary>Converts a GUID to text or converts text/bytes to a GUID according to the target type.</summary>
    /// <param name="value">A GUID, GUID string, or 16-byte array.</param>
    /// <param name="targetType">The binding target type.</param>
    /// <param name="parameter">An optional GUID format such as <c>D</c>, <c>N</c>, <c>B</c>, <c>P</c>, or <c>X</c>.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns>The converted value, or <see cref="Binding.DoNothing"/> when conversion fails.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Guid guid)
        {
            if (targetType == typeof(byte[]))
            {
                return guid.ToByteArray();
            }

            try
            {
                return guid.ToString((parameter as string) ?? "D");
            }
            catch (FormatException)
            {
                return Binding.DoNothing;
            }
        }

        if (value is byte[] bytes && bytes.Length == 16)
        {
            return new Guid(bytes);
        }

        return BclConversion.TryChangeType(value, typeof(Guid), culture, out var converted)
            ? converted!
            : Binding.DoNothing;
    }

    /// <summary>Converts a target representation back to the requested GUID, string, or byte-array source type.</summary>
    /// <param name="value">The target value.</param>
    /// <param name="targetType">The binding source type.</param>
    /// <param name="parameter">An optional GUID format.</param>
    /// <param name="culture">The culture used by the binding.</param>
    /// <returns>The converted value, or <see cref="Binding.DoNothing"/> when conversion fails.</returns>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (targetType == typeof(string) && value is Guid guid)
        {
            return guid.ToString((parameter as string) ?? "D");
        }

        if (targetType == typeof(byte[]) && value is Guid byteGuid)
        {
            return byteGuid.ToByteArray();
        }

        return BclConversion.TryChangeType(value, targetType, culture, out var converted)
            ? converted!
            : Binding.DoNothing;
    }
}

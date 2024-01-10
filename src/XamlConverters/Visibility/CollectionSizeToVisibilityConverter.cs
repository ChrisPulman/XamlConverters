// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows;

namespace CP.Xaml.Converters;

/// <summary>
/// Collection Size To Visibility Converter.
/// </summary>
public class CollectionSizeToVisibilityConverter : CollectionSizeToBoolConverter
{
    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var meetsRequiredCollectionSize = base.Convert(value, targetType, parameter, culture) as bool?;
        return meetsRequiredCollectionSize == true ? Visibility.Visible : Visibility.Collapsed;
    }
}

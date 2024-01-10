// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters;

/// <summary>
/// Collection Size To Boolean Converter.
/// </summary>
public class CollectionSizeToBoolConverter : IValueConverter
{
    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var reverse = parameter?.Equals("reverse");
        var requiredCollectionSize = 0;
        if (reverse == false && parameter != null)
        {
            try
            {
                requiredCollectionSize = int.Parse(parameter.ToString()!);
            }
            catch
            {
            }
        }

        bool meetsRequiredCollectionSize;
        if (value is ICollection collection)
        {
            meetsRequiredCollectionSize = collection.Count == requiredCollectionSize;
        }
        else if (value is IEnumerable enumerable)
        {
            var enumerator = enumerable.GetEnumerator();
            var size = 0;
            while (enumerator.MoveNext())
            {
                size++;
                if (size > requiredCollectionSize)
                {
                    break;
                }
            }

            meetsRequiredCollectionSize = size == requiredCollectionSize;
        }
        else
        {
            meetsRequiredCollectionSize = false;
        }

        if (reverse == true)
        {
            meetsRequiredCollectionSize = !meetsRequiredCollectionSize;
        }

        return meetsRequiredCollectionSize;
    }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
}

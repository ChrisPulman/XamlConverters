// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Evaluates arithmetic expressions containing x/a, y/b, z/c, t/d, or indexed placeholders.</summary>
public sealed class MathConverter : IValueConverter, IMultiValueConverter
{
    private readonly Dictionary<string, MathExpression> _expressions = new(StringComparer.Ordinal);

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        Evaluate([value], targetType, parameter, culture);

    /// <inheritdoc/>
    object? IMultiValueConverter.Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture) =>
        Evaluate(values, targetType, parameter, culture);

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ConversionHelpers.DoNothing;

    private object Evaluate(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        var text = parameter?.ToString();
        if (string.IsNullOrWhiteSpace(text))
        {
            return ConversionHelpers.UnsetValue;
        }

        try
        {
            if (!_expressions.TryGetValue(text, out var expression))
            {
                expression = MathExpression.Parse(text);
                _expressions[text] = expression;
            }

            return ConversionHelpers.ConvertDecimal(expression.Evaluate(values, culture), targetType, culture);
        }
        catch (Exception ex) when (ex is ArgumentException or DivideByZeroException or FormatException or InvalidCastException or OverflowException)
        {
            return ConversionHelpers.UnsetValue;
        }
    }
}

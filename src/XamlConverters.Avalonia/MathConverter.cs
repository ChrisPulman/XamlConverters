// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Evaluates arithmetic expressions containing x/a, y/b, z/c, t/d, or indexed placeholders.</summary>
public sealed class MathConverter : IValueConverter, IMultiValueConverter
{
    /// <summary>The cache of parsed arithmetic expressions.</summary>
    private readonly Dictionary<string, MathExpression> _expressions = new(StringComparer.Ordinal);

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        Evaluate([value], targetType, parameter, culture);

    /// <inheritdoc/>
    object? IMultiValueConverter.Convert(
        IList<object?> values,
        Type targetType,
        object? parameter,
        CultureInfo culture) => Evaluate(values, targetType, parameter, culture);

    /// <inheritdoc/>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) => ConversionHelpers.DoNothing;

    /// <summary>Evaluates the configured arithmetic expression.</summary>
    /// <param name="values">The converter input values.</param>
    /// <param name="targetType">The requested target type.</param>
    /// <param name="parameter">The arithmetic expression.</param>
    /// <param name="culture">The conversion culture.</param>
    /// <returns>The evaluated value or an Avalonia binding sentinel.</returns>
    private object Evaluate(
        IList<object?> values,
        Type targetType,
        object? parameter,
        CultureInfo culture)
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

            return ConversionHelpers.ConvertDecimal(
                expression.Evaluate(values, culture),
                targetType,
                culture);
        }
        catch (Exception ex)
            when (ex
                    is ArgumentException
                        or DivideByZeroException
                        or FormatException
                        or InvalidCastException
                        or OverflowException)
        {
            return ConversionHelpers.UnsetValue;
        }
    }
}

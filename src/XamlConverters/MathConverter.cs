// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace CP.Xaml.Converters;

/// <summary>
/// Value converter that performs arithmetic calculations over its argument(s).
/// </summary>
/// <remarks>
/// MathConverter can act as a value converter, or as a multi-value converter (WPF only). It is
/// also a markup extension (WPF only) which allows to avoid declaring resources,
/// ConverterParameter must contain an arithmetic expression over converter arguments. Operations
/// supported are +, -, * and / Single argument of a value converter may referred as x, a, or {0}
/// Arguments of multi value converter may be referred as x,y,z,t (first-fourth argument), or
/// a,b,c,d, or {0}, {1}, {2}, {3}, {4}, ... The converter supports arithmetic expressions of
/// arbitrary complexity, including nested sub expressions.
/// </remarks>
public class MathConverter : MarkupExtension, IMultiValueConverter, IValueConverter
{
    /// <summary>
    /// The stored expressions.
    /// </summary>
    private readonly Dictionary<string, IExpression> _storedExpressions = new();

    /// <summary>
    /// The IExpression.
    /// </summary>
    private interface IExpression
    {
        /// <summary>
        /// Evaluates the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A decimal.</returns>
        decimal Eval(object[] args);
    }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => CallOtherConvert((object[])value, targetType, parameter, culture);

    /// <summary>
    /// Converts source values to a value for the binding target. The data binding engine calls
    /// this method when it propagates the values from source bindings to the binding target.
    /// </summary>
    /// <param name="values">
    /// The array of values that the source bindings in the <see
    /// cref="MultiBinding"/> produces. The value <see
    /// cref="DependencyProperty.UnsetValue"/> indicates that the source binding
    /// has no value to provide for conversion.
    /// </param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value.If the method returns null, the valid null value is used.A return value
    /// of <see cref="DependencyProperty"/>. <see
    /// cref="DependencyProperty.UnsetValue"/> indicates that the converter did
    /// not produce a value, and that the binding will use the <see
    /// cref="BindingBase.FallbackValue"/> if it is available, or else will
    /// use the default value.A return value of <see cref="Binding"/>. <see
    /// cref="Binding.DoNothing"/> indicates that the binding does not
    /// transfer the value or use the <see
    /// cref="BindingBase.FallbackValue"/> or the default value.
    /// </returns>
    /// <exception cref="ArgumentException">Argument Exception.</exception>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var result = Parse(parameter?.ToString()!).Eval(values);
            return targetType == typeof(decimal)
                ? result
                : targetType == typeof(string)
                ? result.ToString(CultureInfo.InvariantCulture)
                : targetType == typeof(int)
                ? (int)result
                : targetType == typeof(double)
                ? (double)result
                : (object)(targetType == typeof(long)
                ? (long)result
                : throw new ArgumentException(string.Format("Unsupported target type {0}", targetType?.FullName)));
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

        return DependencyProperty.UnsetValue;
    }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    /// <exception cref="NotImplementedException">Not Implemented Exception.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;

    /// <summary>
    /// Converts a binding target value to the source binding values.
    /// </summary>
    /// <param name="value">The value that the binding target produces.</param>
    /// <param name="targetTypes">
    /// The array of types to convert to. The array length indicates the number and types of
    /// values that are suggested for the method to return.
    /// </param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// An array of values that have been converted from the target value back to the source values.
    /// </returns>
    /// <exception cref="NotImplementedException">Not Implemented Exception.</exception>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => new object[] { value };

    /// <summary>
    /// When implemented in a derived class, returns an object that is provided as the value of
    /// the target property for this markup extension.
    /// </summary>
    /// <param name="serviceProvider">
    /// A service provider helper that can provide services for the markup extension.
    /// </param>
    /// <returns>The object value to set on the property where the extension is applied.</returns>
    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    /// <summary>
    /// Processes the exception.
    /// </summary>
    /// <param name="ex">The ex.</param>
    protected virtual void ProcessException(Exception ex) => Console.WriteLine(ex);

    /// <summary>
    /// Calls the other convert.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>new object.</returns>
    private object CallOtherConvert(object[] values, Type targetType, object parameter, CultureInfo culture) => Convert(values, targetType, parameter, culture);

    /// <summary>
    /// Parses the specified s.
    /// </summary>
    /// <param name="s">The s.</param>
    /// <returns>An Expression.</returns>
    private IExpression Parse(string s)
    {
        if (!_storedExpressions.TryGetValue(s, out var result))
        {
            result = new Parser().Parse(s);
            _storedExpressions[s] = result;
        }

        return result;
    }

    /// <summary>
    /// Binary Operation.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="BinaryOperation"/> class.
    /// </remarks>
    /// <param name="operation">The operation.</param>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <exception cref="ArgumentException">Invalid operation + operation.</exception>
    private class BinaryOperation(char operation, IExpression left, IExpression right) : IExpression
    {
        /// <summary>
        /// The left.
        /// </summary>
        private readonly IExpression _left = left;

        /// <summary>
        /// The operation.
        /// </summary>
        private readonly Func<decimal, decimal, decimal> _operation = operation switch
        {
            '+' => (a, b) => a + b,
            '-' => (a, b) => a - b,
            '*' => (a, b) => a * b,
            '/' => (a, b) => a / b,
            _ => throw new ArgumentException("Invalid operation " + operation),
        };

        /// <summary>
        /// The right.
        /// </summary>
        private readonly IExpression _right = right;

        /// <summary>
        /// Evaluates the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A decimal.</returns>
        public decimal Eval(object[] args) => _operation(_left.Eval(args), _right.Eval(args));
    }

    /// <summary>
    /// A Constant.
    /// </summary>
    private class Constant : IExpression
    {
        /// <summary>
        /// The value.
        /// </summary>
        private readonly decimal _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Constant"/> class.
        /// </summary>
        /// <param name="text">The textToParse.</param>
        /// <exception cref="ArgumentException">valid number.</exception>
        public Constant(string text)
        {
            if (!decimal.TryParse(text, out _value))
            {
                throw new ArgumentException(string.Format("'{0}' is not a valid number", text));
            }
        }

        /// <summary>
        /// Evaluates the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A decimal.</returns>
        public decimal Eval(object[] args) => _value;
    }

    /// <summary>
    /// A Negate.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Negate"/> class.
    /// </remarks>
    /// <param name="param">The parameter.</param>
    private class Negate(IExpression param) : IExpression
    {
        /// <summary>
        /// The parameter.
        /// </summary>
        private readonly IExpression _param = param;

        /// <summary>
        /// Evaluates the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>decimal of arguments.</returns>
        public decimal Eval(object[] args) => -_param.Eval(args);
    }

    /// <summary>
    /// A Parser.
    /// </summary>
    private class Parser
    {
        /// <summary>
        /// The decimal REGEX.
        /// </summary>
        private const string DecimalRegEx = @"(\d+\.?\d*|\d*\.?\d+)";

        /// <summary>
        /// The position.
        /// </summary>
        private int _pos;

        /// <summary>
        /// The textToParse.
        /// </summary>
        private string? _text;

        /// <summary>
        /// Parse textToParse.
        /// </summary>
        /// <param name="textToParse">The text to parse.</param>
        /// <returns>A Expression.</returns>
        /// <exception cref="ArgumentException">
        /// MathConverter: error parsing expression 'textToParse'. ex at position ?.
        /// </exception>
        public IExpression Parse(string textToParse)
        {
            try
            {
                _pos = 0;
                _text = textToParse;
                var result = ParseExpression();
                RequireEndOfText();
                return result;
            }
            catch (Exception ex)
            {
                var msg = string.Format("MathConverter: error parsing expression '{0}'. {1} at position {2}", textToParse, ex.Message, _pos);
                throw new ArgumentException(msg, ex);
            }
        }

        /// <summary>
        /// Create Variable.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>A Expression.</returns>
        private Variable CreateVariable(int n)
        {
            ++_pos;
            SkipWhiteSpace();
            return new Variable(n);
        }

        /// <summary>
        /// Parses the expression.
        /// </summary>
        /// <returns>A Expression.</returns>
        private IExpression ParseExpression()
        {
            var left = ParseTerm();

            while (true)
            {
                if (_pos >= _text?.Length || _text == null)
                {
                    return left;
                }

                var c = _text[_pos];

                if (c == '+' || c == '-')
                {
                    ++_pos;
                    var right = ParseTerm();
                    left = new BinaryOperation(c, left, right);
                }
                else
                {
                    return left;
                }
            }
        }

        /// <summary>
        /// Parses the factor.
        /// </summary>
        /// <returns>A Value.</returns>
        /// <exception cref="ArgumentException">
        /// Unexpected end of textToParse or Unmatched '{' or Missing parameter index after '{' or.
        /// </exception>
        private IExpression ParseFactor()
        {
            while (true)
            {
                SkipWhiteSpace();
                if (_text == null || _pos >= _text.Length)
                {
                    throw new ArgumentException("Unexpected end of textToParse");
                }

                var c = _text[_pos];

                switch (c)
                {
                    case '+':
                        ++_pos;
                        continue;

                    case '-':
                        ++_pos;
                        return new Negate(ParseFactor());

                    case 'x':
                    case 'a':
                        return CreateVariable(0);

                    case 'y':
                    case 'b':
                        return CreateVariable(1);

                    case 'z':
                    case 'c':
                        return CreateVariable(2);

                    case 't':
                    case 'd':
                        return CreateVariable(3);

                    case '(':
                        ++_pos;
                        var expression = ParseExpression();
                        SkipWhiteSpace();
                        Require(')');
                        SkipWhiteSpace();
                        return expression;

                    case '{':
                        ++_pos;
                        var end = _text.IndexOf('}', _pos);
                        if (end < 0)
                        {
                            --_pos;
                            throw new ArgumentException("Unmatched '{'");
                        }

                        if (end == _pos)
                        {
                            throw new ArgumentException("Missing parameter index after '{'");
                        }

                        Variable result = new(_text.Substring(_pos, end - _pos).Trim());
                        _pos = end + 1;
                        SkipWhiteSpace();
                        return result;
                }

                var match = Regex.Match(_text.Substring(_pos), DecimalRegEx);
                if (match.Success)
                {
                    _pos += match.Length;
                    SkipWhiteSpace();
                    return new Constant(match.Value);
                }

                throw new ArgumentException(string.Format("Unexpected character '{0}'", c));
            }
        }

        /// <summary>
        /// Parses the term.
        /// </summary>
        /// <returns>A Expression.</returns>
        private IExpression ParseTerm()
        {
            var left = ParseFactor();

            while (true)
            {
                if (_pos >= _text?.Length || _text == null)
                {
                    return left;
                }

                var c = _text[_pos];

                if (c == '*' || c == '/')
                {
                    ++_pos;
                    var right = ParseFactor();
                    left = new BinaryOperation(c, left, right);
                }
                else
                {
                    return left;
                }
            }
        }

        private void Require(char c)
        {
            if (_text == null || _pos >= _text.Length || _text[_pos] != c)
            {
                throw new ArgumentException("Expected '" + c + "'");
            }

            ++_pos;
        }

        private void RequireEndOfText()
        {
            if (_text == null)
            {
                throw new ArgumentException("Unexpected character 'NULL'");
            }

            if (_pos != _text.Length)
            {
                throw new ArgumentException("Unexpected character '" + _text[_pos] + "'");
            }
        }

        private void SkipWhiteSpace()
        {
            if (_text == null)
            {
                return;
            }

            while (_pos < _text.Length && char.IsWhiteSpace(_text[_pos]))
            {
                ++_pos;
            }
        }
    }

    private class Variable : IExpression
    {
        private readonly int _index;

        /// <summary>
        /// Initializes a new instance of the <see cref="Variable"/> class.
        /// </summary>
        /// <param name="text">The textToParse.</param>
        /// <exception cref="ArgumentException">Argument Exception.</exception>
        public Variable(string text)
        {
            if (!int.TryParse(text, out _index) || _index < 0)
            {
                throw new ArgumentException(string.Format("'{0}' is not a valid parameter index", text));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Variable"/> class.
        /// </summary>
        /// <param name="n">The n.</param>
        public Variable(int n) => _index = n;

        /// <summary>
        /// Evaluates the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A decimal.</returns>
        /// <exception cref="ArgumentException">Argument Exception.</exception>
        public decimal Eval(object[] args) =>
            _index >= args.Length
                ? throw new ArgumentException(string.Format("MathConverter: parameter index {0} is out of range. {1} parameter(s) supplied", _index, args.Length))
                : System.Convert.ToDecimal(args[_index]);
    }
}

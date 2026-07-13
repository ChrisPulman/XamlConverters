// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

internal sealed class MathExpression
{
    private readonly Node _root;

    private MathExpression(Node root) => _root = root;

    /// <summary>
    /// Provides converter behavior.
    /// </summary>
    public static MathExpression Parse(string text) => new(new Parser(text).Parse());

    /// <summary>
    /// Provides converter behavior.
    /// </summary>
    public decimal Evaluate(IList<object?> values, CultureInfo culture) => _root.Evaluate(values, culture);

    private abstract record Node
    {
        /// <summary>
        /// Provides converter behavior.
        /// </summary>
        public abstract decimal Evaluate(IList<object?> values, CultureInfo culture);
    }

    private sealed record Constant(decimal Value) : Node
    {
        /// <summary>
        /// Provides converter behavior.
        /// </summary>
        public override decimal Evaluate(IList<object?> values, CultureInfo culture) => Value;
    }

    private sealed record Variable(int Index) : Node
    {
        /// <summary>
        /// Provides converter behavior.
        /// </summary>
        public override decimal Evaluate(IList<object?> values, CultureInfo culture) =>
            Index < values.Count && ConversionHelpers.TryDecimal(values[Index], culture, out var value)
                ? value
                : throw new ArgumentException("Expression variable is unavailable or non-numeric.");
    }

    private sealed record Unary(Node Operand) : Node
    {
        /// <summary>
        /// Provides converter behavior.
        /// </summary>
        public override decimal Evaluate(IList<object?> values, CultureInfo culture) => -Operand.Evaluate(values, culture);
    }

    private sealed record Binary(char Operator, Node Left, Node Right) : Node
    {
        /// <summary>
        /// Provides converter behavior.
        /// </summary>
        public override decimal Evaluate(IList<object?> values, CultureInfo culture)
        {
            var left = Left.Evaluate(values, culture);
            var right = Right.Evaluate(values, culture);
            return Operator switch
            {
                '+' => left + right,
                '-' => left - right,
                '*' => left * right,
                '/' => left / right,
                _ => throw new ArgumentException("Unsupported operator."),
            };
        }
    }

    private sealed class Parser(string text)
    {
        private int _position;

        /// <summary>
        /// Provides converter behavior.
        /// </summary>
        public Node Parse()
        {
            var node = ParseExpression();
            SkipWhitespace();
            return _position == text.Length ? node : throw new ArgumentException("Unexpected expression text.");
        }

        private Node ParseExpression()
        {
            var left = ParseTerm();
            while (TryRead('+') || TryRead('-'))
            {
                var operation = text[_position - 1];
                left = new Binary(operation, left, ParseTerm());
            }

            return left;
        }

        private Node ParseTerm()
        {
            var left = ParseFactor();
            while (TryRead('*') || TryRead('/'))
            {
                var operation = text[_position - 1];
                left = new Binary(operation, left, ParseFactor());
            }

            return left;
        }

        private Node ParseFactor()
        {
            SkipWhitespace();
            if (TryRead('+'))
            {
                return ParseFactor();
            }

            if (TryRead('-'))
            {
                return new Unary(ParseFactor());
            }

            if (TryRead('('))
            {
                var expression = ParseExpression();
                return TryRead(')') ? expression : throw new ArgumentException("Missing closing parenthesis.");
            }

            if (_position < text.Length && "xaybzctd".Contains(text[_position]))
            {
                var index = "xaybzctd".IndexOf(text[_position++]) / 2;
                return new Variable(index);
            }

            if (TryRead('{'))
            {
                var start = _position;
                while (_position < text.Length && char.IsDigit(text[_position]))
                {
                    _position++;
                }

                var token = text.Substring(start, _position - start);
                return TryRead('}') && int.TryParse(token, out var index)
                    ? new Variable(index)
                    : throw new ArgumentException("Invalid indexed variable.");
            }

            var numberStart = _position;
            while (_position < text.Length && (char.IsDigit(text[_position]) || text[_position] is '.' or ','))
            {
                _position++;
            }

            var number = text.Substring(numberStart, _position - numberStart);
            return decimal.TryParse(number, NumberStyles.Any, CultureInfo.InvariantCulture, out var value)
                ? new Constant(value)
                : throw new ArgumentException("Expected a number or variable.");
        }

        private bool TryRead(char expected)
        {
            SkipWhitespace();
            if (_position >= text.Length || text[_position] != expected)
            {
                return false;
            }

            _position++;
            return true;
        }

        private void SkipWhitespace()
        {
            while (_position < text.Length && char.IsWhiteSpace(text[_position]))
            {
                _position++;
            }
        }
    }
}

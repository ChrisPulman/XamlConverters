// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Globalization;
using CP.Xaml.Converters.Avalonia.Internal;

namespace CP.Xaml.Converters.Avalonia;

/// <summary>Parses and evaluates arithmetic expressions over converter inputs.</summary>
internal sealed class MathExpression
{
    /// <summary>The number of character aliases assigned to each positional variable.</summary>
    private const int AliasesPerVariable = 2;

    /// <summary>The parsed expression root.</summary>
    private readonly Node _root;

    /// <summary>Initializes a new instance of the <see cref="MathExpression"/> class.</summary>
    /// <param name="root">The parsed expression root.</param>
    private MathExpression(Node root) => _root = root;

    /// <summary>Parses an arithmetic expression.</summary>
    /// <param name="text">The expression text.</param>
    /// <returns>The parsed expression.</returns>
    public static MathExpression Parse(string text) => new(new Parser(text).Parse());

    /// <summary>Evaluates the expression against the supplied converter values.</summary>
    /// <param name="values">The converter input values.</param>
    /// <param name="culture">The culture used for numeric conversion.</param>
    /// <returns>The expression result.</returns>
    public decimal Evaluate(IList<object?> values, CultureInfo culture) => _root.Evaluate(values, culture);

    /// <summary>Parses expression tokens into an expression tree.</summary>
    /// <param name="text">The expression text.</param>
    private sealed class Parser(string text)
    {
        /// <summary>The current parser position.</summary>
        private int _position;

        /// <summary>Parses the complete expression.</summary>
        /// <returns>The root expression node.</returns>
        public Node Parse()
        {
            var node = ParseExpression();
            SkipWhitespace();
            return _position == text.Length ? node : throw new ArgumentException("Unexpected expression text.");
        }

        /// <summary>Parses an addition or subtraction expression.</summary>
        /// <returns>The parsed node.</returns>
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

        /// <summary>Parses a multiplication or division term.</summary>
        /// <returns>The parsed node.</returns>
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

        /// <summary>Parses a literal, variable, unary expression, or parenthesized expression.</summary>
        /// <returns>The parsed node.</returns>
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

            return (Node?)ParseNamedVariable() ?? (TryRead('{') ? ParseIndexedVariable() : ParseNumber());
        }

        /// <summary>Parses a named positional variable when one is present.</summary>
        /// <returns>The variable node, or <see langword="null"/> when the next token is not a named variable.</returns>
        private Variable? ParseNamedVariable()
        {
            const string variableAliases = "xaybzctd";
            if (_position >= text.Length || !variableAliases.Contains(text[_position]))
            {
                return null;
            }

            var index = variableAliases.IndexOf(text[_position++]) / AliasesPerVariable;
            return new Variable(index);
        }

        /// <summary>Parses a positional variable enclosed in braces.</summary>
        /// <returns>The variable node.</returns>
        private Variable ParseIndexedVariable()
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

        /// <summary>Parses a decimal number.</summary>
        /// <returns>The constant node.</returns>
        private Constant ParseNumber()
        {
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

        /// <summary>Consumes the expected character when it is next in the input.</summary>
        /// <param name="expected">The character to consume.</param>
        /// <returns><see langword="true"/> when the character was consumed; otherwise, <see langword="false"/>.</returns>
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

        /// <summary>Advances past whitespace.</summary>
        private void SkipWhitespace()
        {
            while (_position < text.Length && char.IsWhiteSpace(text[_position]))
            {
                _position++;
            }
        }
    }

    /// <summary>Represents a node in the parsed expression tree.</summary>
    private abstract record Node
    {
        /// <summary>Evaluates this node.</summary>
        /// <param name="values">The converter input values.</param>
        /// <param name="culture">The culture used for numeric conversion.</param>
        /// <returns>The node result.</returns>
        public abstract decimal Evaluate(IList<object?> values, CultureInfo culture);
    }

    /// <summary>Represents a decimal constant.</summary>
    /// <param name="Value">The constant value.</param>
    private sealed record Constant(decimal Value) : Node
    {
        /// <inheritdoc/>
        public override decimal Evaluate(IList<object?> values, CultureInfo culture) => Value;
    }

    /// <summary>Represents an indexed converter input.</summary>
    /// <param name="Index">The input index.</param>
    private sealed record Variable(int Index) : Node
    {
        /// <inheritdoc/>
        public override decimal Evaluate(IList<object?> values, CultureInfo culture) =>
            Index < values.Count && ConversionHelpers.TryDecimal(values[Index], culture, out var value)
                ? value
                : throw new ArgumentException("Expression variable is unavailable or non-numeric.");
    }

    /// <summary>Represents numeric negation.</summary>
    /// <param name="Operand">The operand to negate.</param>
    private sealed record Unary(Node Operand) : Node
    {
        /// <inheritdoc/>
        public override decimal Evaluate(IList<object?> values, CultureInfo culture) => -Operand.Evaluate(values, culture);
    }

    /// <summary>Represents a binary arithmetic operation.</summary>
    /// <param name="Operator">The arithmetic operator.</param>
    /// <param name="Left">The left operand.</param>
    /// <param name="Right">The right operand.</param>
    private sealed record Binary(char Operator, Node Left, Node Right) : Node
    {
        /// <inheritdoc/>
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
}

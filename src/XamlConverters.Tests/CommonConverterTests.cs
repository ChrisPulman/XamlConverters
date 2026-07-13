// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Windows.Data;

namespace CP.Xaml.Converters.Tests;

/// <summary>
/// Tests common framework and primitive value conversions.
/// </summary>
public class CommonConverterTests
{
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

    /// <summary>
    /// Converts text using invariant casing rules.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsTextCasing()
    {
        var upper = new ToUpperConverter().Convert("Mixed Case", typeof(string), null!, InvariantCulture);
        var lower = new ToLowerConverter().Convert("Mixed Case", typeof(string), null!, InvariantCulture);

        await Assert.That(upper).IsEqualTo("MIXED CASE");
        await Assert.That(lower).IsEqualTo("mixed case");
    }

    /// <summary>
    /// Negates boolean values in both conversion directions.
    /// </summary>
    /// <param name="input">The input boolean.</param>
    /// <param name="expected">The expected negated boolean.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    [Arguments(true, false)]
    [Arguments(false, true)]
    public async Task NegatesBooleanValues(bool input, bool expected)
    {
        var converter = new BoolNegationConverter();

        await Assert.That((bool)converter.Convert(input, typeof(bool), null!, InvariantCulture)).IsEqualTo(expected);
        await Assert.That((bool)converter.ConvertBack(input, typeof(bool), null!, InvariantCulture)).IsEqualTo(expected);
    }

    /// <summary>
    /// Compares values with optional inversion.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ComparesValuesForEquality()
    {
        var equality = new EqualityConverter();
        var objectEquality = new ObjectEqualsParameterConverter();

        await Assert.That((bool)equality.Convert(42, typeof(bool), "42", InvariantCulture)).IsTrue();
        await Assert.That((bool)equality.Convert(42, typeof(bool), "!42", InvariantCulture)).IsFalse();
        await Assert.That((bool)objectEquality.Convert("VALUE", typeof(bool), "value", InvariantCulture)).IsTrue();
        await Assert.That((bool)objectEquality.Convert("VALUE", typeof(bool), "!value", InvariantCulture)).IsFalse();
    }

    /// <summary>
    /// Reports simple and fully-qualified type names.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsObjectsToTypeNames()
    {
        var converter = new ObjectToTypeNameConverter();

        await Assert.That(converter.Convert(42, typeof(string), null!, InvariantCulture)).IsEqualTo("Int32");
        await Assert.That(converter.Convert(42, typeof(string), "full", InvariantCulture)).IsEqualTo("System.Int32");
    }

    /// <summary>
    /// Multiplies and divides numeric values symmetrically.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task MultipliesAndDividesNumericValues()
    {
        var converter = new MultiplierConverter();
        var converted = converter.Convert(6, typeof(double), "2.5", InvariantCulture);
        var convertedBack = converter.ConvertBack(converted, typeof(double), "2.5", InvariantCulture);

        await Assert.That((double)converted).IsEqualTo(15d).Within(0.00001d);
        await Assert.That((double)convertedBack).IsEqualTo(6d).Within(0.00001d);
    }

    /// <summary>
    /// Supports both value/parameter and multi-value comparisons.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Test]
    public async Task ComparesGreaterThanOrEqualValues()
    {
        var converter = new IsGreaterThanOrEqualToConverter();
        var singleResult = ((IValueConverter)converter).Convert(5, typeof(bool), 5, InvariantCulture);
        var multiResult = ((IMultiValueConverter)converter).Convert(new object[] { 4, 5 }, typeof(bool), null!, InvariantCulture);

        await Assert.That((bool)singleResult).IsTrue();
        await Assert.That((bool)multiResult).IsFalse();
    }
}

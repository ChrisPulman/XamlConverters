// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;

namespace CP.Xaml.Converters.Tests;

/// <summary>Tests culture-aware BCL, enum, and collection converters.</summary>
public class BclConverterTests
{
    /// <summary>The invariant culture used by converter tests.</summary>
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

    /// <summary>Converts primitive values in both directions.</summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsPrimitiveValues()
    {
        var converter = new ChangeTypeConverter();

        await Assert
            .That(converter.Convert("42", typeof(int), null, InvariantCulture))
            .IsEqualTo(TestValues.Answer);
        await Assert
            .That(converter.ConvertBack(TestValues.Answer, typeof(string), null, InvariantCulture))
            .IsEqualTo("42");
    }

    /// <summary>Converts GUID text and byte-array representations.</summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsGuidRepresentations()
    {
        var guid = Guid.Parse("a75dd9c5-b47d-4bf7-b547-946da756f6d7");
        var converter = new GuidConverter();

        var bytes = (byte[])converter.Convert(guid, typeof(byte[]), null, InvariantCulture);
        var restored = converter.ConvertBack(bytes, typeof(Guid), null, InvariantCulture);

        await Assert.That(bytes.Length).IsEqualTo(TestValues.GuidByteCount);
        await Assert.That(restored).IsEqualTo(guid);
    }

    /// <summary>Encodes and decodes Base64 text.</summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsBase64Representations()
    {
        var converter = new ByteArrayToBase64Converter();
        var encoded = converter.Convert(
            new byte[] { 1, TestValues.SecondByte, TestValues.ThirdByte },
            typeof(string),
            null,
            InvariantCulture);
        var decoded = (byte[])
            converter.ConvertBack(encoded, typeof(byte[]), null, InvariantCulture);

        await Assert.That(encoded).IsEqualTo("AQID");
        await Assert.That(decoded.Length).IsEqualTo(TestValues.SampleValueCount);
        await Assert.That(decoded[TestValues.ThirdByteIndex]).IsEqualTo(TestValues.ThirdByte);
    }

    /// <summary>Selects, searches, and joins collection values.</summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsCollectionValues()
    {
        var values = new[]
        {
            TestValues.FirstSampleValue,
            TestValues.SecondSampleValue,
            TestValues.ThirdSampleValue,
        };

        var contains = new CollectionContainsConverter().Convert(
            values,
            typeof(bool),
            "20",
            InvariantCulture);
        var item = new CollectionItemConverter().Convert(
            values,
            typeof(int),
            "1",
            InvariantCulture);
        var first = new CollectionFirstOrDefaultConverter().Convert(
            Array.Empty<int>(),
            typeof(int),
            TestValues.MissingSampleValue,
            InvariantCulture);
        var joined = new EnumerableToStringConverter().Convert(
            values,
            typeof(string),
            "|",
            InvariantCulture);

        await Assert.That((bool)contains).IsTrue();
        await Assert.That(item).IsEqualTo(TestValues.SecondSampleValue);
        await Assert.That(first).IsEqualTo(TestValues.MissingSampleValue);
        await Assert.That(joined).IsEqualTo("10|20|30");
    }

    /// <summary>Converts enum descriptions, flags, and declared values.</summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsEnumValues()
    {
        var description = new EnumDescriptionConverter();
        var flag = new EnumHasFlagConverter();
        var values = new EnumValuesConverter();

        var text = description.Convert(SampleFlags.Read, typeof(string), null, InvariantCulture);
        var restored = description.ConvertBack(
            "Can read",
            typeof(SampleFlags),
            null,
            InvariantCulture);
        var hasFlag = flag.Convert(
            SampleFlags.Read | SampleFlags.Write,
            typeof(bool),
            "Write",
            InvariantCulture);
        var enumValues = (Array)
            values.Convert(typeof(SampleFlags), typeof(IEnumerable), null, InvariantCulture);

        await Assert.That(text).IsEqualTo("Can read");
        await Assert.That(restored).IsEqualTo(SampleFlags.Read);
        await Assert.That((bool)hasFlag).IsTrue();
        await Assert.That(enumValues.Length).IsEqualTo(TestValues.SampleValueCount);
    }
}

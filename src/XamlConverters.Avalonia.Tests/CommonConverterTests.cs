// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Globalization;

namespace CP.Xaml.Converters.Avalonia.Tests;

/// <summary>Tests common Avalonia converter behavior and WPF feature parity.</summary>
public class CommonConverterTests
{
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

    /// <summary>Converts common BCL values in both directions.</summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsCommonBclValues()
    {
        var converter = new ChangeTypeConverter();

        var integer = converter.Convert("42", typeof(int), null, InvariantCulture);
        var text = converter.ConvertBack(42, typeof(string), null, InvariantCulture);

        await Assert.That(integer).IsEqualTo(42);
        await Assert.That(text).IsEqualTo("42");
    }

    /// <summary>Converts GUID and Base64 representations.</summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsGuidAndBase64Representations()
    {
        var guid = Guid.Parse("a75dd9c5-b47d-4bf7-b547-946da756f6d7");
        var guidConverter = new GuidConverter();
        var base64Converter = new ByteArrayToBase64Converter();

        var guidText = guidConverter.Convert(guid, typeof(string), "N", InvariantCulture);
        var parsedGuid = guidConverter.ConvertBack(guidText, typeof(Guid), null, InvariantCulture);
        var encoded = base64Converter.Convert(new byte[] { 1, 2, 3 }, typeof(string), null, InvariantCulture);
        var decoded = (byte[])base64Converter.ConvertBack(encoded, typeof(byte[]), null, InvariantCulture);

        await Assert.That(guidText).IsEqualTo("a75dd9c5b47d4bf7b547946da756f6d7");
        await Assert.That(parsedGuid).IsEqualTo(guid);
        await Assert.That(encoded).IsEqualTo("AQID");
        await Assert.That(decoded.Length).IsEqualTo(3);
        await Assert.That(decoded[2]).IsEqualTo((byte)3);
    }

    /// <summary>Selects, searches, and joins collection values.</summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsCollectionValues()
    {
        var values = new[] { 10, 20, 30 };
        var contains = new CollectionContainsConverter().Convert(values, typeof(bool), "20", InvariantCulture);
        var item = new CollectionItemConverter().Convert(values, typeof(int), "1", InvariantCulture);
        var first = new CollectionFirstOrDefaultConverter().Convert(Array.Empty<int>(), typeof(int), 99, InvariantCulture);
        var joined = new EnumerableToStringConverter().Convert(values, typeof(string), "|", InvariantCulture);

        await Assert.That((bool)contains).IsTrue();
        await Assert.That(item).IsEqualTo(20);
        await Assert.That(first).IsEqualTo(99);
        await Assert.That(joined).IsEqualTo("10|20|30");
    }

    /// <summary>Converts enum descriptions, values, and flags.</summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    public async Task ConvertsEnumValues()
    {
        var description = new EnumDescriptionConverter();
        var flag = new EnumHasFlagConverter();
        var values = new EnumValuesConverter();

        var text = description.Convert(SampleFlags.Read, typeof(string), null, InvariantCulture);
        var enumValue = description.ConvertBack("Can read", typeof(SampleFlags), null, InvariantCulture);
        var hasFlag = flag.Convert(SampleFlags.Read | SampleFlags.Write, typeof(bool), "Write", InvariantCulture);
        var enumValues = (Array)values.Convert(typeof(SampleFlags), typeof(IEnumerable), null, InvariantCulture);

        await Assert.That(text).IsEqualTo("Can read");
        await Assert.That(enumValue).IsEqualTo(SampleFlags.Read);
        await Assert.That((bool)hasFlag).IsTrue();
        await Assert.That(enumValues.Length).IsEqualTo(3);
    }

    /// <summary>Publishes common converters through the Avalonia resource dictionary.</summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    public async Task RegistersCommonConverters()
    {
        var dictionary = new ConvertersDictionary();

        await Assert.That(dictionary.ContainsKey("ChangeType")).IsTrue();
        await Assert.That(dictionary.ContainsKey("EnumDescription")).IsTrue();
        await Assert.That(dictionary.ContainsKey("Guid")).IsTrue();
    }
}

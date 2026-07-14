// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Windows;
using System.Windows.Markup;

[assembly: XmlnsDefinition("https://github.com/ChrisPulman/XamlConverters", "CP.Xaml.Converters")]
[assembly: XmlnsPrefix("https://github.com/ChrisPulman/XamlConverters", "converters")]

namespace CP.Xaml.Converters;

/// <summary>Converters Dictionary.</summary>
/// <seealso cref="ResourceDictionary" />
[Localizability(LocalizationCategory.Ignore)]
[Ambient]
[UsableDuringInitialization(true)]
public class ConvertersDictionary : ResourceDictionary
{
    /// <summary>The pack URI of the shared converter resource dictionary.</summary>
    private const string DictionaryUri =
        "pack://application:,,,/CP.Xaml.Converters;component/Resources/Converters.xaml";

    /// <summary>Initializes a new instance of the <see cref="ConvertersDictionary"/> class.</summary>
    public ConvertersDictionary() => Source = new(DictionaryUri, UriKind.Absolute);
}

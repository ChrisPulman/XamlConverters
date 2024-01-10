// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows;
using System.Windows.Markup;

[assembly: XmlnsDefinition("https://github.com/ChrisPulman/XamlConverters", "CP.Xaml.Converters")]
[assembly: XmlnsPrefix("https://github.com/ChrisPulman/XamlConverters", "converters")]

namespace CP.Xaml.Converters;

/// <summary>
/// Converters Dictionary.
/// </summary>
/// <seealso cref="ResourceDictionary" />
[Localizability(LocalizationCategory.Ignore)]
[Ambient]
[UsableDuringInitialization(true)]
public class ConvertersDictionary : ResourceDictionary
{
    private const string DictionaryUri = "pack://application:,,,/XamlConverters;component/Resources/Converters.xaml";

    /// <summary>
    /// Initializes a new instance of the <see cref="ConvertersDictionary"/> class.
    /// Default constructor defining <see cref="ResourceDictionary.Source"/> of the <c>XamlConverters</c> Converters dictionary.
    /// </summary>
    public ConvertersDictionary() => Source = new Uri(DictionaryUri, UriKind.Absolute);
}

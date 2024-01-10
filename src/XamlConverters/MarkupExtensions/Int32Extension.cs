// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows.Markup;

namespace CP.Xaml.Converters;

/// <summary>
/// Int32 Extension.
/// </summary>
/// <seealso cref="MarkupExtension"/>
/// <remarks>
/// Initializes a new instance of the <see cref="Int32Extension"/> class.
/// </remarks>
/// <param name="value">The value.</param>
public sealed class Int32Extension(int value) : MarkupExtension
{
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    public int Value { get; set; } = value;

    /// <summary>
    /// Provides the value.
    /// </summary>
    /// <param name="serviceProvider">The sp.</param>
    /// <returns>A Value.</returns>
    public override object ProvideValue(IServiceProvider serviceProvider) => Value;
}

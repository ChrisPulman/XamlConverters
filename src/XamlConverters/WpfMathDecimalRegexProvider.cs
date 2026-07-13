// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.
#if NET7_0_OR_GREATER
using System.Text.RegularExpressions;

namespace CP.Xaml.Converters;

/// <summary>Creates source-generated regular expressions for the WPF math converter.</summary>
internal static partial class WpfMathDecimalRegexProvider
{
    /// <summary>Creates the compiled decimal expression.</summary>
    /// <returns>The decimal expression.</returns>
    [GeneratedRegex(@"(\d+\.?\d*|\d*\.?\d+)")]
    internal static partial Regex Create();
}
#endif

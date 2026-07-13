// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.
#if NET7_0_OR_GREATER
using System.Text.RegularExpressions;

namespace CP.Xaml.Converters;

/// <summary>Creates source-generated regular expressions for the WPF arithmetic converter.</summary>
internal static partial class WpfArithmeticRegexProvider
{
    /// <summary>Creates the compiled arithmetic expression.</summary>
    /// <returns>The arithmetic expression.</returns>
    [GeneratedRegex("([+\\-*/]{1,1})\\s{0,}(\\-?[\\d\\.]+)")]
    internal static partial Regex Create();
}
#endif

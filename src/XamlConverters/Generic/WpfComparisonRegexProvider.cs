// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.
#if NET7_0_OR_GREATER
using System.Text.RegularExpressions;

namespace CP.Xaml.Converters;

/// <summary>Creates source-generated regular expressions for the WPF comparison converter.</summary>
internal static partial class WpfComparisonRegexProvider
{
    /// <summary>Creates the compiled comparison expression.</summary>
    /// <returns>The comparison expression.</returns>
    [GeneratedRegex("^(?<invert>!{0,1})(?<op>>=|<=|==|!=|>|<)\\s{0,}(?<rhs>.+)$", RegexOptions.Compiled)]
    internal static partial Regex Create();
}
#endif

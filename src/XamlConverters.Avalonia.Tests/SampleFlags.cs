// Copyright (c) 2022-2026 Chris Pulman. All rights reserved.
// Chris Pulman licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.ComponentModel;

namespace CP.Xaml.Converters.Avalonia.Tests;

/// <summary>Flags used to verify enum converter behavior.</summary>
[Flags]
internal enum SampleFlags
{
    /// <summary>No flags.</summary>
    None = 0,

    /// <summary>Read access.</summary>
    [Description("Can read")]
    Read = 1,

    /// <summary>Write access.</summary>
    Write = 2,
}

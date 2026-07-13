// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;

namespace CP.Xaml.Converters.Tests;

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

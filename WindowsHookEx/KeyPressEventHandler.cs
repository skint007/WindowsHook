﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace WindowsHookEx
{
    /// <summary>
    /// Represents a method that will handle the <see cref='System.Windows.Forms.Control.KeyPress'/>
    /// event of a <see cref='System.Windows.Forms.Control'/>.
    /// </summary>
    public delegate void KeyPressEventHandler(object sender, KeyPressEventArgs e);
}

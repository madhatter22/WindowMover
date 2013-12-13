﻿using System;

namespace WindowMover.Models
{
    [Flags]
    public enum KeyModifiers
    {
        Nomod = 0x0000,
        Alt = 0x0001,
        Ctrl = 0x0002,
        Shift = 0x0004,
        Win = 0x0008
    }
}
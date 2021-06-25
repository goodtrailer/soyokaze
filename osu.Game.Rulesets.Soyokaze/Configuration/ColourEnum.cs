// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Text;

namespace osu.Game.Rulesets.Soyokaze.Configuration
{
    public enum ColourEnum
    {
        None,

        BrightRed,      // 255, 0,   0
        BrightYellow,   // 255, 255, 0
        BrightGreen,    // 0,   255, 0
        BrightBlue,     // 0,   255, 255
        BrightPurple,   // 255, 0,   255

        Red,            // 208, 49,  45
        Orange,         // 255, 128, 0
        Yellow,         // 255, 196, 32
        Green,          // 42,  196, 42
        Blue,           // 64,  152, 255
        Purple,         // 152, 64,  255
        Pink,           // 255, 128, 196

        White,          // 255, 255, 255
        Black,          // 0,   0,   0
    }
}

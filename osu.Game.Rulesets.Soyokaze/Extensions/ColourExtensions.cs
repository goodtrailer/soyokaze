// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Game.Rulesets.Soyokaze.Configuration;

namespace osu.Game.Rulesets.Soyokaze.Extensions
{
    public static class ColourExtensions
    {
        public static Colour4 ToColour4(ColourEnum colourEnum)
        {
            switch (colourEnum)
            {
                case ColourEnum.BrightRed:
                    return new Colour4(255, 0, 0, 255);
                case ColourEnum.BrightYellow:
                    return new Colour4(255, 255, 0, 255);
                case ColourEnum.BrightGreen:
                    return new Colour4(0, 255, 0, 255);
                case ColourEnum.BrightBlue:
                    return new Colour4(0, 255, 255, 255);
                case ColourEnum.BrightPurple:
                    return new Colour4(255, 0, 255, 255);
                case ColourEnum.Red:
                    return new Colour4(208, 49, 45, 255);
                case ColourEnum.Orange:
                    return new Colour4(255, 128, 0, 255);
                case ColourEnum.Yellow:
                    return new Colour4(255, 196, 32, 255);
                case ColourEnum.Green:
                    return new Colour4(42, 196, 42, 255);
                case ColourEnum.Blue:
                    return new Colour4(64, 152, 255, 255);
                case ColourEnum.Purple:
                    return new Colour4(152, 64, 255, 255);
                case ColourEnum.Pink:
                    return new Colour4(255, 128, 196, 255);
                case ColourEnum.White:
                    return new Colour4(255, 255, 255, 255);
                case ColourEnum.Black:
                    return new Colour4(0, 0, 0, 255);
                default:
                    return new Colour4(0, 0, 0, 255);
            }
        }
    }
}

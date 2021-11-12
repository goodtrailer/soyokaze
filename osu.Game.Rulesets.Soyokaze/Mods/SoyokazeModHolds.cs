// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Graphics.Sprites;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Soyokaze.Mods
{
    public class SoyokazeModHolds : Mod
    {
        public override string Name => "Holds [Obsolete]";
        public override string Acronym => "HO";
        public override string Description => string.Empty;
        public override double ScoreMultiplier => 1.0;
        public override IconUsage? Icon => FontAwesome.Solid.Fingerprint;
    }
}

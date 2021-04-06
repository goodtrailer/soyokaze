// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Soyokaze.Objects;

namespace osu.Game.Rulesets.Soyokaze.Mods
{
    public class SoyokazeModNightcore : ModNightcore<SoyokazeHitObject>
    {
        public override double ScoreMultiplier => 1.12;
    }
}

// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Difficulty.Skills;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Soyokaze.Difficulty.Preprocessing;

namespace osu.Game.Rulesets.Soyokaze.Difficulty.Skills
{
    public class SkillRead : StrainDecaySkill
    {
        // slow decay and medium multiplier = buff consistently fast and cluttered maps
        protected override double SkillMultiplier => 7.0;
        protected override double StrainDecayBase => 0.3;

        public SkillRead(Mod[] mods)
            : base(mods)
        {
        }

        protected override double StrainValueOf(DifficultyHitObject current)
        {
            SoyokazeDifficultyHitObject soyokazeObject = current as SoyokazeDifficultyHitObject;

            return SkillMultiplier * (soyokazeObject.ButtonVariety - 1) / soyokazeObject.TotalDeltaTime;
        }
    }
}

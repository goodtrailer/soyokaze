// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Difficulty.Skills;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Soyokaze.Difficulty.Preprocessing;

namespace osu.Game.Rulesets.Soyokaze.Difficulty.Skills
{
    public class SkillSpeed : StrainDecaySkill
    {
        protected override double SkillMultiplier => 0.85;
        protected override double StrainDecayBase => 0.71;

        public SkillSpeed(Mod[] mods)
            : base(mods)
        {
        }

        protected override double StrainValueOf(DifficultyHitObject current)
        {
            SoyokazeDifficultyHitObject soyokazeObject = current as SoyokazeDifficultyHitObject;

            return SkillMultiplier / Math.Pow(soyokazeObject.ConsecutiveDeltaTime, 0.32);
        }
    }
}

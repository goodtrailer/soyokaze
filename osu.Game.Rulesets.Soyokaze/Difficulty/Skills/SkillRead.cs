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
        protected override double SkillMultiplier => 1.59;
        protected override double StrainDecayBase => 0.08;

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

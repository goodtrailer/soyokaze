// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Difficulty.Skills;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Soyokaze.Difficulty.Preprocessing;

namespace osu.Game.Rulesets.Soyokaze.Difficulty.Skills
{
    public class SkillSpeed : StrainDecaySkill
    {
        // fast decay and high multiplier = buff short and fast bursts/triples
        protected override double SkillMultiplier => 13.5;
        protected override double StrainDecayBase => 0.115;

        public SkillSpeed(Mod[] mods)
            : base(mods)
        {
        }

        protected override double StrainValueOf(DifficultyHitObject current)
        {
            SoyokazeDifficultyHitObject soyokazeObject = current as SoyokazeDifficultyHitObject;

            if (soyokazeObject.Consecutive < 2)
                return 0;

            return SkillMultiplier * (soyokazeObject.Consecutive - 1) / soyokazeObject.ConsecutiveDeltaTime;
        }
    }
}

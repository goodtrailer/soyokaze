// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Difficulty.Skills;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Soyokaze.Difficulty.Skills
{
    public class SkillSpeed : StrainSkill
    {
        protected override double SkillMultiplier => 1400;
        protected override double StrainDecayBase => 0.3;

        public SkillSpeed(Mod[] mods)
            : base(mods)
        {
        }

        protected override double StrainValueOf(DifficultyHitObject current)
        {

        }
    }
}

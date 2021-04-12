// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Difficulty;

namespace osu.Game.Rulesets.Soyokaze.Difficulty
{
    public class SoyokazeDifficultyAttributes : DifficultyAttributes
    {
        public double SpeedStrain;
        public double ReadStrain;
        public double ApproachRate;
        public double OverallDifficulty;
    }
}

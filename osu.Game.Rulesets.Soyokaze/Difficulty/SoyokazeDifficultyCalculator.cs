// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Difficulty.Skills;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Soyokaze.Difficulty.Preprocessing;
using osu.Game.Rulesets.Soyokaze.Difficulty.Skills;
using osu.Game.Rulesets.Soyokaze.Mods;

namespace osu.Game.Rulesets.Soyokaze.Difficulty
{
    public class SoyokazeDifficultyCalculator : DifficultyCalculator
    {
        private const double difficulty_multiplier = 0.445;

        public SoyokazeDifficultyCalculator(IRulesetInfo ruleset, IWorkingBeatmap beatmap)
            : base(ruleset, beatmap)
        {
        }

        protected override DifficultyAttributes CreateDifficultyAttributes(IBeatmap beatmap, Mod[] mods, Skill[] skills, double clockRate)
        {
            if (beatmap.HitObjects.Count == 0)
                return new DifficultyAttributes
                {
                    Mods = mods,
                };

            double speedRating = Math.Sqrt(skills[0].DifficultyValue()) * difficulty_multiplier;
            double readRating = Math.Sqrt(skills[1].DifficultyValue()) * difficulty_multiplier;

            double starRating = speedRating + readRating + (speedRating - readRating) / 3;

            int maxCombo = beatmap.HitObjects.Count;

            return new SoyokazeDifficultyAttributes
            {
                StarRating = starRating,
                Mods = mods,
                MaxCombo = maxCombo,
            };
        }

        protected override IEnumerable<DifficultyHitObject> CreateDifficultyHitObjects(IBeatmap beatmap, double clockRate)
        {
            for (int i = 0; i < beatmap.HitObjects.Count - SoyokazeDifficultyHitObject.COUNT; i++)
            {
                SoyokazeDifficultyHitObject difficultyObject = new SoyokazeDifficultyHitObject(clockRate, beatmap.HitObjects.Skip(i).Take(SoyokazeDifficultyHitObject.COUNT).ToArray());
                yield return difficultyObject;
            }
        }

        protected override Skill[] CreateSkills(IBeatmap beatmap, Mod[] mods, double clockrate) => new Skill[]
        {
            new SkillSpeed(mods),
            new SkillRead(mods),
        };

        protected override Mod[] DifficultyAdjustmentMods => new Mod[]
        {
            new SoyokazeModDoubleTime(),
            new SoyokazeModHalfTime(),
            new SoyokazeModEasy(),
            new SoyokazeModHardRock(),
        };
    }
}

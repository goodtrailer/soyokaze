// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Logging;
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

        // SR values which are good and should be exponentially curved between
        private const double x1 = 1.3;
        private const double x2 = 6.8;
        private static readonly double k = Math.Pow(x1, x2 / (x2 - x1)) * Math.Pow(x2, x1 / (x1 - x2));
        private static readonly double a = Math.Pow(x1 / x2, 1 / (x1 - x2));

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
            double baseStarRating = speedRating + readRating + (speedRating - readRating) / 3;

            double starRating = k * Math.Pow(a, baseStarRating);

            return new SoyokazeDifficultyAttributes
            {
                StarRating = starRating,
                Mods = mods,
                MaxCombo = beatmap.HitObjects.Count,
            };
        }

        protected override IEnumerable<DifficultyHitObject> CreateDifficultyHitObjects(IBeatmap beatmap, double clockRate)
        {
            var diffObjects = new List<DifficultyHitObject>();

            for (int i = SoyokazeDifficultyHitObject.MIN_COUNT; i <= beatmap.HitObjects.Count; i++)
            {
                int begin = Math.Max(0, i - SoyokazeDifficultyHitObject.MAX_COUNT);
                int end = i;
                var hitObjects = beatmap.HitObjects.Skip(begin).Take(end - begin).ToArray();
                var diffObject = new SoyokazeDifficultyHitObject(clockRate, hitObjects, diffObjects, i - SoyokazeDifficultyHitObject.MIN_COUNT);

                diffObjects.Add(diffObject);
            }

            return diffObjects;
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

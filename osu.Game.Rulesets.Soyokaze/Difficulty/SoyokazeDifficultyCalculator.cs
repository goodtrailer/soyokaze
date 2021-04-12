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
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Difficulty.Preprocessing;
using osu.Game.Rulesets.Soyokaze.Difficulty.Skills;
using osu.Game.Rulesets.Soyokaze.Objects;

namespace osu.Game.Rulesets.Soyokaze.Difficulty
{
    public class SoyokazeDifficultyCalculator : DifficultyCalculator
    {
        private const double difficulty_multiplier = 0.0675;

        public SoyokazeDifficultyCalculator(Ruleset ruleset, WorkingBeatmap beatmap)
            : base(ruleset, beatmap)
        {
        }

        protected override DifficultyAttributes CreateDifficultyAttributes(IBeatmap beatmap, Mod[] mods, Skill[] skills, double clockRate)
        {
            if (beatmap.HitObjects.Count == 0)
                return new DifficultyAttributes(mods, skills, 0);

            double speedRating = Math.Sqrt(skills[0].DifficultyValue()) * difficulty_multiplier;
            double readRating = Math.Sqrt(skills[1].DifficultyValue()) * difficulty_multiplier;
            double starRating = speedRating + readRating + (speedRating - readRating) / 3;

            HitWindows hitWindows = new HitWindows();
            hitWindows.SetDifficulty(beatmap.BeatmapInfo.BaseDifficulty.OverallDifficulty);
            double perfectWindow = hitWindows.WindowFor(HitResult.Perfect) / clockRate;

            double preempt = BeatmapDifficulty.DifficultyRange(beatmap.BeatmapInfo.BaseDifficulty.ApproachRate, 1800, 1200, 450) / clockRate;

            int maxCombo = beatmap.HitObjects.Count;

            return new SoyokazeDifficultyAttributes
            {
                SpeedStrain = speedRating,
                ReadStrain = readRating,
                StarRating = starRating,
                ApproachRate = preempt > 1200 ? (1800 - preempt) / 120 : (1200 - preempt) / 150 + 5,
                OverallDifficulty = (80 - perfectWindow) / 6,
                Mods = mods,
                MaxCombo = maxCombo,
                Skills = skills,
            };
        }

        protected override IEnumerable<DifficultyHitObject> CreateDifficultyHitObjects(IBeatmap beatmap, double clockRate)
        {
            SoyokazeHitObject[] hitObjects = beatmap.HitObjects as SoyokazeHitObject[];

            for (int i = 0; i < hitObjects.Length - SoyokazeDifficultyHitObject.COUNT; i++)
                yield return new SoyokazeDifficultyHitObject(clockRate, hitObjects.Skip(i).Take(SoyokazeDifficultyHitObject.COUNT).ToArray());
        }

        protected override Skill[] CreateSkills(IBeatmap beatmap, Mod[] mods) => new Skill[]
        {
            new SkillSpeed(mods),
            new SkillRead(mods),
        };
    }
}

// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Soyokaze.Scoring
{
    public class SoyokazeHitWindows : HitWindows
    {
        public static readonly DifficultyRange GREAT_WINDOW_RANGE = new DifficultyRange(80, 50, 25);
        public static readonly DifficultyRange OK_WINDOW_RANGE = new DifficultyRange(140, 100, 70);
        public static readonly DifficultyRange MEH_WINDOW_RANGE = new DifficultyRange(200, 150, 115);
        public static readonly DifficultyRange MISS_WINDOW_RANGE = new DifficultyRange(400, 400, 400);

        private Dictionary<HitResult, double> windows = new Dictionary<HitResult, double>();

        public override bool IsHitResultAllowed(HitResult result)
        {
            switch (result)
            {
                case HitResult.Great:
                case HitResult.Ok:
                case HitResult.Meh:
                case HitResult.Miss:
                    return true;
            }

            return false;
        }

        public override void SetDifficulty(double difficulty)
        {
            windows.Clear();
            windows.Add(HitResult.Great, IBeatmapDifficultyInfo.DifficultyRange(difficulty, GREAT_WINDOW_RANGE));
            windows.Add(HitResult.Ok, IBeatmapDifficultyInfo.DifficultyRange(difficulty, OK_WINDOW_RANGE));
            windows.Add(HitResult.Meh, IBeatmapDifficultyInfo.DifficultyRange(difficulty, MEH_WINDOW_RANGE));
            windows.Add(HitResult.Miss, IBeatmapDifficultyInfo.DifficultyRange(difficulty, MISS_WINDOW_RANGE));
        }

        public override double WindowFor(HitResult result) => windows[result];
    }
}

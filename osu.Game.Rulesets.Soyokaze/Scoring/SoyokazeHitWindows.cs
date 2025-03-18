// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Soyokaze.Scoring
{
    public class SoyokazeHitWindows : HitWindows
    {
        internal static readonly DifficultyRange[] SOYOKAZE_RANGES =
        {
            new DifficultyRange(HitResult.Great, 80, 50, 25),
            new DifficultyRange(HitResult.Ok, 140, 100, 70),
            new DifficultyRange(HitResult.Meh, 200, 150, 115),
            new DifficultyRange(HitResult.Miss, 400, 400, 400),
        };

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

        protected override DifficultyRange[] GetRanges() => SOYOKAZE_RANGES;
    }
}

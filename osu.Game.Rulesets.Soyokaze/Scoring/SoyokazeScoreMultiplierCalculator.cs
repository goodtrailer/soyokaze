// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Mods;

namespace osu.Game.Rulesets.Soyokaze.Scoring
{
    public class SoyokazeScoreMultiplierCalculator : ScoreMultiplierCalculator
    {
        public SoyokazeScoreMultiplierCalculator(ScoreMultiplierContext context)
            : base(context)
        {
            Single<SoyokazeModEasy>(hasMultiplier: 0.85);
            Single<SoyokazeModNoFail>(hasMultiplier: 0.5);
            Single<SoyokazeModHalfTime>(hasMultiplier: m => rateAdjustMultiplier(m.SpeedChange.Value));
            Single<SoyokazeModDaycore>(hasMultiplier: m => rateAdjustMultiplier(m.SpeedChange.Value));
            Single<SoyokazeModWindDown>(hasMultiplier: 0.5);
            Single<SoyokazeModStaccato>(hasMultiplier: 0.86);

            Single<SoyokazeModHardRock>(hasMultiplier: 1.06);
            Single<SoyokazeModDoubleTime>(hasMultiplier: m => rateAdjustMultiplier(m.SpeedChange.Value));
            Single<SoyokazeModNightcore>(hasMultiplier: m => rateAdjustMultiplier(m.SpeedChange.Value));
            Single<SoyokazeModWindUp>(hasMultiplier: 1.0);
            Single<SoyokazeModHidden>(hasMultiplier: 1.06);
            // SoyokazeModPerfect
            // SoyokazeModSuddenDeath

            Single<SoyokazeModHolds>(hasMultiplier: 1.0);
            Single<SoyokazeModDifficultyAdjust>(hasMultiplier: 0.5);
            // SoyokazeModRandom

            // SoyokazeModAutoplay
            // SoyokazeModCinema
        }

        // Copied from ManiaScoreMultiplierCalculator
        // https://github.com/ppy/osu/blob/fec525/osu.Game.Rulesets.Mania/Scoring/ManiaScoreMultiplierCalculator.cs#L88-L100
        private static double rateAdjustMultiplier(double speedChange)
        {
            // Round to the nearest multiple of 0.1.
            double value = (int)(speedChange * 10) / 10.0;

            // Offset back to 0.
            value -= 1;

            if (speedChange >= 1)
                return 1 + (value / 5);
            else
                return 0.6 + value;
        }
    }
}

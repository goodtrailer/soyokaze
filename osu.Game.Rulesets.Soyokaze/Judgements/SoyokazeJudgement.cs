// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Soyokaze.Judgements
{
    public class SoyokazeJudgement : Judgement
    {
        public override HitResult MaxResult => HitResult.Great;

        protected override double HealthIncreaseFor(HitResult result)
        {
            switch (result)
            {
                default:
                    return 0;

                case HitResult.Miss:
                case HitResult.LargeTickMiss:
                    return -DEFAULT_MAX_HEALTH_INCREASE;

                case HitResult.Meh:
                case HitResult.SmallTickMiss:
                    return -DEFAULT_MAX_HEALTH_INCREASE * 0.05;

                case HitResult.Ok:
                case HitResult.SmallBonus:
                case HitResult.SmallTickHit:
                    return DEFAULT_MAX_HEALTH_INCREASE * 0.5;

                case HitResult.Good:
                    return DEFAULT_MAX_HEALTH_INCREASE * 0.75;

                case HitResult.Great:
                case HitResult.LargeBonus:
                case HitResult.LargeTickHit:
                    return DEFAULT_MAX_HEALTH_INCREASE;

                case HitResult.Perfect:
                    return DEFAULT_MAX_HEALTH_INCREASE * 1.15f;
            }
        }
    }
}

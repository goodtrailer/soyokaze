// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Judgements;

namespace osu.Game.Rulesets.Soyokaze.Scoring
{
    public partial class SoyokazeScoreProcessor : ScoreProcessor
    {
        public SoyokazeScoreProcessor(Ruleset ruleset)
            : base(ruleset)
        {
        }

        protected override HitEvent CreateHitEvent(JudgementResult result)
        {
            var hitEvent = base.CreateHitEvent(result);

            if (result is SoyokazeHoldJudgementResult hr)
                hitEvent = new HitEvent(hr.TrueTimeOffset, hitEvent.Result, hitEvent.HitObject, hitEvent.LastHitObject, hitEvent.Position);

            return hitEvent;
        }
    }
}

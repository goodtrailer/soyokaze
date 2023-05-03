// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Soyokaze.Objects;

namespace osu.Game.Rulesets.Soyokaze.Judgements
{
    public class SoyokazeHoldJudgementResult : JudgementResult
    {
        public double TrueTimeOffset { get; set; }

        public SoyokazeHoldJudgementResult(Hold hitObject, Judgement judgement)
            : base(hitObject, judgement)
        {
        }
    }
}

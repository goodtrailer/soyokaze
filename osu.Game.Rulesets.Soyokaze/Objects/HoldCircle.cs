// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Soyokaze.Judgements;

namespace osu.Game.Rulesets.Soyokaze.Objects
{
    public class HoldCircle : HitCircle
    {
        public override Judgement CreateJudgement() => new SoyokazeIgnoreJudgement();
    }
}

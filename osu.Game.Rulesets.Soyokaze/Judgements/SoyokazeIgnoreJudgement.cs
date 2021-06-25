// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Soyokaze.Judgements
{
    public class SoyokazeIgnoreJudgement : SoyokazeJudgement
    {
        public override HitResult MaxResult => HitResult.IgnoreHit;
    }
}

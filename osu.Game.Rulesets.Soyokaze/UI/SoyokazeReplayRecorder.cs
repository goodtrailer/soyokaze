// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Soyokaze.Replays;
using osu.Game.Rulesets.UI;
using osu.Game.Scoring;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.UI
{
    public partial class SoyokazeReplayRecorder : ReplayRecorder<SoyokazeAction>
    {
        public SoyokazeReplayRecorder(Score score)
            : base(score)
        {
        }

        protected override ReplayFrame HandleFrame(Vector2 mousePosition, List<SoyokazeAction> actions, ReplayFrame previousFrame)
        {
            return new SoyokazeReplayFrame(Time.Current, mousePosition, actions.ToArray());
        }
    }
}

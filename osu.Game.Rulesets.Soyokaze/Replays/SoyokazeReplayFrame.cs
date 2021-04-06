// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Soyokaze.UI;

namespace osu.Game.Rulesets.Soyokaze.Replays
{
    public class SoyokazeReplayFrame : ReplayFrame
    {
        public List<SoyokazeAction> Actions = new List<SoyokazeAction>();

        public SoyokazeReplayFrame(double time, SoyokazeAction? button = null)
        {
            Time = time;
            if (button.HasValue)
                Actions.Add(button.Value);
        }
    }
}

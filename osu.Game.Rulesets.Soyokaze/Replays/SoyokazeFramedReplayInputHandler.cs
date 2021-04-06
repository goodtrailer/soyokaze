// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Input.StateChanges;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Soyokaze.UI;

namespace osu.Game.Rulesets.Soyokaze.Replays
{
    public class SoyokazeFramedReplayInputHandler : FramedReplayInputHandler<SoyokazeReplayFrame>
    {
        public SoyokazeFramedReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        protected override bool IsImportant(SoyokazeReplayFrame frame) => frame.Actions.Any();

        public override void CollectPendingInputs(List<IInput> inputs)
        {
            inputs.Add(new ReplayState<SoyokazeAction> { PressedActions = CurrentFrame?.Actions ?? new List<SoyokazeAction>() });
        }
    }
}

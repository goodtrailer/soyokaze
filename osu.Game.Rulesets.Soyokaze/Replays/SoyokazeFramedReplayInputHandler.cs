// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Input.StateChanges;
using osu.Framework.Utils;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Soyokaze.UI;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Replays
{
    public class SoyokazeFramedReplayInputHandler : FramedReplayInputHandler<SoyokazeReplayFrame>
    {
        public SoyokazeFramedReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        protected override bool IsImportant(SoyokazeReplayFrame frame) => true;

        protected Vector2 Position
        {
            get
            {
                var frame = CurrentFrame;

                if (frame == null)
                    return Vector2.Zero;

                return NextFrame != null ? Interpolation.ValueAt(CurrentTime.Value, frame.Position, NextFrame.Position, frame.Time, NextFrame.Time) : frame.Position;
            }
        }

        public override void CollectPendingInputs(List<IInput> inputs)
        {
            inputs.Add(new MousePositionAbsoluteInput { Position = GamefieldToScreenSpace(Position) });
            inputs.Add(new ReplayState<SoyokazeAction> { PressedActions = CurrentFrame?.Actions ?? new List<SoyokazeAction>() });
        }
    }
}

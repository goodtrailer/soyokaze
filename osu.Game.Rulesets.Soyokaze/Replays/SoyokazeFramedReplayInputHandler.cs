// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Diagnostics;
using System.Linq;
using osu.Framework.Utils;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Replays
{
    public class SoyokazeFramedReplayInputHandler : FramedReplayInputHandler<SoyokazeReplayFrame>
    {
        public SoyokazeFramedReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        protected override bool IsImportant(SoyokazeReplayFrame frame) => frame.Actions.Any();

        protected Vector2 Position
        {
            get
            {
                var frame = CurrentFrame;

                if (frame == null)
                    return Vector2.Zero;

                Debug.Assert(CurrentTime != null);

                return Interpolation.ValueAt(CurrentTime.Value, frame.Position, NextFrame.Position, frame.Time, NextFrame.Time);
            }
        }
    }
}

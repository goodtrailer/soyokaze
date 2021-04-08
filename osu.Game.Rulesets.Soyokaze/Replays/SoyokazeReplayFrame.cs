// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Extensions.EnumExtensions;
using osu.Game.Beatmaps;
using osu.Game.Replays.Legacy;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Replays.Types;
using osu.Game.Rulesets.Soyokaze.UI;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Replays
{
    public class SoyokazeReplayFrame : ReplayFrame, IConvertibleReplayFrame
    {
        public Vector2 Position;
        public List<SoyokazeAction> Actions = new List<SoyokazeAction>();

        private enum SoyokazeActionFlag
        {
            None = 0,
            Button0 = 0b_0000_0001,
            Button1 = 0b_0000_0010,
            Button2 = 0b_0000_0100,
            Button3 = 0b_0000_1000,
            Button4 = 0b_0001_0000,
            Button5 = 0b_0010_0000,
            Button6 = 0b_0100_0000,
            Button7 = 0b_1000_0000
        }

        public SoyokazeReplayFrame()
        {
        }

        public SoyokazeReplayFrame(double time, Vector2 position, params SoyokazeAction[] buttons)
            : base(time)
        {
            Position = position;
            Actions.AddRange(buttons);
        }

        public void FromLegacy(LegacyReplayFrame currentFrame, IBeatmap beatmap, ReplayFrame lastFrame = null)
        {
            Position = currentFrame.Position;
            SoyokazeActionFlag soyokazeButtonFlags = (SoyokazeActionFlag)currentFrame.ButtonState;

            if (soyokazeButtonFlags.HasFlagFast(SoyokazeActionFlag.Button0))
                Actions.Add(SoyokazeAction.Button0);
            if (soyokazeButtonFlags.HasFlagFast(SoyokazeActionFlag.Button1))
                Actions.Add(SoyokazeAction.Button1);
            if (soyokazeButtonFlags.HasFlagFast(SoyokazeActionFlag.Button2))
                Actions.Add(SoyokazeAction.Button2);
            if (soyokazeButtonFlags.HasFlagFast(SoyokazeActionFlag.Button3))
                Actions.Add(SoyokazeAction.Button3);
            if (soyokazeButtonFlags.HasFlagFast(SoyokazeActionFlag.Button4))
                Actions.Add(SoyokazeAction.Button4);
            if (soyokazeButtonFlags.HasFlagFast(SoyokazeActionFlag.Button5))
                Actions.Add(SoyokazeAction.Button5);
            if (soyokazeButtonFlags.HasFlagFast(SoyokazeActionFlag.Button6))
                Actions.Add(SoyokazeAction.Button6);
            if (soyokazeButtonFlags.HasFlagFast(SoyokazeActionFlag.Button7))
                Actions.Add(SoyokazeAction.Button7);
        }

        public LegacyReplayFrame ToLegacy(IBeatmap beatmap)
        {
            SoyokazeActionFlag soyokazeButtonFlags = SoyokazeActionFlag.None;

            if (Actions.Contains(SoyokazeAction.Button0))
                soyokazeButtonFlags |= SoyokazeActionFlag.Button0;
            if (Actions.Contains(SoyokazeAction.Button1))
                soyokazeButtonFlags |= SoyokazeActionFlag.Button1;
            if (Actions.Contains(SoyokazeAction.Button2))
                soyokazeButtonFlags |= SoyokazeActionFlag.Button2;
            if (Actions.Contains(SoyokazeAction.Button3))
                soyokazeButtonFlags |= SoyokazeActionFlag.Button3;
            if (Actions.Contains(SoyokazeAction.Button4))
                soyokazeButtonFlags |= SoyokazeActionFlag.Button4;
            if (Actions.Contains(SoyokazeAction.Button5))
                soyokazeButtonFlags |= SoyokazeActionFlag.Button5;
            if (Actions.Contains(SoyokazeAction.Button6))
                soyokazeButtonFlags |= SoyokazeActionFlag.Button6;
            if (Actions.Contains(SoyokazeAction.Button7))
                soyokazeButtonFlags |= SoyokazeActionFlag.Button7;

            return new LegacyReplayFrame(Time, Position.X, Position.Y, (ReplayButtonState)soyokazeButtonFlags);
        }
    }
}

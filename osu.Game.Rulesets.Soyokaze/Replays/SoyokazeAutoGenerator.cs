// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Soyokaze.Objects;

namespace osu.Game.Rulesets.Soyokaze.Replays
{
    public class SoyokazeAutoGenerator : AutoGenerator
    {
        protected Replay Replay;
        protected List<ReplayFrame> Frames => Replay.Frames;

        public new Beatmap<SoyokazeHitObject> Beatmap => (Beatmap<SoyokazeHitObject>)base.Beatmap;

        public SoyokazeAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
            Replay = new Replay();
        }

        public override Replay Generate()
        {
            Frames.Add(new SoyokazeReplayFrame());

            foreach (SoyokazeHitObject hitObject in Beatmap.HitObjects)
            {
                Frames.Add(new SoyokazeReplayFrame
                {
                    Time = hitObject.StartTime,
                    // todo: add required inputs and extra frames.
                });
            }

            return Replay;
        }
    }
}

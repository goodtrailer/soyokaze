// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.UI;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Replays
{
    public class SoyokazeAutoGenerator : AutoGenerator
    {
        private Replay replay;
        private LinkedList<double> releaseFrameTimes = new LinkedList<double>();
        private KeyPress[] keyPresses = new KeyPress[8];

        private const double default_press_duration = 75.0;
        private const double default_press_safety_buffer = 5.0;

        public new Beatmap<SoyokazeHitObject> Beatmap => (Beatmap<SoyokazeHitObject>)base.Beatmap;

        public SoyokazeAutoGenerator(IBeatmap beatmap, IReadOnlyList<Mod> mods)
            : base(beatmap)
        {
            replay = new Replay();
        }

        public override Replay Generate()
        {
            replay.Frames.Clear();
            releaseFrameTimes.Clear();
            for (int i = 0; i < keyPresses.Length; i++)
                keyPresses[i] = new KeyPress
                {
                    Start = -1.0,
                    End = -1.0,
                };

            addOrderedFrame(new SoyokazeReplayFrame(-1000, Vector2.Zero));

            for (int i = 0; i < Beatmap.HitObjects.Count; i++)
            {
                SoyokazeHitObject currentObject = Beatmap.HitObjects[i];
                SoyokazeAction currentButton = currentObject.Button;

                for (var node = releaseFrameTimes.First; node != null; node = node.Next)
                {
                    if (node.Value >= currentObject.StartTime)
                        continue;

                    addOrderedFrame(generateReplayFrame(node.Value));
                    releaseFrameTimes.Remove(node);
                }

                double pressDuration;
                switch (currentObject)
                {
                    case Hold hold:
                        pressDuration = hold.Duration;
                        break;
                    default:
                        pressDuration = default_press_duration;
                        break;
                }

                // Determine press duration
                for (int j = i + 1; j < Beatmap.HitObjects.Count; j++)
                {
                    SoyokazeHitObject nextObject = Beatmap.HitObjects[j];
                    if (nextObject.Button == currentButton)
                    {
                        double delta = nextObject.StartTime - currentObject.StartTime;
                        double safeDelta = (delta <= default_press_safety_buffer) ? (delta / 2) : (delta - default_press_safety_buffer);

                        if (pressDuration > safeDelta)
                            pressDuration = safeDelta;

                        break;
                    }
                }

                keyPresses[(int)currentButton].Start = currentObject.StartTime;
                keyPresses[(int)currentButton].End = currentObject.StartTime + pressDuration;
                addOrderedFrame(generateReplayFrame(currentObject.StartTime));
                releaseFrameTimes.AddLast(currentObject.StartTime + pressDuration);
            }

            for (var node = releaseFrameTimes.First; node != null; node = node.Next)
                addOrderedFrame(generateReplayFrame(node.Value));

            return replay;
        }

        private struct KeyPress
        {
            public double Start;
            public double End;
        }

        private SoyokazeReplayFrame generateReplayFrame(double time)
        {
            List<SoyokazeAction> buttons = new List<SoyokazeAction>();

            for (int i = 0; i < keyPresses.Length; i++)
                if (keyPresses[i].Start <= time && time < keyPresses[i].End)
                    buttons.Add((SoyokazeAction)i);

            return new SoyokazeReplayFrame(time, Vector2.Zero, buttons.ToArray());
        }

        private class FrameComparer : IComparer<ReplayFrame>
        {
            public int Compare(ReplayFrame x, ReplayFrame y)
            {
                if (x.Time < y.Time)
                    return -1;
                else if (x.Time == y.Time)
                    return 0;
                else
                    return 1;
            }
        }

        private void addOrderedFrame(SoyokazeReplayFrame frame)
        {
            #region Unused binary search algorithm

            /* Below is a binary search algorithm that osu!standard uses, but in
             * practice, I'm pretty sure it's actually slower than just working
             * backwards, because  generally speaking, mismatched frames are gonna
             * be at the back and not at the front of the List when working with
             * autoplay generation.
                int index = Frames.BinarySearch(frame, new FrameComparer());
                if (index < 0)
                    index = ~index;
                else
                    // Go to the first index which is actually bigger
                    for (; index < Frames.Count && frame.Time == Frames[index].Time; index++) ;
            */

            #endregion

            int index = replay.Frames.Count;
            while (index > 0 && replay.Frames[index - 1].Time > frame.Time)
                index--;

            replay.Frames.Insert(index, frame);
        }
    }
}

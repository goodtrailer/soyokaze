// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.UI;

namespace osu.Game.Rulesets.Soyokaze.Difficulty.Preprocessing
{
    public class SoyokazeDifficultyHitObject : DifficultyHitObject
    {
        public const int MIN_COUNT = 2;
        public const int MAX_COUNT = 8;

        public SoyokazeAction Button = (SoyokazeAction)(-1);
        public int ButtonVariety;
        public double TotalDeltaTime;
        public double ConsecutiveDeltaTime;

        public SoyokazeDifficultyHitObject(double clockRate, HitObject[] hitObjects, List<DifficultyHitObject> diffObjects, int index)
            : base(hitObjects[^1], hitObjects[^2], clockRate, diffObjects, index)
        {
            Button = (hitObjects[^1] as SoyokazeHitObject).Button;

            bool[] counted = new bool[8];
            for (int i = 0; i < hitObjects.Length; i++)
            {
                if (!(hitObjects[i] is SoyokazeHitObject obj) || counted[(int)obj.Button])
                    continue;

                counted[(int)obj.Button] = true;
                ButtonVariety++;
            }

            SoyokazeHitObject lastConsecutive = null;
            for (int i = 2; i < hitObjects.Length; i++)
            {
                if (!(hitObjects[^i] is SoyokazeHitObject obj) || obj.Button != Button)
                    continue;

                lastConsecutive = obj;
                break;
            }
            TotalDeltaTime = Math.Max((hitObjects[^1].StartTime - hitObjects[0].StartTime) / clockRate, 1);
            ConsecutiveDeltaTime = Math.Max((hitObjects[^1].StartTime - lastConsecutive?.StartTime ?? -1e6) / clockRate, 1);
        }
    }
}

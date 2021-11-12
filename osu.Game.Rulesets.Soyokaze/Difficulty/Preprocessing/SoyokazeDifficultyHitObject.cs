// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.UI;

namespace osu.Game.Rulesets.Soyokaze.Difficulty.Preprocessing
{
    public class SoyokazeDifficultyHitObject : DifficultyHitObject
    {
        public const int COUNT = 8;

        public SoyokazeAction Button = (SoyokazeAction)(-1);
        public int ButtonVariety = 0;
        public double TotalDeltaTime = 0.0;
        public double ConsecutiveDeltaTime = 0.0;

        public SoyokazeDifficultyHitObject(double clockRate, params HitObject[] hitObjects)
            : base(hitObjects[^1], hitObjects[^2], clockRate)
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

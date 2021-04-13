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
        public int Consecutive = 0;
        public int ButtonVariety = 0;
        public double TotalDeltaTime = 0.0;
        public double ConsecutiveDeltaTime = 0.0;

        public SoyokazeDifficultyHitObject(double clockRate, params HitObject[] hitObjects)
            : base(hitObjects[COUNT - 1], hitObjects[COUNT - 2], clockRate)
        {
            Button = (hitObjects[COUNT - 1] as SoyokazeHitObject).Button;


            for (int i = hitObjects.Length - 1; i >= 0; i--)
            {
                if ((hitObjects[i] as SoyokazeHitObject).Button != Button)
                    break;
                Consecutive++;
            }

            bool[] counted = new bool[COUNT]; ;
            foreach (SoyokazeHitObject hitObject in hitObjects)
            {
                if (counted[(int)hitObject.Button])
                    continue;
                counted[(int)hitObject.Button] = true;
                ButtonVariety++;
            }

            TotalDeltaTime = Math.Max((hitObjects[COUNT - 1].StartTime - hitObjects[0].StartTime) / clockRate, 1);
            ConsecutiveDeltaTime = Math.Max((hitObjects[COUNT - 1].StartTime - hitObjects[COUNT - Consecutive].StartTime) / clockRate, 1);
        }
    }
}

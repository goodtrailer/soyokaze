// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.UI;

namespace osu.Game.Rulesets.Soyokaze.Difficulty.Preprocessing
{
    public class SoyokazeDifficultyHitObject : DifficultyHitObject
    {
        private SoyokazeHitObject[] hitObjects;

        public const int COUNT = 8;

        public SoyokazeAction Button => hitObjects[COUNT - 1].Button;

        private int consecutive = -1;
        public int Consecutive
        {
            get
            {
                if (consecutive < 0)
                {
                    int count = 1;
                    for (int i = hitObjects.Length - 2; i >= 0; i++)
                    {
                        if (hitObjects[i].Button != Button)
                            break;
                        count++;
                    }
                    consecutive = count;
                }
                return consecutive;
            }
        }

        private int buttonVariety = -1;
        public int ButtonVariety
        {
            get
            {
                if (buttonVariety < 0)
                {
                    int variety = 0;
                    bool[] counted = new bool[COUNT]; ;

                    foreach (SoyokazeHitObject hitObject in hitObjects)
                    {
                        if (counted[(int)hitObject.Button])
                            continue;
                        counted[(int)hitObject.Button] = true;
                        variety++;
                    }

                    buttonVariety = variety;
                }
                return buttonVariety;
            }
        }

        public double TotalDeltaTime => hitObjects[COUNT - 1].StartTime - hitObjects[0].StartTime;

        public double ConsecutiveDeltaTime => hitObjects[COUNT - 1].StartTime - hitObjects[COUNT - Consecutive].StartTime;

        public SoyokazeDifficultyHitObject(double clockRate, params SoyokazeHitObject[] hitObjects)
            : base(hitObjects[COUNT - 1], hitObjects[COUNT - 2], clockRate)
        {
            this.hitObjects = hitObjects;
        }
    }
}

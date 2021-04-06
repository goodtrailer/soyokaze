// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Utils;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.UI;

namespace osu.Game.Rulesets.Soyokaze.Mods
{
    public class SoyokazeModRandom : ModRandom, IApplicableToBeatmap
    {
        public override string Description => @"Shuffle around the notes!";

        public void ApplyToBeatmap(IBeatmap beatmap)
        {
            foreach (var obj in beatmap.HitObjects)
            {
                if (obj is SoyokazeHitObject hitObject)
                    hitObject.Button = (SoyokazeAction)RNG.Next(8);
            }
        }
    }
}

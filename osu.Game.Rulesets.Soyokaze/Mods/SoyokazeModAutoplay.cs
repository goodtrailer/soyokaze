// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Soyokaze.Replays;
using osu.Game.Scoring;
using osu.Game.Users;

namespace osu.Game.Rulesets.Soyokaze.Mods
{
    public class SoyokazeModAutoplay : ModAutoplay
    {
        public override Score CreateReplayScore(IBeatmap beatmap, IReadOnlyList<Mod> mods) => new Score
        {
            ScoreInfo = new ScoreInfo
            {
                User = new User { Username = "goodtrailer's super-duper autoplay bot" },
            },
            Replay = new SoyokazeAutoGenerator(beatmap, mods).Generate(),
        };
    }
}

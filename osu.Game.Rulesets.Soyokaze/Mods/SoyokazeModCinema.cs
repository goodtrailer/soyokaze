﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.Replays;

namespace osu.Game.Rulesets.Soyokaze.Mods
{
    public class SoyokazeModCinema : ModCinema<SoyokazeHitObject>
    {
        public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
        {
            var replay = new SoyokazeAutoGenerator(beatmap, mods).Generate();
            var user = new ModCreatedUser { Username = "goodtrailer's really cool theater bot" };
            return new ModReplayData(replay, user);
        }
    }
}

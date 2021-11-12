// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Graphics.Sprites;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Soyokaze.Beatmaps;

namespace osu.Game.Rulesets.Soyokaze.Mods
{
    public class SoyokazeModStaccato : Mod, IApplicableToBeatmapConverter
    {
        public override string Name => "Staccato";
        public override string Acronym => "ST";
        public override string Description => "We hate hold notes!";
        public override double ScoreMultiplier => 0.86;
        public override IconUsage? Icon => FontAwesome.Regular.DotCircle;
        public override ModType Type => ModType.DifficultyReduction;

        public void ApplyToBeatmapConverter(IBeatmapConverter converter)
        {
            var soyokazeConverter = converter as SoyokazeBeatmapConverter;
            soyokazeConverter.CreateHolds.Value = false;
        }
    }
}

// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Graphics.Sprites;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Soyokaze.Beatmaps;

namespace osu.Game.Rulesets.Soyokaze.Mods
{
    public class SoyokazeModHolds : Mod, IApplicableToBeatmapConverter
    {
        public override string Name => "Holds";
        public override string Acronym => "HO";
        public override string Description => "O Lord, a rhythm game!?";
        public override double ScoreMultiplier => 1.09;
        public override IconUsage? Icon => FontAwesome.Solid.Fingerprint;
        public override ModType Type => ModType.DifficultyIncrease;

        public void ApplyToBeatmapConverter(IBeatmapConverter converter)
        {
            var soyokazeConverter = converter as SoyokazeBeatmapConverter;
            soyokazeConverter.CreateHolds.Value = true;
        }
    }
}

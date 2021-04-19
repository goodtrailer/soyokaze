// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Soyokaze.Mods
{
    public class SoyokazeModEasy : ModEasyWithExtraLives
    {
        public override double ScoreMultiplier => 0.85;

        public override string Description => "Larger circles, more forgiving HP drain, less accuracy required, and multiple lives. As a bonus, it makes the map impossible to read!";
    }
}

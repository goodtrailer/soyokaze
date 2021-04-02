// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Graphics.Sprites;

namespace osu.Game.Rulesets.Soyokaze.Skinning.Defaults
{
    public class DefaultHitCircleText : SpriteText
    {
        public DefaultHitCircleText()
        {
            Shadow = true;
            Font = FontUsage.Default.With(size: 40);
            UseFullGlyphHeight = true;
        }
    }
}

// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.UI
{
    public class SoyokazeCursorContainer : GameplayCursorContainer
    {
        protected override Drawable CreateCursor() => new SpriteIcon
        {
            Scale = new Vector2(25f),
            Origin = Anchor.Centre,
            Icon = FontAwesome.Solid.Plus,
        };
    }
}

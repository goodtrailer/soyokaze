// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Game.Rulesets.Soyokaze.Skinning;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Soyokaze.UI
{
    public class SoyokazeCursorContainer : GameplayCursorContainer
    {
        protected override Drawable CreateCursor() => new SkinnableCursor();
    }
}

// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Game.Rulesets.Soyokaze.Skinning;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Soyokaze.UI
{
    /* DEPRECATED: soyokaze! no longer has a cursor on screen
     * Originally, smoking was planned to be implemented, but after some testing
     * I realized that having to reach over and grab your mouse is an absolute
     * pain.
     */
    public class SoyokazeCursorContainer : GameplayCursorContainer
    {
        protected override Drawable CreateCursor() => new SkinnableCursor();
    }
}

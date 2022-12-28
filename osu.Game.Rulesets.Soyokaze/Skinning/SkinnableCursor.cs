// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Soyokaze.Skinning.Defaults;
using osu.Game.Skinning;

namespace osu.Game.Rulesets.Soyokaze.Skinning
{
    // DEPRECATED: see SoyokazeCursorContainer
    public partial class SkinnableCursor : Container
    {
        public SkinnableCursor()
        {
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(new SkinnableDrawable(new SoyokazeSkinComponentLookup(SoyokazeSkinComponents.Cursor), _ => new DefaultCursor()));
        }
    }
}

// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace osu.Game.Rulesets.Soyokaze.Skinning.Defaults
{
    public abstract partial class AbstractDefaultComponent : CompositeDrawable
    {
        public virtual Texture Texture { get; init; }

        public AbstractDefaultComponent()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        protected abstract SoyokazeSkinComponents Component { get; }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            var lookup = new SoyokazeSkinComponentLookup(Component);
            AddInternal(new Sprite
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Texture = Texture ?? textures.Get(lookup.StoreName),
            });
        }
    }
}

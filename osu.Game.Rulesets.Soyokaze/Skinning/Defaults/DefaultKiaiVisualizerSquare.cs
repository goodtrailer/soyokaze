// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Game.Skinning;

namespace osu.Game.Rulesets.Soyokaze.Skinning.Defaults
{
    public partial class DefaultKiaiVisualizerSquare : CompositeDrawable
    {
        public DefaultKiaiVisualizerSquare()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures, ISkinSource skin)
        {
            var textureName = new SoyokazeSkinComponentLookup(SoyokazeSkinComponents.KiaiVisualizerSquare).LookupName;
            AddInternal(new Sprite
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Texture = skin.GetTexture(textureName) ?? textures.Get(textureName),
            });
        }
    }
}

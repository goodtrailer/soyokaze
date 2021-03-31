// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osu.Game.Rulesets.Objects.Drawables;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables.Pieces
{
    public class DrawableApproachCirclePiece : CompositeDrawable
    {
        [Resolved]
        private DrawableHitObject drawableObject { get; set; }

        private readonly Bindable<Color4> accentColour = new Bindable<Color4>();

        public override bool RemoveWhenNotAlive => false;

        public DrawableApproachCirclePiece()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            AddInternal(new SpriteIcon
            {
                RelativeSizeAxes = Axes.Both,
                Icon = FontAwesome.Regular.Circle,
            });

            accentColour.BindTo(drawableObject.AccentColour);
            accentColour.BindValueChanged(colourChanged => Colour = colourChanged.NewValue, true);
        }
    }
}

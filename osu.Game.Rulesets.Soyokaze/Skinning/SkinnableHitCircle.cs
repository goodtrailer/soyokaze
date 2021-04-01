// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Skinning.Defaults;
using osu.Game.Skinning;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Soyokaze.Skinning
{
    public class SkinnableHitCircle : Container
    {
        [Resolved]
        private DrawableHitObject drawableObject { get; set; }

        private readonly Bindable<Color4> accentColour = new Bindable<Color4>();

        private Drawable hitCircle;
        private Drawable hitCircleOverlay;

        public SkinnableHitCircle()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Masking = true;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                hitCircle = new SkinnableDrawable(new SoyokazeSkinComponent(SoyokazeSkinComponents.HitCircle), _ => new DefaultHitCircle()),
                hitCircleOverlay = new SkinnableDrawable(new SoyokazeSkinComponent(SoyokazeSkinComponents.HitCircleOverlay), _ => new DefaultHitCircleOverlay()),
            };

            accentColour.BindTo(drawableObject.AccentColour);
            accentColour.BindValueChanged(colourChanged => hitCircle.Colour = colourChanged.NewValue, true);
        }
    }
}

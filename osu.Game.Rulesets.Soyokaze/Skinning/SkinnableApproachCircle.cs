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
    public class SkinnableApproachCircle : Container
    {
        [Resolved]
        private DrawableHitObject drawableObject { get; set; }

        private readonly Bindable<Color4> accentColour = new Bindable<Color4>();

        private Drawable approachCircle;
        public override bool RemoveWhenNotAlive => false;

        public SkinnableApproachCircle()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(approachCircle = new SkinnableDrawable(new SoyokazeSkinComponent(SoyokazeSkinComponents.ApproachCircle), _ => new DefaultApproachCircle()));

            accentColour.BindTo(drawableObject.AccentColour);
            accentColour.BindValueChanged(colourChanged => approachCircle.Colour = colourChanged.NewValue, true);
        }
    }
}

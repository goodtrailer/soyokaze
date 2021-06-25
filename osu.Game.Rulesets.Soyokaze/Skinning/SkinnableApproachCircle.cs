// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.Extensions;
using osu.Game.Rulesets.Soyokaze.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Skinning.Defaults;
using osu.Game.Skinning;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Soyokaze.Skinning
{
    public class SkinnableApproachCircle : Container
    {
        public override bool RemoveWhenNotAlive => false;

        [Resolved]
        private DrawableHitObject drawableObject { get; set; }

        private readonly Bindable<Color4> accentColourBindable = new Bindable<Color4>();
        private readonly Bindable<ColourEnum> highlightColourEnumBindable = new Bindable<ColourEnum>();
        private readonly Bindable<bool> highlightBindable = new Bindable<bool>();
        private Drawable approachCircle;
        private Colour4 highlightColour;

        public SkinnableApproachCircle()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(ISkinSource skin, SoyokazeConfigManager cm)
        {
            AddInternal(approachCircle = new SkinnableDrawable(new SoyokazeSkinComponent(SoyokazeSkinComponents.ApproachCircle), _ => new DefaultApproachCircle()));

            accentColourBindable.BindTo(drawableObject.AccentColour);
            accentColourBindable.BindValueChanged(colourChanged =>
            {
                if (!highlightBindable.Value)
                    approachCircle.Colour = colourChanged.NewValue;
            });

            if (drawableObject is DrawableHoldCircle)
            {
                cm?.BindWith(SoyokazeConfig.HoldHighlightColour, highlightColourEnumBindable);
                highlightColourEnumBindable.BindValueChanged(colourEnumChanged =>
                {
                    if (colourEnumChanged.NewValue == ColourEnum.None)
                        highlightColour = skin.GetConfig<SoyokazeSkinColour, Colour4>(SoyokazeSkinColour.HoldHighlight)?.Value ?? Colour4.Lime;
                    else
                        highlightColour = ColourExtensions.ToColour4(colourEnumChanged.NewValue);

                    if (highlightBindable.Value)
                        approachCircle.Colour = highlightColour;
                });

                cm?.BindWith(SoyokazeConfig.HighlightHolds, highlightBindable);
                highlightBindable.BindValueChanged(valueChanged =>
                {
                    if (valueChanged.NewValue)
                        approachCircle.Colour = highlightColour;
                    else
                        approachCircle.Colour = accentColourBindable.Value;
                }, true);
            }
        }
    }
}

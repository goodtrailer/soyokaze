// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.Extensions;
using osu.Game.Rulesets.Soyokaze.Skinning.Defaults;
using osu.Game.Skinning;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Soyokaze.Skinning
{
    public class SkinnableHoldProgress : Container
    {
        public override bool RemoveWhenNotAlive => false;
        public Bindable<double> Current { get => circularProgress.Current; set => circularProgress.Current = value; }

        [Resolved]
        private DrawableHitObject drawableObject { get; set; }

        private readonly Bindable<Color4> accentColourBindable = new Bindable<Color4>();
        private readonly Bindable<bool> highlightBindable = new Bindable<bool>();
        private readonly Bindable<ColourEnum> highlightColourEnumBindable = new Bindable<ColourEnum>();
        private CircularProgress circularProgress;
        private SkinnableDrawable background;
        private Colour4 highlightColour;

        public SkinnableHoldProgress()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            circularProgress = new CircularProgress
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Scale = new Vector2(0.5f),
                InnerRadius = 1f,
                Current = { Value = 0 },
            };

            background = new SkinnableDrawable(new SoyokazeSkinComponent(SoyokazeSkinComponents.HoldOverlay), _ => new DefaultHoldOverlay());
        }

        [BackgroundDependencyLoader]
        private void load(ISkinSource skin, SoyokazeConfigManager cm)
        {
            InternalChildren = new Drawable[]
            {
                background,
                circularProgress,
            };

            accentColourBindable.BindTo(drawableObject.AccentColour);
            accentColourBindable.BindValueChanged(colourChanged =>
            {
                if (!highlightBindable.Value)
                    circularProgress.Colour = colourChanged.NewValue;
            }, true);

            cm?.BindWith(SoyokazeConfig.HoldHighlightColour, highlightColourEnumBindable);
            highlightColourEnumBindable.BindValueChanged(colourEnumChanged =>
            {
                if (colourEnumChanged.NewValue == ColourEnum.None)
                    highlightColour = skin.GetConfig<SoyokazeSkinColour, Colour4>(SoyokazeSkinColour.HoldHighlight)?.Value ?? Colour4.Lime;
                else
                    highlightColour = ColourExtensions.ToColour4(colourEnumChanged.NewValue);

                if (highlightBindable.Value)
                    circularProgress.Colour = highlightColour;
            }, true);

            cm?.BindWith(SoyokazeConfig.HighlightHolds, highlightBindable);
            highlightBindable.BindValueChanged(valueChanged =>
            {
                if (valueChanged.NewValue)
                    circularProgress.Colour = highlightColour;
                else
                    circularProgress.Colour = accentColourBindable.Value;
            }, true);
        }

        public TransformSequence<CircularProgress> FillTo(double newValue, double duration = 0, Easing easing = Easing.None) => circularProgress.FillTo(newValue, duration, easing);
    }
}

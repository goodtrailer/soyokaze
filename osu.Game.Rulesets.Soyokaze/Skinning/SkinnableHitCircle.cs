// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Skinning.Defaults;
using osu.Game.Skinning;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Soyokaze.Skinning
{
    public class SkinnableHitCircle : Container
    {
        [Resolved]
        private DrawableHitObject drawableObject { get; set; }

        private readonly Bindable<Color4> accentColourBindable = new Bindable<Color4>();
        private readonly Bindable<int> indexInCurrentComboBindable = new Bindable<int>();

        private SkinnableDrawable hitCircle;
        private SkinnableDrawable hitCircleOverlay;
        private SkinnableSpriteText hitCircleText;

        private const bool use_default_text = false;

        public SkinnableHitCircle()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Masking = true;

            hitCircle = new SkinnableDrawable(
                new SoyokazeSkinComponent(SoyokazeSkinComponents.HitCircle),
                _ => new DefaultHitCircle()
            );

            hitCircleOverlay = new SkinnableDrawable(
                new SoyokazeSkinComponent(SoyokazeSkinComponents.HitCircleOverlay),
                _ => new DefaultHitCircleOverlay()
            );

            hitCircleText = new SkinnableSpriteText(
                new SoyokazeSkinComponent(SoyokazeSkinComponents.HitCircleText),
                _ => new DefaultHitCircleText() { Alpha = use_default_text ? 1 : 0 },
                confineMode: ConfineMode.NoScaling
            );
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                hitCircle,
                hitCircleOverlay,
                hitCircleText,
            };

            DrawableSoyokazeHitObject drawableSoyokazeObject = (DrawableSoyokazeHitObject)drawableObject;

            accentColourBindable.BindTo(drawableObject.AccentColour);
            indexInCurrentComboBindable.BindTo(drawableSoyokazeObject.IndexInCurrentComboBindable);

            accentColourBindable.BindValueChanged(colourChanged => hitCircle.Colour = colourChanged.NewValue, true);
            indexInCurrentComboBindable.BindValueChanged(indexChanged => hitCircleText.Text = (indexChanged.NewValue + 1).ToString(), true);

            drawableObject.ApplyCustomUpdateState += updateState;
        }

        private void updateState(DrawableHitObject drawableObject, ArmedState state)
        {
            // fade out text even quicker
            switch (state)
            {
                case ArmedState.Hit:
                    hitCircleText.FadeOut();
                    break;
            }
        }
    }
}

﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Transforms;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.Extensions;
using osu.Game.Rulesets.Soyokaze.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Skinning.Defaults;
using osu.Game.Rulesets.Soyokaze.UI;
using osu.Game.Skinning;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Soyokaze.Skinning
{
    public partial class SkinnableHoldProgress : Container
    {
        public override bool RemoveWhenNotAlive => false;
        public float Progress
        {
            get => progressMask.Scale.Y;
            set => progressMask.Size = new Vector2(1f, value);
        }

        [Resolved]
        private DrawableHitObject drawableObject { get; set; }

        private readonly Bindable<Color4> accentColourBindable = new Bindable<Color4>();
        private readonly Bindable<SoyokazeAction> buttonBindable = new Bindable<SoyokazeAction>();
        private readonly Bindable<bool> highlightBindable = new Bindable<bool>();
        private readonly Bindable<ColourEnum> highlightColourEnumBindable = new Bindable<ColourEnum>();
        private Container progressContainer;
        private Container progressMask;
        private SkinnableDrawable progress;
        private SkinnableDrawable background;
        private Color4 highlightColour;

        public SkinnableHoldProgress()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            progressContainer = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    progress = new SkinnableDrawable(
                        new SoyokazeSkinComponentLookup(SoyokazeSkinComponents.HoldOverlay),
                        _ => new DefaultHoldOverlay()
                    )
                    {
                        RelativeSizeAxes = Axes.None,
                        AutoSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Alpha = 0f,
                    },
                    progressMask = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.BottomCentre,
                        Origin = Anchor.BottomCentre,
                        Size = new Vector2(1f, 0f),
                        Masking = true,
                        Child = progress.CreateProxy(),
                    },
                },
            };

            background = new SkinnableDrawable(
                new SoyokazeSkinComponentLookup(SoyokazeSkinComponents.HoldOverlayBackground),
                _ => new DefaultHoldOverlayBackground()
            )
            {
                RelativeSizeAxes = Axes.None,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
        }

        [BackgroundDependencyLoader]
        private void load(ISkinSource skin, SoyokazeConfigManager cm)
        {
            InternalChildren = new Drawable[]
            {
                background,
                progressContainer,
            };

            DrawableSoyokazeHitObject drawableSoyokazeObject = (DrawableSoyokazeHitObject)drawableObject;

            accentColourBindable.BindTo(drawableSoyokazeObject.AccentColour);
            buttonBindable.BindTo(drawableSoyokazeObject.ButtonBindable);

            accentColourBindable.BindValueChanged(colourChanged =>
            {
                if (!highlightBindable.Value)
                    progress.Colour = colourChanged.NewValue;
            }, true);
            buttonBindable.BindValueChanged(buttonChanged =>
            {
                bool doRotation = skin.GetConfig<SoyokazeSkinConfiguration, bool>(SoyokazeSkinConfiguration.RotateHoldProgress)?.Value ?? true;
                float rotation = doRotation ? PositionExtensions.ButtonToRotation(buttonChanged.NewValue) : 0f;
                background.Rotation = rotation;
                progressContainer.Rotation = rotation;
            }, true);

            cm?.BindWith(SoyokazeConfig.HoldHighlightColour, highlightColourEnumBindable);
            highlightColourEnumBindable.BindValueChanged(colourEnumChanged =>
            {
                if (colourEnumChanged.NewValue == ColourEnum.None)
                    highlightColour = skin.GetConfig<SoyokazeSkinColour, Color4>(SoyokazeSkinColour.HoldHighlight)?.Value ?? Color4.Lime;
                else
                    highlightColour = ColourExtensions.ToColour4(colourEnumChanged.NewValue);

                if (highlightBindable.Value)
                    progress.Colour = highlightColour;
            }, true);

            cm?.BindWith(SoyokazeConfig.HighlightHolds, highlightBindable);
            highlightBindable.BindValueChanged(valueChanged =>
            {
                if (valueChanged.NewValue)
                    progress.Colour = highlightColour;
                else
                    progress.Colour = accentColourBindable.Value;
            }, true);
        }

        public TransformSequence<Container> ProgressTo(float newValue, double duration = 0, Easing easing = Easing.None)
        {
            TransformSequence<Container> resize = progressMask.ResizeHeightTo(newValue, duration, easing);

            if (newValue > 0f)
            {
                return resize.Append(_ => progress.FadeTo(1f));
            }
            else
            {
                return resize.Append(_ =>
                {
                    using (BeginDelayedSequence(duration))
                        progress.FadeTo(0f);
                });
            }
        }
    }
}

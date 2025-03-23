// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Game.Rulesets.Scoring;
using osu.Game.Skinning;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Skinning.Legacy
{
    public class SoyokazeLegacySkinTransformer : LegacySkinTransformer
    {
        public SoyokazeLegacySkinTransformer(ISkin skin)
            : base(skin)
        {
        }

        public override Drawable GetDrawableComponent(ISkinComponentLookup lookup)
        {
            switch (lookup)
            {
                // This was taken from LegacySkin.GetDrawableComponent(ISkinComponentLookup lookup)
                case SkinComponentLookup<HitResult> resultComponent:
                    // kind of wasteful that we throw this away, but should do for now.
                    if (getJudgementAnimation(resultComponent.Component) != null)
                    {
                        // TODO: this should be inside the judgement pieces.
                        Func<Drawable> createDrawable = () => getJudgementAnimation(resultComponent.Component);

                        var particle = getParticleTexture(resultComponent.Component);

                        if (particle != null)
                            return new LegacyJudgementPieceNew(resultComponent.Component, createDrawable, particle);

                        return new LegacyJudgementPieceOld(resultComponent.Component, createDrawable);
                    }

                    return null;

                case SoyokazeSkinComponentLookup soyokazeComponent:
                    switch (soyokazeComponent.Component)
                    {
                        case SoyokazeSkinComponents.ApproachCircle:
                        case SoyokazeSkinComponents.Cursor:
                        case SoyokazeSkinComponents.HitCircle:
                        case SoyokazeSkinComponents.HitCircleOverlay:
                        case SoyokazeSkinComponents.HoldOverlay:
                        case SoyokazeSkinComponents.HoldOverlayBackground:
                        case SoyokazeSkinComponents.InputOverlayKey:
                        case SoyokazeSkinComponents.InputOverlayBackground:
                        case SoyokazeSkinComponents.KiaiVisualizerSquare:
                            return null;

                        case SoyokazeSkinComponents.HitCircleText:
                            if (!this.HasFont(LegacyFont.HitCircle))
                                return null;
                            
                            // stable applies a blanket 0.8x scale to hitcircle fonts;
                            // see OsuLegacySkinTransformer.GetDrawableComponent(ISkinComponentLookup lookup)
                            const float hitcircle_text_scale = 0.8f;
                            return new LegacySpriteText(LegacyFont.HitCircle)
                            {
                                Scale = new Vector2(hitcircle_text_scale),
                            };

                        default:
                            return base.GetDrawableComponent(lookup);
                    }
                default:
                    return null;
            }
        }

        public override IBindable<TValue> GetConfig<TLookup, TValue>(TLookup lookup)
        {
            switch (lookup)
            {
                case SoyokazeSkinColour colour:
                    return base.GetConfig<SkinCustomColourLookup, TValue>(new SkinCustomColourLookup(colour));
                default:
                    return base.GetConfig<TLookup, TValue>(lookup);
            }
        }

        // This function was copied from LegacySkin.getJudgementAnimation(HitResult result)
        private Drawable getJudgementAnimation(HitResult result)
        {
            switch (result)
            {
                case HitResult.Miss:
                    return this.GetAnimation("hit0", true, false);

                case HitResult.LargeTickMiss:
                    return this.GetAnimation("slidertickmiss", true, false);

                case HitResult.IgnoreMiss:
                    return this.GetAnimation("sliderendmiss", true, false);

                case HitResult.Meh:
                    return this.GetAnimation("hit50", true, false);

                case HitResult.Ok:
                    return this.GetAnimation("hit100", true, false);

                case HitResult.Great:
                    return this.GetAnimation("hit300", true, false);
            }

            return null;
        }

        // This function was copied from LegacySkin.getParticleTexture(HitResult result)
        private Texture getParticleTexture(HitResult result)
        {
            switch (result)
            {
                case HitResult.Meh:
                    return GetTexture("particle50");

                case HitResult.Ok:
                    return GetTexture("particle100");

                case HitResult.Great:
                    return GetTexture("particle300");
            }

            return null;
        }
    }
}

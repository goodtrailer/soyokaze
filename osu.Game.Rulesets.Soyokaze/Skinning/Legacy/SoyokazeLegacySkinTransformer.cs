// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Skinning.Defaults;
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
                case SkinComponentLookup<HitResult> resultLookup:
                    // kind of wasteful that we throw this away, but should do for now.
                    if (getJudgementAnimation(resultLookup.Component) != null)
                    {
                        // TODO: this should be inside the judgement pieces.
                        Func<Drawable> createDrawable = () => getJudgementAnimation(resultLookup.Component);

                        var particle = getParticleTexture(resultLookup.Component);

                        if (particle != null)
                            return new LegacyJudgementPieceNew(resultLookup.Component, createDrawable, particle);

                        return new LegacyJudgementPieceOld(resultLookup.Component, createDrawable);
                    }

                    return null;

                case SoyokazeSkinComponentLookup soyokazeLookup:
                    string fallbackName = soyokazeLookup.Component.ToString().ToLower();
                    string primaryName = $"{SoyokazeRuleset.SHORT_NAME}/{fallbackName}";
                    Texture texture = GetTexture(primaryName) ?? GetTexture(fallbackName);

                    switch (soyokazeLookup.Component)
                    {
                        case SoyokazeSkinComponents.ApproachCircle:
                            return new DefaultApproachCircle { Texture = texture };

                        case SoyokazeSkinComponents.Cursor:
                            return new DefaultCursor { Texture = texture };

                        case SoyokazeSkinComponents.HitCircle:
                            return new DefaultHitCircle { Texture = texture };

                        case SoyokazeSkinComponents.HitCircleOverlay:
                            return new DefaultHitCircleOverlay { Texture = texture };

                        case SoyokazeSkinComponents.HoldOverlay:
                            return new DefaultHoldOverlay { Texture = texture };

                        case SoyokazeSkinComponents.HoldOverlayBackground:
                            return new DefaultHoldOverlayBackground { Texture = texture };

                        case SoyokazeSkinComponents.InputOverlayKey:
                            return new DefaultInputOverlayKey { Texture = texture };

                        case SoyokazeSkinComponents.InputOverlayBackground:
                            return new DefaultInputOverlayBackground { Texture = texture };

                        case SoyokazeSkinComponents.KiaiVisualizerSquare:
                            return new DefaultKiaiVisualizerSquare { Texture = texture };

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
                    return this.GetAnimation($"{SoyokazeRuleset.SHORT_NAME}/hit0", true, false) ?? this.GetAnimation("hit0", true, false);

                case HitResult.LargeTickMiss:
                    return this.GetAnimation($"{SoyokazeRuleset.SHORT_NAME}/slidertickmiss", true, false) ?? this.GetAnimation("slidertickmiss", true, false);

                case HitResult.IgnoreMiss:
                    return this.GetAnimation($"{SoyokazeRuleset.SHORT_NAME}/sliderendmiss", true, false) ?? this.GetAnimation("sliderendmiss", true, false);

                case HitResult.Meh:
                    return this.GetAnimation($"{SoyokazeRuleset.SHORT_NAME}/hit50", true, false) ?? this.GetAnimation("hit50", true, false);

                case HitResult.Ok:
                    return this.GetAnimation($"{SoyokazeRuleset.SHORT_NAME}/hit100", true, false) ?? this.GetAnimation("hit100", true, false);

                case HitResult.Great:
                    return this.GetAnimation($"{SoyokazeRuleset.SHORT_NAME}/hit300", true, false) ?? this.GetAnimation("hit300", true, false);
            }

            return null;
        }

        // This function was copied from LegacySkin.getParticleTexture(HitResult result)
        private Texture getParticleTexture(HitResult result)
        {
            switch (result)
            {
                case HitResult.Meh:
                    return GetTexture($"{SoyokazeRuleset.SHORT_NAME}/particle50") ?? GetTexture("particle50");

                case HitResult.Ok:
                    return GetTexture($"{SoyokazeRuleset.SHORT_NAME}/particle100") ?? GetTexture("particle100");

                case HitResult.Great:
                    return GetTexture($"{SoyokazeRuleset.SHORT_NAME}/particle300") ?? GetTexture("particle300");
            }

            return null;
        }
    }
}

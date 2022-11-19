// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Framework.Graphics;
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

        public override Drawable GetDrawableComponent(ISkinComponentLookup component)
        {
            if (!(component is SoyokazeSkinComponentLookup soyokazeComponent))
                return null;

            switch (soyokazeComponent.Component)
            {
                case SoyokazeSkinComponents.ApproachCircle:
                case SoyokazeSkinComponents.Cursor:
                case SoyokazeSkinComponents.HitCircle:
                case SoyokazeSkinComponents.HitCircleOverlay:
                case SoyokazeSkinComponents.HoldOverlay:
                case SoyokazeSkinComponents.InputOverlayKey:
                case SoyokazeSkinComponents.InputOverlayBackground:
                case SoyokazeSkinComponents.KiaiVisualizerSquare:
                    return null;

                case SoyokazeSkinComponents.HitCircleText:
                    if (!this.HasFont(LegacyFont.HitCircle))
                        return null;

                    return new LegacySpriteText(LegacyFont.HitCircle)
                    {
                        // stable applies a blanket 0.8x scale to hitcircle fonts
                        Scale = new Vector2(0.8f),
                    };

                default:
                    return base.GetDrawableComponent(component);
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
    }
}

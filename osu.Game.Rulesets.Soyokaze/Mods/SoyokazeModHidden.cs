// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.Objects.Drawables;

namespace osu.Game.Rulesets.Soyokaze.Mods
{
    public class SoyokazeModHidden : ModHidden
    {
        [SettingSource("Fading Approach Circle", "Allow approach circles to fade instead of disappearing")]
        public Bindable<bool> FadingApproachCircle { get; } = new BindableBool(true);

        public override double ScoreMultiplier => 1.06;
        public override string Description => "Play with fading circles.";

        private const double fade_in_fraction = 0.4;
        private const double fade_out_fraction = 0.3;

        public override void ApplyToBeatmap(IBeatmap beatmap)
        {
            base.ApplyToBeatmap(beatmap);

            foreach (var hitObject in beatmap.HitObjects.OfType<SoyokazeHitObject>())
            {
                hitObject.FadeIn = hitObject.Preempt * fade_in_fraction;
                foreach (var nested in hitObject.NestedHitObjects.OfType<SoyokazeHitObject>())
                    nested.FadeIn = nested.Preempt * fade_in_fraction;
            }
        }

        protected override void ApplyIncreasedVisibilityState(DrawableHitObject drawableObject, ArmedState state)
        {
            applyState(drawableObject, true);
        }

        protected override void ApplyNormalVisibilityState(DrawableHitObject drawableObject, ArmedState state)
        {
            applyState(drawableObject, false);
        }

        private void applyState(DrawableHitObject drawableObject, bool increaseVisibility)
        {
            if (!(drawableObject.HitObject is SoyokazeHitObject hitObject))
                return;

            double fadeOutStartTime = hitObject.StartTime - hitObject.Preempt + hitObject.FadeIn;
            double fadeOutDuration;

            switch (hitObject)
            {
                case Hold h:
                    fadeOutDuration = h.Duration + h.Preempt - h.FadeIn;
                    break;
                default:
                    fadeOutDuration = hitObject.Preempt * fade_out_fraction;
                    break;
            }

            Drawable fadeTarget = drawableObject;
            switch (drawableObject)
            {
                case DrawableHitCircle circle:
                    fadeTarget = circle.HitCircle;
                    if (!increaseVisibility)
                        if (FadingApproachCircle.Value)
                            using (circle.BeginAbsoluteSequence(fadeOutStartTime))
                            {
                                circle.ApproachCircle.FadeOut(fadeOutDuration * 0.8);
                            }
                        else
                            using (circle.BeginAbsoluteSequence(hitObject.StartTime - hitObject.Preempt))
                            {
                                circle.ApproachCircle.Hide();
                            }
                        goto default;
                default:
                    using (fadeTarget.BeginAbsoluteSequence(fadeOutStartTime))
                    {
                        fadeTarget.FadeOut(fadeOutDuration);
                    }
                    break;
            }
        }
    }
}

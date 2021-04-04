// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.Extensions;
using osu.Game.Rulesets.Soyokaze.Skinning;
using osu.Game.Rulesets.Soyokaze.UI;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables
{
    public class DrawableHitCircle : DrawableSoyokazeHitObject
    {
        public Drawable ApproachCircle { get; private set; }
        public Drawable HitCircle { get; private set; }
        public Drawable ApproachCircleProxy => ApproachCircle;

        public override bool HandlePositionalInput => true;

        public DrawableHitCircle()
            : this(null)
        {
        }
        public DrawableHitCircle(SoyokazeHitObject hitObject = null)
            : base(hitObject)
        {
            ApproachCircle = new SkinnableApproachCircle()
            {
                Alpha = 0,
                RelativeSizeAxes = Axes.Both,
                Scale = new Vector2(4),
            };
            HitCircle = new SkinnableHitCircle()
            {
                Alpha = 0,
                RelativeSizeAxes = Axes.Both,
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            AddInternal(ApproachCircle);
            AddInternal(HitCircle);

            SizeBindable.BindValueChanged(_ => Size = HitObject.Size);
            ScreenCenterDistanceBindable.BindValueChanged(_ => updatePosition());
            GapBindable.BindValueChanged(_ => updatePosition());
            ButtonBindable.BindValueChanged(_ => updatePosition());
        }

        private void updatePosition()
        {
            Vector2[] positions = PositionExtensions.GetPositions(ScreenCenterDistanceBindable.Value, GapBindable.Value, true);
            Position = positions[(int)ButtonBindable.Value];
        }

        protected override void UpdateInitialTransforms()
        {
            base.UpdateInitialTransforms();

            HitCircle.FadeIn(HitObject.FadeIn);

            ApproachCircle.FadeInFromZero(System.Math.Min(HitObject.FadeIn * 2, HitObject.Preempt / 2));
            ApproachCircle.ScaleTo(1f, HitObject.Preempt);
            ApproachCircle.Expire(true);
        }

        protected override void UpdateStartTimeStateTransforms()
        {
            base.UpdateStartTimeStateTransforms();

            ApproachCircle.FadeOut(50);
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            double duration;
            switch (state)
            {
                case ArmedState.Hit:
                    duration = 400;
                    this.ScaleTo(1.5f, duration, Easing.OutQuint).FadeOut(duration, Easing.OutQuint).Expire();
                    break;

                case ArmedState.Miss:
                    duration = 200;
                    this.ScaleTo(0.8f, duration, Easing.OutQuint);
                    this.MoveToOffset(new Vector2(0, 10), duration, Easing.In);
                    this.FadeColour(Color4.Red.Opacity(0.5f), duration / 2, Easing.OutQuint).Then().FadeOut(duration / 2, Easing.InQuint).Expire();
                    break;
            }
        }

        public override double LifetimeStart
        {
            get => base.LifetimeStart;
            set
            {
                base.LifetimeStart = value;
                ApproachCircle.LifetimeStart = value;
            }
        }

        public override double LifetimeEnd
        {
            get => base.LifetimeEnd;
            set
            {
                base.LifetimeEnd = value;
                ApproachCircle.LifetimeEnd = value;
            }
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (!userTriggered)
            {
                if (!HitObject.HitWindows.CanBeHit(timeOffset))
                    ApplyResult(r => r.Type = r.Judgement.MinResult);
                return;
            }

            HitResult result = HitObject.HitWindows.ResultFor(timeOffset);

            if (result == HitResult.None)
            {
                // shake, but make sure not to exceed into the window where you can actually miss
                Shake(-timeOffset - HitObject.HitWindows.WindowFor(HitResult.Miss));
                return;
            }

            ApplyResult(r => r.Type = result);
        }

        public override bool Hit(SoyokazeAction action)
        {
            if (Judged)
                return false;

            SoyokazeAction validAction = ButtonBindable.Value;
            if (action != validAction)
                return false;

            UpdateResult(true);
            return true;
        }
    }
}

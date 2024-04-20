﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Skinning;
using osu.Game.Rulesets.Soyokaze.UI;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables
{
    public partial class DrawableHitCircle : DrawableSoyokazeHitObject
    {
        public new HitCircle HitObject => base.HitObject as HitCircle;
        public SkinnableApproachCircle ApproachCircle { get; private set; }
        public SkinnableHitCircle HitCircle { get; private set; }
        public Drawable ApproachCircleProxy => ApproachCircle;

        public DrawableHitCircle()
            : this(null)
        {
        }

        public DrawableHitCircle(HitCircle hitCircle = null)
            : base(hitCircle)
        {
            ApproachCircle = new SkinnableApproachCircle
            {
                Alpha = 0,
                Scale = new Vector2(4),
            };
            HitCircle = new SkinnableHitCircle
            {
                Alpha = 0,
                Scale = new Vector2(1),
            };
            Size = new Vector2(SoyokazeHitObject.OBJECT_RADIUS * 2);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(ApproachCircle);
            AddInternal(HitCircle);
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
            const double hit_duration = 400;
            const float hit_dilate = 1.5f;

            const double miss_duration = 200;
            const float miss_contract = 0.8f;
            const float miss_offset = 10f;

            switch (state)
            {
                case ArmedState.Hit:
                    this.ScaleTo(Scale * hit_dilate, hit_duration, Easing.OutQuint);
                    this.FadeOut(hit_duration, Easing.OutQuint);
                    Expire();
                    break;

                case ArmedState.Miss:
                    this.ScaleTo(Scale * miss_contract, miss_duration, Easing.OutQuint);
                    this.MoveToOffset(new Vector2(0, miss_offset), miss_duration, Easing.In);
                    this.FadeOut(miss_duration, Easing.OutQuint);
                    Expire();
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
                    ApplyResult(static (r, _) => r.Type = r.Judgement.MinResult, 0);
                return;
            }

            HitResult result = HitObject.HitWindows.ResultFor(timeOffset);

            if (result == HitResult.None)
            {
                // shake, but make sure not to exceed into the window where you can actually miss
                Shake(-timeOffset - HitObject.HitWindows.WindowFor(HitResult.Miss));
                return;
            }

            ApplyResult(result);
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

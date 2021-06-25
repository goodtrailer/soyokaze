// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Logging;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Skinning;
using osu.Game.Rulesets.Soyokaze.UI;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables
{
    public class DrawableHold : DrawableSoyokazeHitObject
    {
        public new Hold HitObject => base.HitObject as Hold;
        public DrawableHoldCircle HoldCircle => holdCircleContainer.Child;
        public SkinnableApproachCircle OuterApproachCircle { get; private set; }

        private Container<DrawableHoldCircle> holdCircleContainer;
        private double holdStartTime = double.MinValue;
        private double holdDuration = 0.0;

        public DrawableHold()
            : this(null)
        {
        }

        public DrawableHold(Hold hold = null)
            : base(hold)
        {
            holdCircleContainer = new Container<DrawableHoldCircle>
            {
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
            };
            OuterApproachCircle = new SkinnableApproachCircle
            {
                Alpha = 0,
                Scale = new Vector2(4),
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(holdCircleContainer);
            AddInternal(OuterApproachCircle);
        }

        protected override void OnApply()
        {
            base.OnApply();

            holdStartTime = -1.0;
            holdDuration = 0.0;
        }

        protected override DrawableHitObject CreateNestedHitObject(HitObject hitObject)
        {
            switch (hitObject)
            {
                case HoldCircle holdCircle:
                    return new DrawableHitCircle(holdCircle);
            }

            return base.CreateNestedHitObject(hitObject);
        }

        protected override void AddNestedHitObject(DrawableHitObject hitObject)
        {
            base.AddNestedHitObject(hitObject);

            switch (hitObject)
            {
                case DrawableHoldCircle holdCircle:
                    holdCircleContainer.Child = holdCircle;
                    break;
            }
        }

        protected override void ClearNestedHitObjects()
        {
            base.ClearNestedHitObjects();
            holdCircleContainer.Clear(false);
        }

        protected override void UpdateInitialTransforms()
        {
            base.UpdateInitialTransforms();

            OuterApproachCircle.FadeInFromZero(System.Math.Min(HitObject.FadeIn * 2, HitObject.Preempt / 2));
            OuterApproachCircle.ScaleTo(1f, HitObject.Preempt + HitObject.Duration);
            OuterApproachCircle.Expire(true);
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Hit:
                case ArmedState.Miss:
                    OuterApproachCircle.FadeOut(50);
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
                OuterApproachCircle.LifetimeStart = value;
            }
        }

        public override double LifetimeEnd
        {
            get => base.LifetimeEnd;
            set
            {
                base.LifetimeEnd = value;
                OuterApproachCircle.LifetimeEnd = value;
            }
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (userTriggered || Time.Current < HitObject.EndTime)
                return;

            // if the player releases the key after the hold is over, which is
            // what usually happens, then Release wouldn't be called if not for
            // this call below
            Release(ButtonBindable.Value);

            double holdFraction = holdDuration / HitObject.Duration;
            double holdCircleFraction =
                (double)HoldCircle.TrueResult.Judgement.NumericResultFor(HoldCircle.TrueResult) /
                HoldCircle.TrueResult.Judgement.MaxNumericResult;

            double scoreFraction = (holdCircleFraction + holdFraction) / 2;

            Logger.Log("HIT RESULT: " + HoldCircle.TrueResult.Type.ToString() + " // " + holdCircleFraction.ToString("0.00") + " // " + holdFraction.ToString("0.00") + " // " + scoreFraction.ToString("0.00"));

            HitResult result;

            if (scoreFraction > 0.9)
                result = HitResult.Perfect;
            else if (scoreFraction > 0.7)
                result = HitResult.Great;
            else if (scoreFraction > 0.5)
                result = HitResult.Good;
            else if (scoreFraction > 0.3)
                result = HitResult.Ok;
            else if (scoreFraction > 0.1)
                result = HitResult.Meh;
            else
                result = HitResult.Miss;

            ApplyResult(r => r.Type = result);
        }

        public override bool Hit(SoyokazeAction action)
        {
            if (Judged)
                return false;

            SoyokazeAction validAction = ButtonBindable.Value;
            if (action != validAction)
                return false;

            holdStartTime = Time.Current;
            HoldCircle.Hit(action);

            Logger.Log("HIT: " + holdStartTime.ToString("0.00"));

            return true;
        }

        public bool Release(SoyokazeAction action)
        {
            if (Judged)
                return false;

            SoyokazeAction validAction = ButtonBindable.Value;
            if (action != validAction)
                return false;

            if (holdStartTime != double.MinValue)
            {
                holdDuration += Time.Current - holdStartTime;
                holdStartTime = double.MinValue;
            }

            Logger.Log("RELEASE: " + holdDuration.ToString("0.00"));

            return true;
        }
    }
}

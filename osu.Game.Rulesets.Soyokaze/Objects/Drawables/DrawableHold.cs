// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
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
        public SkinnableHoldProgress HoldProgress;

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

            HoldProgress = new SkinnableHoldProgress
            {
                RelativeSizeAxes = Axes.Both,
                Current = { Value = 0 },
            };

            Size = new Vector2(SoyokazeHitObject.OBJECT_RADIUS * 2);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(holdCircleContainer);
            AddInternal(HoldProgress);
        }

        protected override void OnApply()
        {
            base.OnApply();

            holdStartTime = double.MinValue;
            holdDuration = 0.0;
        }

        protected override void Update()
        {
            base.Update();
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

            HoldProgress.FadeInFromZero(System.Math.Min(HitObject.FadeIn * 2, HitObject.Preempt / 2));
            using (BeginDelayedSequence(HitObject.Preempt))
            {
                HoldProgress.FillTo(1.0, HitObject.Duration);
            }
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Hit:
                case ArmedState.Miss:
                    HoldProgress.FadeOut(50);
                    Expire();
                    break;
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

            HitResult result;

            if (scoreFraction > 0.9)
                result = HitResult.Perfect;
            else if (scoreFraction > 0.8)
                result = HitResult.Great;
            else if (scoreFraction > 0.7)
                result = HitResult.Good;
            else if (scoreFraction > 0.6)
                result = HitResult.Ok;
            else if (scoreFraction > 0.5)
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

            return true;
        }
    }
}

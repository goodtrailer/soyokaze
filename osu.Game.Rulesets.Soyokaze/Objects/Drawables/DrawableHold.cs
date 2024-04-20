// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Audio;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Judgements;
using osu.Game.Rulesets.Soyokaze.Scoring;
using osu.Game.Rulesets.Soyokaze.Skinning;
using osu.Game.Rulesets.Soyokaze.UI;
using osu.Game.Skinning;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables
{
    public partial class DrawableHold : DrawableSoyokazeHitObject
    {
        public new Hold HitObject => base.HitObject as Hold;
        public DrawableHoldCircle HoldCircle => holdCircleContainer.Child;
        public SkinnableHoldProgress HoldProgress;

        private PausableSkinnableSound holdSamples;

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
            Size = new Vector2(SoyokazeHitObject.OBJECT_RADIUS * 2);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                holdCircleContainer = new Container<DrawableHoldCircle>
                {
                    RelativeSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                },
                HoldProgress = new SkinnableHoldProgress
                {
                    RelativeSizeAxes = Axes.Both,
                    Progress = 0.0,
                },
                holdSamples = new PausableSkinnableSound { Looping = true },
            };
        }

        protected override void OnApply()
        {
            base.OnApply();

            holdStartTime = double.MinValue;
            holdDuration = 0.0;
        }

        protected override void OnFree()
        {
            base.OnFree();

            holdSamples?.ClearSamples();
        }

        protected override void LoadSamples()
        {
            base.LoadSamples();

            if (HitObject.HoldSamples == null)
                return;

            var slidingSamples = new List<ISampleInfo>();

            var normalSample = HitObject.HoldSamples.FirstOrDefault(s => s.Name == HitSampleInfo.HIT_NORMAL);
            if (normalSample != null)
                slidingSamples.Add(normalSample.With("sliderslide"));

            var whistleSample = HitObject.HoldSamples.FirstOrDefault(s => s.Name == HitSampleInfo.HIT_WHISTLE);
            if (whistleSample != null)
                slidingSamples.Add(whistleSample.With("sliderwhistle"));

            holdSamples.Samples = slidingSamples.ToArray();
        }

        public override void StopAllSamples()
        {
            base.StopAllSamples();

            holdSamples.Stop();
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

            HoldProgress.FadeInFromZero(Math.Min(HitObject.FadeIn * 2, HitObject.Preempt / 2));
            using (BeginDelayedSequence(HitObject.Preempt))
            {
                HoldProgress.ProgressTo(1.0, HitObject.Duration);
            }
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            const double hit_duration = 350;
            const float hit_dilate = 1.5f;

            const double miss_duration = 175;
            const float miss_contract = 0.9f;
            const float miss_offset = 10f;

            switch (state)
            {
                case ArmedState.Hit:
                    HoldProgress.ScaleTo(HoldProgress.Scale * hit_dilate, hit_duration, Easing.OutQuint);
                    HoldProgress.FadeOut(hit_duration, Easing.OutQuint);
                    this.MoveToOffset(Vector2.Zero, hit_duration).Expire();
                    break;

                case ArmedState.Miss:
                    HoldProgress.ScaleTo(HoldProgress.Scale * miss_contract, miss_duration, Easing.OutQuint);
                    HoldProgress.MoveToOffset(new Vector2(0, miss_offset), miss_duration, Easing.In);
                    HoldProgress.FadeOut(miss_duration, Easing.OutQuint);
                    this.MoveToOffset(Vector2.Zero, miss_duration).Expire();
                    break;
            }
        }

        protected override JudgementResult CreateResult(Judgement judgement) => new SoyokazeHoldJudgementResult(HitObject, judgement);

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (userTriggered || Time.Current < HitObject.EndTime)
                return;

            // if the player releases the key after the hold is over, which is
            // what usually happens, then Release wouldn't be called if not for
            // this call below
            Release(ButtonBindable.Value);

            double holdFraction = holdDuration / HitObject.Duration;
            double holdCircleFraction = 0.0;

            JudgementResult trueResult = HoldCircle.TrueResult;
            if (trueResult != null)
            {
                SoyokazeScoreProcessor scorer = new SoyokazeScoreProcessor();
                double score = scorer.GetBaseScoreForResult(trueResult.Type);
                double max = scorer.GetBaseScoreForResult(trueResult.Judgement.MaxResult);
                holdCircleFraction = score / max;
            }

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

            var payload = new { result, HoldCircle.TrueTimeOffset };

            ApplyResult(static (r, p) =>
            {
                if (!(r is SoyokazeHoldJudgementResult hr))
                    throw new InvalidOperationException($"Expected result of type {nameof(SoyokazeHoldJudgementResult)}");

                hr.Type = p.result;
                hr.TrueTimeOffset = p.TrueTimeOffset;
            }, payload);
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

            const float hold_scale = 0.8f;
            const double hold_duration = 80;
            HoldProgress.ScaleTo(new Vector2(hold_scale), hold_duration, Easing.OutQuint);
            holdSamples.Play();

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

            const float hold_scale = 1f;
            const double hold_duration = 120;
            HoldProgress.ScaleTo(new Vector2(hold_scale), hold_duration, Easing.OutQuint);
            holdSamples.Stop();

            return true;
        }
    }
}

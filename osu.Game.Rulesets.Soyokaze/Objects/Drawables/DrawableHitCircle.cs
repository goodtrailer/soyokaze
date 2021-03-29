// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osuTK;
using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Game.Graphics.Containers;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Objects.Drawables.Pieces;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables
{
    class DrawableHitCircle : DrawableSoyokazeHitObject
    {
        public DrawableApproachCirclePiece ApproachCirclePiece { get; private set; }
        public DrawableHitCirclePiece HitCirclePiece { get; private set; }
        public Drawable ApproachCircleProxy => ApproachCirclePiece;

        public override bool HandlePositionalInput => true;

        public DrawableHitCircle()
            : this(null)
        {
        }
        public DrawableHitCircle(SoyokazeHitObject hitObject = null)
            : base(hitObject)
        {
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            ApproachCirclePiece = new DrawableApproachCirclePiece()
            {
                Alpha = 0,
                RelativeSizeAxes = Axes.Both,
                Scale = new Vector2(4),
            };
            HitCirclePiece = new DrawableHitCirclePiece()
            {
                Alpha = 0,
                RelativeSizeAxes = Axes.Both,
            };

            AddInternal(ApproachCirclePiece);
            AddInternal(HitCirclePiece);

            PositionBindable.BindValueChanged(_ => Position = HitObject.Position);
            SizeBindable.BindValueChanged(_ => Size = HitObject.Size);
        }

        protected override void UpdateInitialTransforms()
        {
            base.UpdateInitialTransforms();

            HitCirclePiece.FadeIn(HitObject.FadeIn);

            ApproachCirclePiece.FadeInFromZero(System.Math.Min(HitObject.FadeIn * 2, HitObject.Preempt / 2));
            ApproachCirclePiece.ScaleTo(1f, HitObject.Preempt);
            ApproachCirclePiece.Expire(true);
        }

        protected override void UpdateStartTimeStateTransforms()
        {
            base.UpdateStartTimeStateTransforms();

            ApproachCirclePiece.FadeOut(50);
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            double duration;
            switch (state)
            {
                case ArmedState.Hit:
                    duration = 500;
                    this.ScaleTo(2, duration, Easing.OutQuint).FadeOut(duration, Easing.OutQuint).Expire();
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
                ApproachCirclePiece.LifetimeStart = value;
            }
        }

        public override double LifetimeEnd
        {
            get => base.LifetimeEnd;
            set
            {
                base.LifetimeEnd = value;
                ApproachCirclePiece.LifetimeEnd = value;
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
                Shake(- timeOffset - HitObject.HitWindows.WindowFor(HitResult.Miss));
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

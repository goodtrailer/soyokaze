// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Objects.Drawables.Pieces;
using osu.Game.Rulesets.Scoring;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables
{
    class DrawableHitCircle : DrawableSoyokazeHitObject, IKeyBindingHandler<SoyokazeAction>
    {
        public ApproachCirclePiece ApproachCircleComponent { get; private set; }
        public HitCirclePiece HitCircleComponent { get; private set; }

        public Drawable ApproachCircleProxy => ApproachCircleComponent;
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
            ApproachCircleComponent = new ApproachCirclePiece()
            {
                Alpha = 0,
            };
            HitCircleComponent = new HitCirclePiece()
            {
                Alpha = 0,
            };
            AddInternal(ApproachCircleComponent);
            AddInternal(HitCircleComponent);

            PositionBindable.BindValueChanged(_ => Position = HitObject.Position);
            SizeBindable.BindValueChanged(_ => Size = HitObject.Size);
        }

        protected override void UpdateInitialTransforms()
        {
            base.UpdateInitialTransforms();

            HitCircleComponent.FadeIn(HitObject.FadeIn);

            ApproachCircleComponent.FadeInFromZero(System.Math.Min(HitObject.FadeIn * 2, HitObject.Preempt / 2));
            ApproachCircleComponent.ScaleTo(1f, HitObject.Preempt);
            ApproachCircleComponent.Expire(true);
        }

        protected override void UpdateStartTimeStateTransforms()
        {
            base.UpdateStartTimeStateTransforms();

            ApproachCircleComponent.FadeOut(50);
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            double duration = 0;
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
                ApproachCircleComponent.LifetimeStart = value;
            }
        }

        public override double LifetimeEnd
        {
            get => base.LifetimeEnd;
            set
            {
                base.LifetimeEnd = value;
                ApproachCircleComponent.LifetimeEnd = value;
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
                return;
            
            ApplyResult(r => r.Type = result);
        }

        public bool OnPressed(SoyokazeAction action)
        {
            if (Judged)
                return false;

            SoyokazeAction validAction = ButtonBindable.Value;
            if (action == validAction)
            {
                UpdateResult(true);
                return true;
            }
            else
                return false;
        }

        public void OnReleased(SoyokazeAction action)
        {
        }
    }
}

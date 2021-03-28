// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Bindables;
using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables
{
    public class DrawableSoyokazeHitObject : DrawableHitObject<SoyokazeHitObject>
    {
        public readonly IBindable<Vector2> PositionBindable = new Bindable<Vector2>();
        public readonly IBindable<Vector2> SizeBindable = new Bindable<Vector2>();
        public readonly IBindable<SoyokazeAction> ButtonBindable = new Bindable<SoyokazeAction>();

        protected override double InitialLifetimeOffset => HitObject.Preempt;


        public DrawableSoyokazeHitObject(SoyokazeHitObject hitObject)
            : base(hitObject)
        {
        }

        protected override void UpdateInitialTransforms() => this.FadeIn(HitObject.FadeIn);

        [BackgroundDependencyLoader]
        private void load()
        {
            Alpha = 1;
            Origin = Anchor.Centre;
        }

        protected override void OnApply()
        {
            base.OnApply();

            PositionBindable.BindTo(HitObject.PositionBindable);
            SizeBindable.BindTo(HitObject.SizeBindable);
            ButtonBindable.BindTo(HitObject.ButtonBindable);
        }

        protected override void OnFree()
        {
            base.OnFree();

            PositionBindable.UnbindFrom(HitObject.PositionBindable);
            SizeBindable.UnbindFrom(HitObject.SizeBindable);
            ButtonBindable.UnbindFrom(HitObject.ButtonBindable);
        }


    }
}

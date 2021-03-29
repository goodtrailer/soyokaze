// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Bindables;
using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables
{
    public abstract class DrawableSoyokazeHitObject : DrawableHitObject<SoyokazeHitObject>
    {
        public readonly IBindable<Vector2> PositionBindable;
        public readonly IBindable<Vector2> SizeBindable;
        public readonly IBindable<SoyokazeAction> ButtonBindable;

        protected override double InitialLifetimeOffset => HitObject.Preempt;



        public DrawableSoyokazeHitObject(SoyokazeHitObject hitObject)
            : base(hitObject)
        {
            Alpha = 1;
            Origin = Anchor.Centre;

            PositionBindable = new Bindable<Vector2>();
            SizeBindable = new Bindable<Vector2>();
            ButtonBindable = new Bindable<SoyokazeAction>();
        }

        public abstract bool Hit(SoyokazeAction action);

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

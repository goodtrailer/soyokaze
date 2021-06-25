// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.Extensions;
using osu.Game.Rulesets.Soyokaze.UI;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables
{
    public abstract class DrawableSoyokazeHitObject : DrawableHitObject<SoyokazeHitObject>
    {
        public readonly Bindable<float> ScaleBindable = new Bindable<float>();
        public readonly Bindable<SoyokazeAction> ButtonBindable = new Bindable<SoyokazeAction>();
        public readonly Bindable<int> IndexInCurrentComboBindable = new Bindable<int>();
        public readonly Bindable<int> ScreenCenterDistanceBindable = new Bindable<int>();
        public readonly Bindable<int> GapBindable = new Bindable<int>();

        protected override double InitialLifetimeOffset => HitObject.Preempt;

        private ShakeContainer shakeContainer;

        public DrawableSoyokazeHitObject(SoyokazeHitObject hitObject)
            : base(hitObject)
        {
            Alpha = 1;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
        }

        [BackgroundDependencyLoader(true)]
        private void load(SoyokazeConfigManager cm)
        {
            shakeContainer = new ShakeContainer()
            {
                ShakeDuration = 30,
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
            };
            base.AddInternal(shakeContainer);

            cm?.BindWith(SoyokazeConfig.HitCircleScreenCenterDistance, ScreenCenterDistanceBindable);
            cm?.BindWith(SoyokazeConfig.HitCircleGap, GapBindable);

            ScaleBindable.BindValueChanged(_ => UpdateScale(), true);
            ScreenCenterDistanceBindable.BindValueChanged(_ => UpdatePosition(), true);
            GapBindable.BindValueChanged(_ => UpdatePosition(), true);
            ButtonBindable.BindValueChanged(_ => UpdatePosition(), true);
        }

        public abstract bool Hit(SoyokazeAction action);

        public virtual void Shake(double maximumLength) => shakeContainer.Shake(maximumLength);

        protected virtual void UpdatePosition()
        {
            Vector2[] positions = PositionExtensions.GetPositions(ScreenCenterDistanceBindable.Value, GapBindable.Value, true, Anchor.Centre);
            Position = positions[(int)ButtonBindable.Value];
        }

        protected virtual void UpdateScale()
        {
            Scale = new Vector2(ScaleBindable.Value);
        }

        protected override void OnApply()
        {
            base.OnApply();

            ScaleBindable.BindTo(HitObject.ScaleBindable);
            ButtonBindable.BindTo(HitObject.ButtonBindable);
            IndexInCurrentComboBindable.BindTo(HitObject.IndexInCurrentComboBindable);
        }

        protected override void OnFree()
        {
            base.OnFree();

            ScaleBindable.UnbindFrom(HitObject.ScaleBindable);
            ButtonBindable.UnbindFrom(HitObject.ButtonBindable);
            IndexInCurrentComboBindable.UnbindFrom(HitObject.IndexInCurrentComboBindable);
        }

        protected override void AddInternal(Drawable drawable) => shakeContainer.Add(drawable);
        protected override void ClearInternal(bool disposeChildren = true) => shakeContainer.Clear(disposeChildren);
        protected override bool RemoveInternal(Drawable drawable) => shakeContainer.Remove(drawable);
    }
}

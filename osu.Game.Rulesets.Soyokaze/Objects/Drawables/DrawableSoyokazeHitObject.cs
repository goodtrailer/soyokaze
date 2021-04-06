// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.UI;

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
        private SoyokazeConfigManager configManager;

        public DrawableSoyokazeHitObject(SoyokazeHitObject hitObject)
            : base(hitObject)
        {
            Alpha = 1;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
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

            configManager = cm;
        }

        public abstract bool Hit(SoyokazeAction action);

        public virtual void Shake(double maximumLength) => shakeContainer.Shake(maximumLength);

        protected override void OnApply()
        {
            base.OnApply();

            ScreenCenterDistanceBindable.BindTo(configManager.GetBindable<int>(SoyokazeConfig.HitCircleScreenCenterDistance));
            GapBindable.BindTo(configManager.GetBindable<int>(SoyokazeConfig.HitCircleGap));
            ScaleBindable.BindTo(HitObject.ScaleBindable);
            ButtonBindable.BindTo(HitObject.ButtonBindable);
            IndexInCurrentComboBindable.BindTo(HitObject.IndexInCurrentComboBindable);
        }

        protected override void OnFree()
        {
            base.OnFree();

            ScreenCenterDistanceBindable.UnbindFrom(configManager.GetBindable<int>(SoyokazeConfig.HitCircleScreenCenterDistance));
            GapBindable.UnbindFrom(configManager.GetBindable<int>(SoyokazeConfig.HitCircleGap));
            ScaleBindable.UnbindFrom(HitObject.ScaleBindable);
            ButtonBindable.UnbindFrom(HitObject.ButtonBindable);
            IndexInCurrentComboBindable.UnbindFrom(HitObject.IndexInCurrentComboBindable);
        }

        protected override void AddInternal(Drawable drawable) => shakeContainer.Add(drawable);
        protected override void ClearInternal(bool disposeChildren = true) => shakeContainer.Clear(disposeChildren);
        protected override bool RemoveInternal(Drawable drawable) => shakeContainer.Remove(drawable);
    }
}

// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.Objects.Drawables;
using osu.Game.Rulesets.Objects;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Soyokaze.UI
{
    [Cached]
    public class SoyokazePlayfield : Playfield
    {
        private readonly ProxyContainer approachCircleContainer;
        protected override GameplayCursorContainer CreateCursor() => new SoyokazeCursorContainer();
        protected override HitObjectLifetimeEntry CreateLifetimeEntry(HitObject hitObject) => new SoyokazeHitObjectLifetimeEntry(hitObject);

        // exists to make AddInternal publicly accessible
        private class ProxyContainer : LifetimeManagementContainer
        {
            public void Add(Drawable proxy) => AddInternal(proxy);
        }

        public SoyokazePlayfield()
        {
            approachCircleContainer = new ProxyContainer { RelativeSizeAxes = Axes.Both };
            AddRangeInternal(new Drawable[]
            {
                HitObjectContainer,
                approachCircleContainer,
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            RegisterPool<HitCircle, DrawableHitCircle>(15);
        }

        protected override void OnNewDrawableHitObject(DrawableHitObject drawable)
        {
            drawable.OnLoadComplete += onDrawableHitObjectLoaded;
        }

        private void onDrawableHitObjectLoaded(Drawable drawable)
        {
            switch (drawable)
            {
                case DrawableHitCircle hitCircle:
                    approachCircleContainer.Add(hitCircle.ApproachCircleProxy.CreateProxy());
                    break;
            }
        }
    }
}

// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Skinning;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Soyokaze.UI
{
    [Cached]
    public partial class SoyokazePlayfield : Playfield, IKeyBindingHandler<SoyokazeAction>
    {
        private readonly ProxyContainer approachCircleContainer = new ProxyContainer { RelativeSizeAxes = Axes.Both };
        private readonly JudgementContainer<DrawableSoyokazeJudgement> judgementContainer = new JudgementContainer<DrawableSoyokazeJudgement> { RelativeSizeAxes = Axes.Both };
        private readonly SkinnableInputOverlay inputOverlay = new SkinnableInputOverlay { RelativeSizeAxes = Axes.Both, Origin = Anchor.Centre, Anchor = Anchor.Centre };
        private readonly SkinnableKiaiVisualizer kiaiVisualizer = new SkinnableKiaiVisualizer { RelativeSizeAxes = Axes.Both, Origin = Anchor.Centre, Anchor = Anchor.Centre };
        private SoyokazeConfigManager configManager;

        // DEPRECATED: see SoyokazeCursorContainer
        // protected override GameplayCursorContainer CreateCursor() => new SoyokazeCursorContainer();
        protected override HitObjectLifetimeEntry CreateLifetimeEntry(HitObject hitObject) => new SoyokazeHitObjectLifetimeEntry(hitObject);

        private partial class ProxyContainer : LifetimeManagementContainer
        {
            public void Add(Drawable proxy) => AddInternal(proxy);
        }

        public SoyokazePlayfield()
        {
            NewResult += onNewResult;
        }

        [BackgroundDependencyLoader(true)]
        private void load(SoyokazeConfigManager cm)
        {
            configManager = cm;

            AddRangeInternal(new Drawable[]
            {
                kiaiVisualizer,
                inputOverlay,
                HitObjectContainer,
                judgementContainer,
                approachCircleContainer,
            });

            RegisterPool<HitCircle, DrawableHitCircle>(15, 30);

            RegisterPool<Hold, DrawableHold>(15, 30);
            RegisterPool<HoldCircle, DrawableHoldCircle>(15, 30);
        }

        protected override void OnNewDrawableHitObject(DrawableHitObject drawableObject)
        {
            drawableObject.OnLoadComplete += onDrawableHitObjectLoaded;
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

        private void onNewResult(DrawableHitObject drawableObject, JudgementResult result)
        {
            if (!drawableObject.DisplayResult || !DisplayJudgements.Value)
                return;

            switch (drawableObject)
            {
                case DrawableHitCircle _:
                case DrawableHold _:
                    judgementContainer.Add(new DrawableSoyokazeJudgement(result, drawableObject, configManager));
                    break;
            }
        }

        public bool OnPressed(KeyBindingPressEvent<SoyokazeAction> e)
        {
            inputOverlay.PressKey(e.Action);

            foreach (var hitObject in HitObjectContainer.AliveObjects)
                switch (hitObject)
                {
                    case DrawableHitCircle hitCircle:
                        if (hitCircle.Hit(e.Action))
                            return true;
                        break;
                    case DrawableHold hold:
                        if (hold.Hit(e.Action))
                            return true;
                        break;
                }
            return true;

            #region Unused chronological sort
            /* This is what I had written at one point to make sure that the list
             * of objects was checked chronologically, because I wasn't sure if
             * HitObjectContainer.AliveObjects was chronological or not. I'm still
             * not sure if it is, but it seems to be from my small amount of
             * playtesting, so I'm commenting it out for now. I'll leave it here
             * in case it turns out to be necessary. But even if it is, I'll
             * probably go for some alternative method, since this is really slow.

                    List<DrawableSoyokazeHitObject> hitObjects = new List<DrawableSoyokazeHitObject>();
                    foreach (var hitObject in HitObjectContainer.AliveObjects)
                    {                
                        switch (hitObject)
                        {
                            case DrawableHitCircle hitCircle:
                                hitObjects.Add(hitCircle);
                                break;
                        }
                    }

                    hitObjects.Sort((x, y) =>
                    {
                        if (x.HitObject.StartTime > y.HitObject.StartTime)
                            return 1;
                        else
                            return -1;
                    });

                    foreach (var hitObject in hitObjects)
                        if (hitObject.Hit(action))
                            return true;
             */
            #endregion
        }

        public void OnReleased(KeyBindingReleaseEvent<SoyokazeAction> e)
        {
            inputOverlay.UnpressKey(e.Action);
            foreach (var hitObject in HitObjectContainer.AliveObjects)
                switch (hitObject)
                {
                    case DrawableHold hold:
                        if (hold.Release(e.Action))
                            return;
                        break;
                }
        }
    }
}

// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.Extensions;
using osu.Game.Rulesets.Soyokaze.Skinning.Defaults;
using osu.Game.Rulesets.Soyokaze.UI;
using osu.Game.Skinning;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Skinning
{
    public class SkinnableInputOverlay : Container
    {
        private Container[] inputOverlayKeyContainers = new Container[8];
        private SkinnableDrawable[] inputOverlayKeys = new SkinnableDrawable[8];
        private SkinnableDrawable[] inputOverlayBackgrounds = new SkinnableDrawable[2];

        private Bindable<int> screenCenterDistanceBindable = new Bindable<int>();
        private Bindable<int> gapBindable = new Bindable<int>();

        private int screenCenterDistance => screenCenterDistanceBindable.Value;

        private int gap => gapBindable.Value;

        private const double press_duration = 60d;
        private const float press_size = 0.6f;

        public SkinnableInputOverlay()
        {
            Origin = Anchor.Centre;

            for (int i = 0; i < 8; i++)
            {
                inputOverlayKeyContainers[i] = new Container() { Origin = Anchor.Centre, Anchor = Anchor.Centre };
                inputOverlayKeys[i] = new SkinnableDrawable(
                    new SoyokazeSkinComponent(SoyokazeSkinComponents.InputOverlayKey),
                    _ => new DefaultInputOverlayKey()
                );
            }

            for (int i = 0; i < 2; i++)
            {
                inputOverlayBackgrounds[i] = new SkinnableDrawable(
                    new SoyokazeSkinComponent(SoyokazeSkinComponents.InputOverlayBackground),
                    _ => new DefaultInputOverlayBackground()
                );
            }
        }

        [BackgroundDependencyLoader]
        private void load(SoyokazeConfigManager cm)
        {
            for (int i = 0; i < 8; i++)
                inputOverlayKeyContainers[i].Add(inputOverlayKeys[i]);
            AddRangeInternal(inputOverlayKeyContainers);
            AddRangeInternal(inputOverlayBackgrounds);

            cm.BindWith(SoyokazeConfig.InputOverlayScreenCenterDistance, screenCenterDistanceBindable);
            cm.BindWith(SoyokazeConfig.InputOverlayGap, gapBindable);

            screenCenterDistanceBindable.BindValueChanged(_ => updatePositions(), true);
            gapBindable.BindValueChanged(_ => updatePositions(), true);
        }

        private void updatePositions()
        {
            Vector2[] positions = PositionExtensions.GetPositions(screenCenterDistance, gap, true);
            for (int i = 0; i < 8; i++)
                inputOverlayKeyContainers[i].Position = positions[i];

            inputOverlayBackgrounds[0].Position = PositionExtensions.SCREEN_CENTER + new Vector2(-screenCenterDistance, 0);
            inputOverlayBackgrounds[1].Position = PositionExtensions.SCREEN_CENTER + new Vector2(screenCenterDistance, 0);
        }

        public void PressKey(SoyokazeAction button)
        {
            inputOverlayKeys[(int)button].ScaleTo(press_size, press_duration);
        }

        public void UnpressKey(SoyokazeAction button)
        {
            inputOverlayKeys[(int)button].ScaleTo(1f, press_duration);
        }
    }
}

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
    public partial class SkinnableInputOverlay : Container
    {
        private SkinnableDrawable[] inputOverlayKeys = new SkinnableDrawable[8];
        private SkinnableDrawable[] inputOverlayBackgrounds = new SkinnableDrawable[2];

        private Bindable<int> screenCenterGapBindable = new Bindable<int>();
        private Bindable<bool> showBindable = new Bindable<bool>();

        private int screenCenterGap => screenCenterGapBindable.Value;
        private int keyGap;

        private const double press_duration = 60d;
        private const float press_scale = 0.6f;

        public SkinnableInputOverlay()
        {
            for (int i = 0; i < inputOverlayKeys.Length; i++)
            {
                inputOverlayKeys[i] = new SkinnableDrawable(
                    new SoyokazeSkinComponentLookup(SoyokazeSkinComponents.InputOverlayKey),
                    _ => new DefaultInputOverlayKey()
                )
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Rotation = PositionExtensions.ButtonToRotation((SoyokazeAction)i),
                };
            }

            for (int i = 0; i < inputOverlayBackgrounds.Length; i++)
            {
                inputOverlayBackgrounds[i] = new SkinnableDrawable(
                    new SoyokazeSkinComponentLookup(SoyokazeSkinComponents.InputOverlayBackground),
                    _ => new DefaultInputOverlayBackground()
                )
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                };
            }
        }

        [BackgroundDependencyLoader(true)]
        private void load(ISkinSource skin, SoyokazeConfigManager cm)
        {
            AddRangeInternal(inputOverlayKeys);
            AddRangeInternal(inputOverlayBackgrounds);

            cm?.BindWith(SoyokazeConfig.ScreenCenterGap, screenCenterGapBindable);
            cm?.BindWith(SoyokazeConfig.ShowInputOverlay, showBindable);
            keyGap = skin.GetConfig<SoyokazeSkinConfiguration, int>(SoyokazeSkinConfiguration.InputOverlayKeyGap)?.Value ?? 20;

            screenCenterGapBindable.BindValueChanged(_ => updatePositions(), true);
            showBindable.BindValueChanged(valueChanged => Alpha = valueChanged.NewValue ? 1f : 0f, true);
        }

        private void updatePositions()
        {
            Vector2[] positions = PositionExtensions.GetPositions(screenCenterGap, keyGap, true, Anchor.Centre);
            for (int i = 0; i < 8; i++)
                inputOverlayKeys[i].Position = positions[i];

            inputOverlayBackgrounds[0].Position = new Vector2(-screenCenterGap, 0);
            inputOverlayBackgrounds[1].Position = new Vector2(screenCenterGap, 0);
        }

        public void PressKey(SoyokazeAction button)
        {
            inputOverlayKeys[(int)button].ScaleTo(press_scale, press_duration);
        }

        public void UnpressKey(SoyokazeAction button)
        {
            inputOverlayKeys[(int)button].ScaleTo(1f, press_duration);
        }
    }
}

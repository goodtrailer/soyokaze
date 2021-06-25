// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Configuration;
using osu.Game.Rulesets.Configuration;
using osu.Game.Rulesets.Soyokaze.Extensions;

namespace osu.Game.Rulesets.Soyokaze.Configuration
{
    public class SoyokazeConfigManager : RulesetConfigManager<SoyokazeConfig>
    {
        public SoyokazeConfigManager(SettingsStore settings, RulesetInfo ruleset, int? variant = null)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();

            SetDefault(SoyokazeConfig.HitCircleScreenCenterDistance, 220, 0, PositionExtensions.SCREEN_WIDTH / 2);
            SetDefault(SoyokazeConfig.HitCircleGap, 120, 0, PositionExtensions.SCREEN_WIDTH / 2);

            SetDefault(SoyokazeConfig.JudgementScreenCenterDistance, 220, 0, PositionExtensions.SCREEN_WIDTH / 2);
            SetDefault(SoyokazeConfig.JudgementGap, 120, 0, PositionExtensions.SCREEN_WIDTH / 2);

            SetDefault(SoyokazeConfig.InputOverlayScreenCenterDistance, 220, 0, PositionExtensions.SCREEN_WIDTH / 2);
            SetDefault(SoyokazeConfig.InputOverlayGap, 20, 0, PositionExtensions.SCREEN_WIDTH / 2);

            SetDefault(SoyokazeConfig.KiaiVisualizerScreenCenterDistance, 220, 0, PositionExtensions.SCREEN_WIDTH / 2);

            SetDefault(SoyokazeConfig.HighlightHolds, true);
            SetDefault(SoyokazeConfig.ShowInputOverlay, true);
            SetDefault(SoyokazeConfig.ShowKiaiVisualizer, true);

            SetDefault(SoyokazeConfig.HoldHighlightColour, ColourEnum.None);
        }
    }
}

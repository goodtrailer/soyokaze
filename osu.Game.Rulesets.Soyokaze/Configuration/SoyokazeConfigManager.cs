﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Configuration;
using osu.Game.Rulesets.Configuration;
using osu.Game.Rulesets.Soyokaze.Extensions;

namespace osu.Game.Rulesets.Soyokaze.Configuration
{
    public class SoyokazeConfigManager : RulesetConfigManager<SoyokazeConfig>
    {
        public const int DEFAULT_HIT_CIRCLE_SCREEN_CENTER_DISTANCE = 280;
        public const int DEFAULT_HIT_CIRCLE_GAP = 120;
        public const int DEFAULT_JUDGEMENT_SCREEN_CENTER_DISTANCE = 280;
        public const int DEFAULT_JUDGEMENT_GAP = 120;

        public SoyokazeConfigManager(SettingsStore settings, RulesetInfo ruleset, int? variant = null)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();

            SetDefault(SoyokazeConfig.HitCircleScreenCenterDistance, DEFAULT_HIT_CIRCLE_SCREEN_CENTER_DISTANCE, 0, PositionExtensions.SCREEN_WIDTH / 2);
            SetDefault(SoyokazeConfig.HitCircleGap, DEFAULT_HIT_CIRCLE_GAP, 0, PositionExtensions.SCREEN_WIDTH / 2);
            SetDefault(SoyokazeConfig.JudgementScreenCenterDistance, DEFAULT_JUDGEMENT_SCREEN_CENTER_DISTANCE, 0, PositionExtensions.SCREEN_WIDTH / 2);
            SetDefault(SoyokazeConfig.JudgementGap, DEFAULT_JUDGEMENT_GAP, 0, PositionExtensions.SCREEN_WIDTH / 2);
        }
    }
}
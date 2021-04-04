﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Soyokaze.Configuration;

namespace osu.Game.Rulesets.Soyokaze.UI
{
    public class SoyokazeSettingsSubsection : RulesetSettingsSubsection
    {
        protected override string Header => "soyokaze!";

        public SoyokazeSettingsSubsection(Ruleset ruleset)
            : base(ruleset)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            if (!(Config is SoyokazeConfigManager configManager))
                return;

            Children = new Drawable[]
            {
                new SettingsSlider<int>
                {
                    LabelText = "Hit Circle distance from screen center",
                    Current = configManager.GetBindable<int>(SoyokazeConfig.HitCircleScreenCenterDistance),
                    ShowsDefaultIndicator = true,
                },
                new SettingsSlider<int>
                {
                    LabelText = "Hit Circle gap",
                    Current = configManager.GetBindable<int>(SoyokazeConfig.HitCircleGap),
                    ShowsDefaultIndicator = true,
                },
                new SettingsSlider<int>
                {
                    LabelText = "Judgement distance from screen center",
                    Current = configManager.GetBindable<int>(SoyokazeConfig.JudgementScreenCenterDistance),
                    ShowsDefaultIndicator = true,
                },
                new SettingsSlider<int>
                {
                    LabelText = "Judgement gap",
                    Current = configManager.GetBindable<int>(SoyokazeConfig.JudgementGap),
                    ShowsDefaultIndicator = true,
                },
                new SettingsSlider<int>
                {
                    LabelText = "Input Overlay distance from screen center",
                    Current = configManager.GetBindable<int>(SoyokazeConfig.InputOverlayScreenCenterDistance),
                    ShowsDefaultIndicator = true,
                },
                new SettingsSlider<int>
                {
                    LabelText = "Input Overlay gap",
                    Current = configManager.GetBindable<int>(SoyokazeConfig.InputOverlayGap),
                    ShowsDefaultIndicator = true,
                },
            };
        }
    }
}

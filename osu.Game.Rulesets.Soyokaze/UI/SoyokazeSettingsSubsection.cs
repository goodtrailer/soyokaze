// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Soyokaze.Configuration;

namespace osu.Game.Rulesets.Soyokaze.UI
{
    public partial class SoyokazeSettingsSubsection : RulesetSettingsSubsection
    {
        protected override LocalisableString Header => "soyokaze!";

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
                new SettingsCheckbox
                {
                    LabelText = "Highlight Holds (for +HO)",
                    Current = configManager.GetBindable<bool>(SoyokazeConfig.HighlightHolds),
                    ShowsDefaultIndicator = true,
                },
                new SettingsEnumDropdown<ColourEnum>
                {
                    LabelText = "Hold highlight colour (None = use skin's config or default)",
                    Current = configManager.GetBindable<ColourEnum>(SoyokazeConfig.HoldHighlightColour),
                    ShowsDefaultIndicator = true,
                },
                new SettingsCheckbox
                {
                    LabelText = "Show Input Overlay",
                    Current = configManager.GetBindable<bool>(SoyokazeConfig.ShowInputOverlay),
                    ShowsDefaultIndicator = true,
                },
                new SettingsCheckbox
                {
                    LabelText = "Show Kiai Visualizer",
                    Current = configManager.GetBindable<bool>(SoyokazeConfig.ShowKiaiVisualizer),
                    ShowsDefaultIndicator = true,
                },
                new SettingsSlider<int>
                {
                    LabelText = "Screen center gap",
                    Current = configManager.GetBindable<int>(SoyokazeConfig.ScreenCenterGap),
                    ShowsDefaultIndicator = true,
                },
                new SettingsSlider<int>
                {
                    LabelText = "Object gap",
                    Current = configManager.GetBindable<int>(SoyokazeConfig.ObjectGap),
                    ShowsDefaultIndicator = true,
                },
            };
        }
    }
}

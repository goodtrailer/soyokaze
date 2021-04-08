// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Soyokaze.UI
{
    public class SoyokazeInputManager : RulesetInputManager<SoyokazeAction>
    {
        public SoyokazeInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {
        }
    }

    public enum SoyokazeAction
    {
        [Description("Button 1 (DPAD-L ↑)")]
        Button0,

        [Description("Button 2 (DPAD-L ←)")]
        Button1,

        [Description("Button 3 (DPAD-L ↓)")]
        Button2,

        [Description("Button 4 (DPAD-L →)")]
        Button3,

        [Description("Button 5 (DPAD-R ↑)")]
        Button4,

        [Description("Button 6 (DPAD-R ←)")]
        Button5,

        [Description("Button 7 (DPAD-R ↓)")]
        Button6,

        [Description("Button 8 (DPAD-R →)")]
        Button7,
    }
}

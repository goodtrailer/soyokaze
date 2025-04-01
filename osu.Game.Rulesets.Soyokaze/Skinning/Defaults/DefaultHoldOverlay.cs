// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Graphics;

namespace osu.Game.Rulesets.Soyokaze.Skinning.Defaults
{
    public partial class DefaultHoldOverlay : AbstractDefaultComponent
    {
        protected override SoyokazeSkinComponents Component => SoyokazeSkinComponents.HoldOverlay;

        public DefaultHoldOverlay()
        {
            AutoSizeAxes = Axes.Both;
        }
    }
}

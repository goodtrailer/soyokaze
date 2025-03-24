// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.Extensions;
using osu.Game.Rulesets.Soyokaze.UI;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables
{
    public partial class DrawableSoyokazeJudgement : DrawableJudgement
    {
        [Resolved]
        private SoyokazeConfigManager configManager { get; set; } = null;

        public DrawableSoyokazeJudgement()
            : base()
        {
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
        }

        protected override Drawable CreateDefaultJudgement(HitResult result) => new DefaultJudgementPiece(result);

        public override void Apply(JudgementResult result, DrawableHitObject judgedObject)
        {
            base.Apply(result, judgedObject);

            SoyokazeAction button = (judgedObject as DrawableSoyokazeHitObject)?.ButtonBindable.Value ?? default;
            int screenCenterDistance = configManager?.Get<int>(SoyokazeConfig.ScreenCenterGap) ?? 0;
            int gap = configManager?.Get<int>(SoyokazeConfig.ObjectGap) ?? 0;

            Vector2[] positions = PositionExtensions.GetPositions(screenCenterDistance, gap, true, Anchor.Centre);
            Position = positions[(int)button];
        }
    }
}

// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

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
    public class DrawableSoyokazeJudgement : DrawableJudgement
    {
        public DrawableSoyokazeJudgement(JudgementResult result, DrawableHitObject drawableObject, SoyokazeConfigManager configManager)
            : base(result, drawableObject)
        {
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;

            SoyokazeAction button = (drawableObject as DrawableSoyokazeHitObject)?.ButtonBindable.Value ?? default;
            int screenCenterDistance = configManager?.Get<int>(SoyokazeConfig.ScreenCenterGap) ?? 0;
            int gap = configManager?.Get<int>(SoyokazeConfig.ObjectGap) ?? 0;

            Vector2[] positions = PositionExtensions.GetPositions(screenCenterDistance, gap, true, Anchor.Centre);
            Position = positions[(int)button];
        }

        protected override Drawable CreateDefaultJudgement(HitResult result) => new DefaultJudgementPiece(result);
    }
}

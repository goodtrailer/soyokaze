// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osuTK;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Extensions;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables
{
    public class DrawableSoyokazeJudgement : DrawableJudgement
    {
        public const int JUDGEMENT_CENTER_DISTANCE = 280;
        public const int JUDGEMENT_GAP = 120;
        public DrawableSoyokazeJudgement(JudgementResult result, DrawableHitObject hitObject)
            : base(result, hitObject)
        {
            Origin = Anchor.Centre;
            Vector2[] positions = PositionExtensions.GetPositions(JUDGEMENT_CENTER_DISTANCE, JUDGEMENT_GAP, true);
            SoyokazeAction button = (hitObject as DrawableSoyokazeHitObject)?.ButtonBindable.Value ?? SoyokazeAction.Button0;
            Position = positions[(int)button];
        }

        protected override Drawable CreateDefaultJudgement(HitResult result) => new DefaultJudgementPiece(result);
    }
}

// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Judgements;
using osu.Game.Rulesets.Soyokaze.UI;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Objects.Drawables
{
    public partial class DrawableHoldCircle : DrawableHitCircle
    {
        public new HoldCircle HitObject => (HoldCircle)base.HitObject;
        public override bool DisplayResult => false;
        public JudgementResult TrueResult { get; private set; }
        public double TrueTimeOffset { get; private set; }

        protected DrawableHold Hold => (DrawableHold)ParentHitObject;

        public override void Shake(double maximumLength)
        {
            Hold.Shake(maximumLength);
        }

        public override bool Hit(SoyokazeAction action)
        {
            if (Judged)
                return false;

            SoyokazeAction validAction = ButtonBindable.Value;
            if (action != validAction)
                return false;

            UpdateResult(true);

            return true;
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (!userTriggered)
            {
                if (!HitObject.HitWindows.CanBeHit(timeOffset))
                {
                    TrueResult = new JudgementResult(HitObject, new SoyokazeJudgement()) { Type = HitResult.Miss };
                    TrueTimeOffset = timeOffset;
                    ApplyResult(r => r.Type = HitResult.IgnoreMiss);
                }
                return;
            }

            HitResult result = HitObject.HitWindows.ResultFor(timeOffset);

            if (result == HitResult.None)
            {
                // shake, but make sure not to exceed into the window where you can actually miss
                Shake(-timeOffset - HitObject.HitWindows.WindowFor(HitResult.Miss));
                return;
            }

            TrueResult = new JudgementResult(HitObject, new SoyokazeJudgement()) { Type = result };
            TrueTimeOffset = timeOffset;
            ApplyResult(r => r.Type = HitResult.IgnoreHit);
        }

        protected override void UpdatePosition()
        {
            Position = Vector2.Zero;
        }

        protected override void UpdateScale()
        {
            Scale = Vector2.One;
        }
    }
}

// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Objects;

namespace osu.Game.Rulesets.Soyokaze.UI
{
    internal class SoyokazeHitObjectLifetimeEntry : HitObjectLifetimeEntry
    {
        public SoyokazeHitObjectLifetimeEntry(HitObject hitObject)
            : base(hitObject)
        {
            LifetimeEnd = HitObject.GetEndTime() + HitObject.HitWindows.WindowFor(HitResult.Miss);
        }

        protected override double InitialLifetimeOffset => (HitObject as SoyokazeHitObject)?.Preempt ?? SoyokazeHitObject.BASE_PREEMPT;
    }
}

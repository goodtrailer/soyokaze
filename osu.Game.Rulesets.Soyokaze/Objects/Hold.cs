// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Threading;
using osu.Game.Rulesets.Objects.Types;

namespace osu.Game.Rulesets.Soyokaze.Objects
{
    public class Hold : SoyokazeHitObject, IHasDuration
    {
        public double EndTime
        {
            get => StartTime + Duration;
            set => Duration = value - StartTime;
        }

        public double Duration { get; set; }

        protected override void CreateNestedHitObjects(CancellationToken cancellationToken)
        {
            base.CreateNestedHitObjects(cancellationToken);

            AddNested(new HoldCircle
            {
                Button = Button,
                Samples = Samples,
                StartTime = StartTime,
                NewCombo = NewCombo,
                ComboOffset = ComboOffset,
            });
        }
    }
}

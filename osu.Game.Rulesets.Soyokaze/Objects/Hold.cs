// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Threading;
using osu.Game.Audio;
using osu.Game.Rulesets.Objects.Types;

namespace osu.Game.Rulesets.Soyokaze.Objects
{
    public class Hold : SoyokazeHitObject, IHasDuration
    {
        private IList<HitSampleInfo> startSamples = new List<HitSampleInfo>();
        public IList<HitSampleInfo> StartSamples
        {
            get => holdCircle?.Samples ?? startSamples;
            set
            {
                startSamples = value;
                if (holdCircle != null)
                    holdCircle.Samples = value;
            }
        }

        public IList<HitSampleInfo> HoldSamples { get; set; } = new List<HitSampleInfo>();

        public IList<HitSampleInfo> EndSamples
        {
            get => Samples;
            set => Samples = value;
        }

        private HoldCircle holdCircle;

        public double EndTime
        {
            get => StartTime + Duration;
            set => Duration = value - StartTime;
        }

        public double Duration { get; set; }

        protected override void CreateNestedHitObjects(CancellationToken cancellationToken)
        {
            base.CreateNestedHitObjects(cancellationToken);

            AddNested(holdCircle = new HoldCircle
            {
                Button = Button,
                Samples = StartSamples,
                StartTime = StartTime,
                NewCombo = NewCombo,
                ComboOffset = ComboOffset,
            });
        }
    }
}

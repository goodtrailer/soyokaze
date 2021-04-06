// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Judgements;
using osu.Game.Rulesets.Soyokaze.UI;

namespace osu.Game.Rulesets.Soyokaze.Objects
{
    public class SoyokazeHitObject : HitObject, IHasComboInformation
    {
        /// <summary>
        /// The radius of circles (radius does not change between circles, but scale does)
        /// </summary>
        public const float OBJECT_RADIUS = 64;

        /// <summary>
        /// The base preempt time (AR10).
        /// </summary>
        public const double BASE_PREEMPT = 450;

        /// <summary>
        /// The max (normal) fade in rate (can go lower for AR>10)
        /// </summary>
        public const double BASE_FADEIN = 400;

        public readonly Bindable<SoyokazeAction> ButtonBindable = new Bindable<SoyokazeAction>();

        public SoyokazeAction Button
        {
            get => ButtonBindable.Value;
            set => ButtonBindable.Value = value;
        }

        public readonly Bindable<float> ScaleBindable = new Bindable<float>(1f);

        public float Scale
        {
            get => ScaleBindable.Value;
            set => ScaleBindable.Value = value;
        }

        public double Preempt = BASE_PREEMPT;
        public double FadeIn = BASE_FADEIN;

        // HitObject Impl ----------------------------------------------
        protected override void ApplyDefaultsToSelf(ControlPointInfo controlPointInfo, BeatmapDifficulty difficulty)
        {
            base.ApplyDefaultsToSelf(controlPointInfo, difficulty);

            Preempt = (float)BeatmapDifficulty.DifficultyRange(difficulty.ApproachRate, 1800, 1200, BASE_PREEMPT);

            // i refuse to use Math.Min because min funcs are the hardest thing to read i swear. even though i see them it all the time in math LOL
            FadeIn = BASE_FADEIN;
            if (Preempt < BASE_PREEMPT)
                FadeIn *= Preempt / BASE_PREEMPT;

            Scale = (1.0f - 0.7f * (difficulty.CircleSize - 5) / 5) * 0.8f; // idk why, but the scaling is off by a factor of 1.6 from std.
        }

        public override Judgement CreateJudgement() => new SoyokazeJudgement();

        protected override HitWindows CreateHitWindows() => new HitWindows();

        // IHasComboInformation Impl -----------------------------------

        public readonly Bindable<int> ComboOffsetBindable = new Bindable<int>();

        public int ComboOffset
        {
            get => ComboOffsetBindable.Value;
            set => ComboOffsetBindable.Value = value;
        }

        public Bindable<int> IndexInCurrentComboBindable { get; } = new Bindable<int>();

        public int IndexInCurrentCombo
        {
            get => IndexInCurrentComboBindable.Value;
            set => IndexInCurrentComboBindable.Value = value;
        }

        public Bindable<int> ComboIndexBindable { get; } = new Bindable<int>();

        public int ComboIndex
        {
            get => ComboIndexBindable.Value;
            set => ComboIndexBindable.Value = value;
        }

        public Bindable<bool> LastInComboBindable { get; } = new Bindable<bool>();

        public bool LastInCombo
        {
            get => LastInComboBindable.Value;
            set => LastInComboBindable.Value = value;
        }

        public bool NewCombo { get; set; }
    }
}

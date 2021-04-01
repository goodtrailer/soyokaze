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
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Objects
{
    public class SoyokazeHitObject : HitObject, IHasPosition, IHasComboInformation
    {
        /// <summary>
        /// The base size of circles (not like CS really applies to ballad of breeze though)
        /// </summary>
        public const float BASE_SIZE = 128;

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

        public readonly Bindable<Vector2> SizeBindable = new Bindable<Vector2>(new Vector2(BASE_SIZE));

        public Vector2 Size
        {
            get => SizeBindable.Value;
            set => SizeBindable.Value = value;
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
        }

        public override Judgement CreateJudgement() => new SoyokazeJudgement();

        protected override HitWindows CreateHitWindows() => new HitWindows();

        // IHasPosition Impl -------------------------------------------

        public readonly Bindable<Vector2> PositionBindable = new Bindable<Vector2>();

        public Vector2 Position
        {
            get => PositionBindable.Value;
            set => PositionBindable.Value = value;
        }

        public float X => Position.X;
        public float Y => Position.Y;

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

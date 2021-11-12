// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using osu.Framework.Bindables;
using osu.Framework.Logging;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Soyokaze.Extensions;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.UI;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Beatmaps
{
    public class SoyokazeBeatmapConverter : BeatmapConverter<SoyokazeHitObject>
    {
        public BindableBool CreateHolds { get; } = new BindableBool(true);

        public SoyokazeBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
        }

        public override bool CanConvert() => Beatmap.HitObjects.All(h => h is IHasPosition);

        protected override IEnumerable<SoyokazeHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap, CancellationToken cancellationToken)
        {
            IHasPosition positionData = original as IHasPosition;
            IHasCombo comboData = original as IHasCombo;

            Vector2 originalPosition = positionData?.Position ?? Vector2.Zero;
            int column = 0, row = 0;
            for (int i = 1; i < PositionExtensions.NUM_COLUMNS; i++)
                if (originalPosition.X > i * PositionExtensions.BEATMAP_WIDTH / PositionExtensions.NUM_COLUMNS)
                    column = i;
            for (int i = 1; i < PositionExtensions.NUM_ROWS; i++)
                if (originalPosition.Y > i * PositionExtensions.BEATMAP_HEIGHT / PositionExtensions.NUM_ROWS)
                    row = i;
            SoyokazeAction button = (SoyokazeAction)PositionExtensions.PositionToButton(column + PositionExtensions.NUM_COLUMNS * row);

            SoyokazeHitObject hitObject;
            switch (original)
            {
                case IHasPathWithRepeats slider:
                    if (!CreateHolds.Value)
                        goto default;
                    hitObject = new Hold
                    {
                        Duration = slider.Duration,
                        StartSamples = slider.NodeSamples[0],
                        HoldSamples = original.Samples,
                        EndSamples = slider.NodeSamples[slider.NodeSamples.Count - 1],
                    };
                    break;

                case IHasDuration longNote:
                    if (!CreateHolds.Value)
                        goto default;
                    hitObject = new Hold
                    {
                        Duration = longNote.Duration,
                        EndSamples = original.Samples,
                    };
                    break;

                default:
                    hitObject = new HitCircle
                    {
                        Samples = original.Samples,
                    };
                    break;
            }

            hitObject.Button = button;
            hitObject.StartTime = original.StartTime;
            hitObject.NewCombo = comboData?.NewCombo ?? false;
            hitObject.ComboOffset = comboData?.ComboOffset ?? 0;

            yield return hitObject;
        }
    }
}

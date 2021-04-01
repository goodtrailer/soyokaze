// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public const int HIT_OBJECT_CENTER_DISTANCE = 280;
        public const int HIT_OBJECT_GAP = 120;

        public SoyokazeBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
        }

        public override bool CanConvert() => Beatmap.HitObjects.All(h => h is IHasPosition);

        protected override IEnumerable<SoyokazeHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap, CancellationToken cancellationToken)
        {
            HitCircle hitCircle = new HitCircle();
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

            Vector2[] hitObjectPositions = PositionExtensions.GetPositions(HIT_OBJECT_CENTER_DISTANCE, HIT_OBJECT_GAP);

            hitCircle.Position = hitObjectPositions[column + PositionExtensions.NUM_COLUMNS * row];
            hitCircle.Button = (SoyokazeAction)PositionExtensions.PositionToButton(column, row);
            hitCircle.Samples = original.Samples;
            hitCircle.StartTime = original.StartTime;
            hitCircle.NewCombo = comboData?.NewCombo ?? false;
            hitCircle.ComboOffset = comboData?.ComboOffset ?? 0;

            yield return hitCircle;
        }
    }
}

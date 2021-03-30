// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using osuTK;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.Extensions;

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
            Vector2 originalPosition = (original as IHasPosition)?.Position ?? Vector2.Zero;

            hitCircle.Samples = original.Samples;
            hitCircle.StartTime = original.StartTime;

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

            yield return hitCircle;
        }
    }
}

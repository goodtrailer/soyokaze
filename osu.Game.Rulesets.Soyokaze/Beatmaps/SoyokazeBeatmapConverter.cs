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

namespace osu.Game.Rulesets.Soyokaze.Beatmaps
{
    public class SoyokazeBeatmapConverter : BeatmapConverter<SoyokazeHitObject>
    {
        private const int beatmapWidth = 512;
        private const int beatmapHeight = 384;
        
        private const int hitObjectCenterDistance = 280;
        private const int hitObjectGap = 120;
        private const int screenWidth = 1366;
        private const int screenHeight = 768;
        // AWJI SDKL : 1054 2367
        public static readonly Vector2[] HitObjectPositions =
        {
            new Vector2(screenWidth/2 - hitObjectCenterDistance - hitObjectGap,        screenHeight/2               ),
            new Vector2(screenWidth/2 - hitObjectCenterDistance,                       screenHeight/2 - hitObjectGap),
            new Vector2(screenWidth/2 + hitObjectCenterDistance - hitObjectGap,        screenHeight/2               ),
            new Vector2(screenWidth/2 + hitObjectCenterDistance,                       screenHeight/2 - hitObjectGap),

            new Vector2(screenWidth/2 - hitObjectCenterDistance,                       screenHeight/2 + hitObjectGap),
            new Vector2(screenWidth/2 - hitObjectCenterDistance + hitObjectGap,        screenHeight/2               ),
            new Vector2(screenWidth/2 + hitObjectCenterDistance,                       screenHeight/2 + hitObjectGap),
            new Vector2(screenWidth/2 + hitObjectCenterDistance + hitObjectGap,        screenHeight/2               )
        };

        public SoyokazeBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
        }

        public override bool CanConvert() => Beatmap.HitObjects.All(h => h is IHasPosition);

        public int ScreenSlotToIndex(int column, int row)
        {
            if (row == 0)
                switch (column)
                {
                    case 0:
                        return 1;
                    case 1:
                        return 0;
                    case 2:
                        return 5;
                    case 3:
                        return 4;
                }
            else if (row == 1)
                switch (column)
                {
                    case 0:
                        return 2;
                    case 1:
                        return 3;
                    case 2:
                        return 6;
                    case 3:
                        return 7;
                }
            return -1;
        }

        protected override IEnumerable<SoyokazeHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap, CancellationToken cancellationToken)
        {
            HitCircle hitCircle = new HitCircle();
            Vector2 originalPosition = (original as IHasPosition)?.Position ?? Vector2.Zero;

            hitCircle.Samples = original.Samples;
            hitCircle.StartTime = original.StartTime;

            int column = 0, row = 0;
            for (int i = 1; i < 4; i++)
                if (originalPosition.X > i * beatmapWidth / 4)
                    column = i;
            if (originalPosition.Y > beatmapHeight / 2)
                row = 1;

            hitCircle.Position = HitObjectPositions[column + 4 * row];
            hitCircle.Button = (SoyokazeAction)ScreenSlotToIndex(column, row); 

            yield return hitCircle;
        }
    }
}

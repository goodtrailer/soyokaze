// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Extensions.EnumExtensions;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Soyokaze.UI;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Extensions
{
    public static class PositionExtensions
    {
        public const int SCREEN_WIDTH = 1366;
        public const int SCREEN_HEIGHT = 768;
        public static readonly Vector2 SCREEN_DIMS = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT);
        public static readonly Vector2 SCREEN_CENTER = new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);
        public const int BEATMAP_WIDTH = 512;
        public const int BEATMAP_HEIGHT = 384;
        public static readonly Vector2 BEATMAP_DIMS = new Vector2(BEATMAP_WIDTH, BEATMAP_HEIGHT);
        public static readonly Vector2 BEATMAP_CENTER = new Vector2(BEATMAP_WIDTH / 2, BEATMAP_HEIGHT / 2);
        public const int NUM_COLUMNS = 4;
        public const int NUM_ROWS = 2;

        public static Vector2[] GetPositions(int screenCenterGap, int objectGap, bool inButtonOrder, Anchor origin)
        {
            Vector2 offset = new Vector2(0);

            if (origin.HasFlag(Anchor.x0))
                offset.X = SCREEN_WIDTH / 2;
            else if (origin.HasFlag(Anchor.x2))
                offset.X = -SCREEN_WIDTH / 2;

            if (origin.HasFlag(Anchor.y0))
                offset.Y = SCREEN_HEIGHT / 2;
            else if (origin.HasFlag(Anchor.y2))
                offset.Y = -SCREEN_HEIGHT / 2;

            Vector2[] positions = new Vector2[]
            {
                new Vector2(offset.X - screenCenterGap - objectGap,        offset.Y      ),
                new Vector2(offset.X - screenCenterGap,              offset.Y - objectGap),
                new Vector2(offset.X + screenCenterGap - objectGap,        offset.Y      ),
                new Vector2(offset.X + screenCenterGap,              offset.Y - objectGap),

                new Vector2(offset.X - screenCenterGap,              offset.Y + objectGap),
                new Vector2(offset.X - screenCenterGap + objectGap,        offset.Y      ),
                new Vector2(offset.X + screenCenterGap,              offset.Y + objectGap),
                new Vector2(offset.X + screenCenterGap + objectGap,        offset.Y      )
            };

            if (!inButtonOrder)
                return positions;
            else
                return new Vector2[]
                {
                    positions[1],
                    positions[0],
                    positions[4],
                    positions[5],

                    positions[3],
                    positions[2],
                    positions[6],
                    positions[7]
                };
        }

        public static float ButtonToRotation(SoyokazeAction button)
        {
            switch (button)
            {
                case SoyokazeAction.Button0:
                case SoyokazeAction.Button4:
                    return 0f;

                case SoyokazeAction.Button1:
                case SoyokazeAction.Button5:
                    return 270f;

                case SoyokazeAction.Button2:
                case SoyokazeAction.Button6:
                    return 180f;

                case SoyokazeAction.Button3:
                case SoyokazeAction.Button7:
                    return 90f;

                default:
                    return float.NaN;
            }
        }

        public static int PositionToButton(int positionIndex)
        {
            switch (positionIndex)
            {
                case 0:
                    return 1;
                case 1:
                    return 0;
                case 2:
                    return 5;
                case 3:
                    return 4;
                case 4:
                    return 2;
                case 5:
                    return 3;
                case 6:
                    return 6;
                case 7:
                    return 7;
                default:
                    return -1;
            }
        }
    }
}

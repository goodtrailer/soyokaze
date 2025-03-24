// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Scoring;
using osu.Game.Screens.Ranking.Statistics;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Statistics
{
    public partial class AccuracyGraph : FillFlowContainer
    {
        public AccuracyGraph(List<HitEvent> hitEvents)
        {
            Direction = FillDirection.Vertical;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(200f, 140f);

            AddRange(new Drawable[]
            {
                new SpriteText
                {
                    Text = (calculateAccuracy(hitEvents) * 100).ToString("0.00") + "%",
                    Colour = Color4Extensions.FromHex("#66FFCC"),
                    Font = OsuFont.Torus.With(size: 42),
                    AllowMultiline = false,

                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new UnstableRate(hitEvents)
                {
                    AutoSizeAxes = Axes.None,

                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.8f, 0.13f),
                },
                new MiniHitEventTimingDistributionGraph(hitEvents)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f, 0.4f),
                },
                new SpriteText
                {
                    Text = hitEvents.Count.ToString() + " objects",
                    Colour = Color4Extensions.FromHex("#66FFCC"),
                    Font = OsuFont.Torus.With(size: 12),
                    AllowMultiline = false,

                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
            });
        }

        private float calculateAccuracy(List<HitEvent> hitEvents)
        {
            int maxScore = 0;
            int score = 0;

            SoyokazeScoreProcessor scorer = new SoyokazeScoreProcessor();
            foreach (HitEvent hitEvent in hitEvents)
            {
                score += scorer.GetBaseScoreForResult(hitEvent.Result);
                maxScore += scorer.GetBaseScoreForResult(hitEvent.HitObject.CreateJudgement().MaxResult);
            }

            return (float)score / maxScore;
        }
    }
}

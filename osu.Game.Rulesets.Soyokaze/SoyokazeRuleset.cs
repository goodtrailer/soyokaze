﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Configuration;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Replays.Types;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Soyokaze.Beatmaps;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.Difficulty;
using osu.Game.Rulesets.Soyokaze.Extensions;
using osu.Game.Rulesets.Soyokaze.Mods;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.Replays;
using osu.Game.Rulesets.Soyokaze.Scoring;
using osu.Game.Rulesets.Soyokaze.Skinning.Legacy;
using osu.Game.Rulesets.Soyokaze.Statistics;
using osu.Game.Rulesets.Soyokaze.UI;
using osu.Game.Rulesets.UI;
using osu.Game.Scoring;
using osu.Game.Screens.Ranking.Statistics;
using osu.Game.Skinning;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze
{
    public partial class SoyokazeRuleset : Ruleset
    {
        public const string SHORT_NAME = "soyokaze";

        public override string Description => "soyokaze!";
        public override string ShortName => SHORT_NAME;
        public override string PlayingVerb => "Feeling the wind";

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) =>
            new SoyokazeBeatmapConverter(beatmap, this);

        public override IBeatmapProcessor CreateBeatmapProcessor(IBeatmap beatmap) => new BeatmapProcessor(beatmap);

        public override IRulesetConfigManager CreateConfig(SettingsStore settings) => new SoyokazeConfigManager(settings, RulesetInfo);

        public override ScoreProcessor CreateScoreProcessor() => new SoyokazeScoreProcessor(this);

        public override RulesetSettingsSubsection CreateSettings() => new SoyokazeSettingsSubsection(this);

        public override DifficultyCalculator CreateDifficultyCalculator(IWorkingBeatmap beatmap) =>
            new SoyokazeDifficultyCalculator(RulesetInfo, beatmap);

        public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) =>
            new DrawableSoyokazeRuleset(this, beatmap, mods);

        public override ISkin CreateSkinTransformer(ISkin skin, IBeatmap beatmap)
        {
            switch (skin)
            {
                case LegacySkin:
                    return new SoyokazeLegacySkinTransformer(skin);

                default:
                    return null;
            }
        }

        public override IConvertibleReplayFrame CreateConvertibleReplayFrame() => new SoyokazeReplayFrame();

        public override StatisticItem[] CreateStatisticsForScore(ScoreInfo score, IBeatmap playableBeatmap)
        {
            var hitEventLists = new List<HitEvent>[8];
            for (int i = 0; i < hitEventLists.Length; i++)
                hitEventLists[i] = new List<HitEvent>();

            foreach (var hitEvent in score.HitEvents)
            {
                if (!(hitEvent.HitObject is SoyokazeHitObject soyokazeObject))
                    continue;

                hitEventLists[(int)soyokazeObject.Button].Add(hitEvent);
            }

            return new[]
            {
                new StatisticItem("Button Accuracies", () =>
                {
                    Container accuracyGraphsContainer = new Container()
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        AutoSizeAxes = Axes.Both,
                    };
                    Vector2[] positions = PositionExtensions.GetPositions(220, 110, true, Anchor.Centre);
                    for (int i = 0; i < positions.Length; i++)
                        accuracyGraphsContainer.Add(new AccuracyGraph(hitEventLists[i]) { Position = positions[i] });
                    return accuracyGraphsContainer;
                }, true),
                new StatisticItem("Overall Distribution", () => new HitEventTimingDistributionGraph(score.HitEvents)
                {
                    RelativeSizeAxes = Axes.X,
                    Size = new Vector2(1f, 100f)
                }, true),
                new StatisticItem("", () => new UnstableRate(score.HitEvents)
                {
                    AutoSizeAxes = Axes.None,
                    RelativeSizeAxes = Axes.X,
                    Size = new Vector2(0.2f, 10f)
                }, true),
            };
        }

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.DifficultyReduction:
                    return new Mod[]
                    {
                        new SoyokazeModEasy(),
                        new SoyokazeModNoFail(),
                        new MultiMod(new SoyokazeModHalfTime(), new SoyokazeModDaycore()),
                        new SoyokazeModStaccato(),
                    };

                case ModType.DifficultyIncrease:
                    return new Mod[]
                    {
                        new SoyokazeModHardRock(),
                        new MultiMod(new SoyokazeModSuddenDeath(), new SoyokazeModPerfect()),
                        new MultiMod(new SoyokazeModDoubleTime(), new SoyokazeModNightcore()),
                        new SoyokazeModHidden(),
                    };

                case ModType.Automation:
                    return new Mod[]
                    {
                        new MultiMod(new SoyokazeModAutoplay(), new SoyokazeModCinema()),
                    };

                case ModType.Conversion:
                    return new Mod[]
                    {
                        new SoyokazeModRandom(),
                        new SoyokazeModDifficultyAdjust(),
                    };

                case ModType.Fun:
                    return new Mod[]
                    {
                        new MultiMod(new ModWindUp(), new ModWindDown()),
                    };

                case ModType.System:
                    return new Mod[]
                    {
                        new SoyokazeModHolds(),
                    };

                default:
                    return new Mod[]
                    {
                    };
            }
        }

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.W, SoyokazeAction.Button0),
            new KeyBinding(InputKey.A, SoyokazeAction.Button1),
            new KeyBinding(InputKey.S, SoyokazeAction.Button2),
            new KeyBinding(InputKey.D, SoyokazeAction.Button3),
            new KeyBinding(InputKey.I, SoyokazeAction.Button4),
            new KeyBinding(InputKey.J, SoyokazeAction.Button5),
            new KeyBinding(InputKey.K, SoyokazeAction.Button6),
            new KeyBinding(InputKey.L, SoyokazeAction.Button7),
        };

        public override Drawable CreateIcon() => new SoyokazeIcon(this);

        public partial class SoyokazeIcon : Container
        {
            private Sprite sprite;
            private Ruleset ruleset;

            public SoyokazeIcon(Ruleset ruleset)
            {
                AutoSizeAxes = Axes.Both;
                Children = new Drawable[]
                {
                    new SpriteIcon
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Icon = FontAwesome.Regular.Circle,
                    },
                    sprite = new Sprite
                    {
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(1),
                        Scale = new Vector2(.65f),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                };

                this.ruleset = ruleset;
            }

            [BackgroundDependencyLoader]
            private void load(IRenderer renderer)
            {
                sprite.Texture = new TextureStore(renderer, new TextureLoaderStore(ruleset.CreateResourceStore()), false).Get("Textures/icon");
            }
        }
    }
}

﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Configuration;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Soyokaze.Beatmaps;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.Difficulty;
using osu.Game.Rulesets.Soyokaze.Mods;
using osu.Game.Rulesets.Soyokaze.Skinning.Legacy;
using osu.Game.Rulesets.Soyokaze.UI;
using osu.Game.Rulesets.UI;
using osu.Game.Skinning;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze
{
    public class SoyokazeRuleset : Ruleset
    {
        public const string SHORT_NAME = "soyokaze";

        public override string Description => "soyokaze!";
        public override string ShortName => SHORT_NAME;
        public override string PlayingVerb => "Feeling the wind";

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) =>
            new SoyokazeBeatmapConverter(beatmap, this);

        public override IBeatmapProcessor CreateBeatmapProcessor(IBeatmap beatmap) => new BeatmapProcessor(beatmap);

        public override IRulesetConfigManager CreateConfig(SettingsStore settings) => new SoyokazeConfigManager(settings, RulesetInfo);

        public override RulesetSettingsSubsection CreateSettings() => new SoyokazeSettingsSubsection(this);

        public override DifficultyCalculator CreateDifficultyCalculator(WorkingBeatmap beatmap) =>
            new SoyokazeDifficultyCalculator(this, beatmap);

        public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) =>
            new DrawableSoyokazeRuleset(this, beatmap, mods);

        public override ISkin CreateLegacySkinProvider(ISkinSource source, IBeatmap beatmap) => new SoyokazeLegacySkinTransformer(source);

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
                    };

                case ModType.DifficultyIncrease:
                    return new Mod[]
                    {
                        new SoyokazeModHardRock(),
                        new MultiMod(new SoyokazeModSuddenDeath(), new SoyokazeModPerfect()),
                        new MultiMod(new SoyokazeModDoubleTime(), new SoyokazeModNightcore()),
                    };

                case ModType.Conversion:
                    return new Mod[]
                    {
                        new SoyokazeModRandom(),
                        new SoyokazeModDifficultyAdjust(),
                    };

                case ModType.Automation:
                    return new Mod[]
                    {
                        new MultiMod(new SoyokazeModAutoplay(), new SoyokazeModCinema()),
                    };

                case ModType.Fun:
                    return new Mod[]
                    {
                        new MultiMod(new ModWindUp(), new ModWindDown()),
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

        public override Drawable CreateIcon() => new Container
        {
            AutoSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                new SpriteIcon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Icon = FontAwesome.Regular.Circle,
                },
                new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1),
                    Scale = new Vector2(.65f),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Texture = new TextureStore(new TextureLoaderStore(CreateResourceStore()), false).Get("Textures/icon"),
                },
            }
        };
    }
}

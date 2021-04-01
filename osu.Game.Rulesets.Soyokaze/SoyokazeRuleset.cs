// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Soyokaze.Beatmaps;
using osu.Game.Rulesets.Soyokaze.Mods;
using osu.Game.Rulesets.Soyokaze.UI;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze
{
    public class SoyokazeRuleset : Ruleset
    {
        public const string SHORT_NAME = "soyokaze";

        public override string Description => "soyokaze!";
        public override string ShortName => SHORT_NAME;
        public override string PlayingVerb => "Feeling the wind";

        public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) =>
            new DrawableSoyokazeRuleset(this, beatmap, mods);

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) =>
            new SoyokazeBeatmapConverter(beatmap, this);

        public override IBeatmapProcessor CreateBeatmapProcessor(IBeatmap beatmap) => new BeatmapProcessor(beatmap);

        public override DifficultyCalculator CreateDifficultyCalculator(WorkingBeatmap beatmap) =>
            new SoyokazeDifficultyCalculator(this, beatmap);

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.DifficultyReduction:
                    return new Mod[] { new SoyokazeModNoFail() };

                case ModType.Automation:
                    return new[] { new SoyokazeModAutoplay() };

                default:
                    return new Mod[] { null };
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

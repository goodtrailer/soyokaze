// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Soyokaze.Objects;
using osu.Game.Rulesets.Soyokaze.Replays;
using osu.Game.Rulesets.UI;
using osu.Game.Scoring;

namespace osu.Game.Rulesets.Soyokaze.UI
{
    [Cached]
    public partial class DrawableSoyokazeRuleset : DrawableRuleset<SoyokazeHitObject>
    {
        public DrawableSoyokazeRuleset(SoyokazeRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
        }

        protected override Playfield CreatePlayfield() => new SoyokazePlayfield();

        public override DrawableHitObject<SoyokazeHitObject> CreateDrawableRepresentation(SoyokazeHitObject h) => null;

        protected override PassThroughInputManager CreateInputManager() => new SoyokazeInputManager(Ruleset?.RulesetInfo);

        protected override ReplayRecorder CreateReplayRecorder(Score score) => new SoyokazeReplayRecorder(score);

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new SoyokazeFramedReplayInputHandler(replay);
    }
}

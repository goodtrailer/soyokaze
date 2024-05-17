// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Linq;
using osu.Game.Skinning;

namespace osu.Game.Rulesets.Soyokaze.Skinning
{
    public class SoyokazeSkinComponentLookup : GameplaySkinComponentLookup<SoyokazeSkinComponents>
    {
        public SoyokazeSkinComponentLookup(SoyokazeSkinComponents component)
            : base(component)
        {
        }

        // TODO: THIS IS A HORROR
        public string LookupName => ComponentName;

        public string StoreName => string.Join('/', new[] { "Gameplay", RulesetPrefix, ComponentName }.Where(s => !string.IsNullOrEmpty(s)));

        public string RulesetPrefix => SoyokazeRuleset.SHORT_NAME;

        public string ComponentName => Component.ToString().ToLower();
    }
}

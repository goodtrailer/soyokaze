<picture>
  <source media="(prefers-color-scheme: dark)" srcset="assets/logo_white.png">
  <source media="(prefers-color-scheme: light)" srcset="assets/logo.png">
  <img src="assets/logo.png" alt="soyokaze!" width="300" height="300">
</picture>

# soyokaze!

[![Version](https://img.shields.io/github/v/release/goodtrailer/soyokaze.svg?color=green&style=flat-square)](https://github.com/goodtrailer/soyokaze/releases/latest)
[![CodeFactor](https://www.codefactor.io/repository/github/goodtrailer/soyokaze/badge/main?style=flat-square)](https://www.codefactor.io/repository/github/goodtrailer/soyokaze/overview/main)
[![License](https://img.shields.io/github/license/goodtrailer/soyokaze.svg?color=blue&style=flat-square)](https://github.com/goodtrailer/soyokaze/blob/master/LICENSE)
[![Downloads](https://img.shields.io/github/downloads/goodtrailer/soyokaze/total.svg?color=orange&style=flat-square)](https://somsubhra.github.io/github-release-stats/?username=goodtrailer&repository=soyokaze&page=1&per_page=0)

***Note.*** soyokaze!'s source code is a mess and does not follow osu!'s conventions particularly well. If you want to write your own ruleset, look at osu!standard's or osu!taiko's. You are looking at a bargain-bin ruleset that I've been half-assedly maintaining for a while and for which I may eventually rewrite large portions of.

An [osu!](https://github.com/ppy/osu) ruleset mimicking [Genshin Impact](https://genshin.mihoyo.com)'s Ballads of Breeze [mini-game](https://youtu.be/ZsacXMduSFY). For more info, you can also check it out over at [rūrusetto](https://rulesets.info/rulesets/soyokaze), a website created by the [Rūrusetto team](https://github.com/Rurusetto) to catelog osu! rulesets.

## Video Showcase (YouTube)
[![Preview1](assets/preview1.png)](https://youtu.be/3Sj6tE2t4do)
| [![Preview0](assets/preview0.png)](https://youtu.be/hWjG0W7EiAE) | [![Preview2](assets/preview2.png)](https://youtu.be/uX0HBadqPzs) | [![Preview3](assets/preview3.png)](https://youtu.be/_QKinzhlMes) |
| --- | --- | --- |

## Skinning
Default skin PNGs (they're all white): [/osu.Game.Rulesets.Soyokaze/Resources/Textures/Gameplay/soyokaze](/osu.Game.Rulesets.Soyokaze/Resources/Textures/Gameplay/soyokaze). Judgements and Hit Circle text are skinnable too following normal [osu!standard skinning guidelines](https://osu.ppy.sh/wiki/en/Skinning/osu%21).

***Note.*** By default, skin PNGs will be taken from the skin's root folder (i.e. where stable looks for skin PNGs). So certain elements (hit circles, approach circles, judgements) will share the same PNGs as osu!std. However, soyokaze! will also look for skin PNGs inside a separate `soyokaze/` folder (i.e. `soyokaze/hitcircle@2x.png` instead of `hitcircle@2x.png`), so it is possible to differentiate between soyokaze! and osu!std. For better organization, *all* soyokaze! PNGs can be placed inside this `soyokaze/` folder.

`skin.ini` default values:
```
[General]
InputOverlayKeyGap: 20
KiaiVisualizerDefaultSpin: 1.5
KiaiVisualizerKiaiSpin: -60
KiaiVisualizerDefaultOpacity: 128
KiaiVisualizerFirstFlashOpacity: 255
KiaiVisualizerFlashOpacity: 152
RotateApproachCircle: false
RotateHitCircle: false
RotateHoldProgress: true
RotateInputOverlayKey: true

[Colours]
HoldHighlight: 0, 255, 0
KiaiVisualizerDefault: 47, 79, 79
KiaiVisualizerFirstFlash: 255, 255, 255
KiaiVisualizerFlash: 255, 255 255
```

## Extras
These are features that I think would be really fun, but are non-trivial to implement (unlike +DT, which took maybe 30 seconds). I may or may not implement these in the future depending on if I find the motivation to.
1. Star rating rework
    * osu!'s star ratings suck real bad for soyokaze!. I tried making my own star rating calculator, and it works *slightly* better. It still undervalues reading and rhythm complexity a ton.
3. Spinners mod (+SP)
    * Basically just note spam, taiko/MuseDash style
4. Multi-notes mod (+MT)
    * Pretty hard mod to implement, would have to look at ![LumpBloom7/sentakki](https://github.com/LumpBloom7/sentakki) for some clues, because I'm not sure how to decide when to do singles, doubles, and triples
5. Editor support
    * Currently unusable without a custom build of osu!, because the legacy beatmap decoder gets angry at non-legacy rulesets or something. Also definitely the hardest feature to implement by far

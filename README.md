<img src="assets/logo.png" alt="soyokaze!" width="300" height="300">

# soyokaze!

[![Version](https://img.shields.io/github/v/release/goodtrailer/soyokaze.svg?color=green&style=flat-square)](https://github.com/goodtrailer/soyokaze/releases/latest)
[![CodeFactor](https://www.codefactor.io/repository/github/goodtrailer/soyokaze/badge/main?style=flat-square)](https://www.codefactor.io/repository/github/goodtrailer/soyokaze/overview/main)
[![License](https://img.shields.io/github/license/goodtrailer/soyokaze.svg?color=blue&style=flat-square)](https://github.com/goodtrailer/soyokaze/blob/master/LICENSE)

An osu! ruleset mimicking Genshin Impact's Ballads of Breeze mini-game.

### Skinning
Default skin PNGs (they're all white): [/osu.Game.Rulesets.Soyokaze/Resources/Textures/Gameplay/soyokaze](/osu.Game.Rulesets.Soyokaze/Resources/Textures/Gameplay/soyokaze).

Judgements and Hit Circle text are skinnable too following normal [osu!standard skinning guidelines](https://osu.ppy.sh/wiki/en/Skinning/osu%21).

`skin.ini` default values:
```
[General]
KiaiVisualizerDefaultSpin: 1.5
KiaiVisualizerKiaiSpin: -60
KiaiVisualizerDefaultOpacity: 128
KiaiVisualizerFirstFlashOpacity: 255
KiaiVisualizerFlashOpacity: 192

[Colours]
KiaiVisualizerDefault: 47, 79, 79
KiaiVisualizerFirstFlash: 255, 255, 255
KiaiVisualizerFlash: 255, 255 255
```

### To-Do
1. ✓ Rudimentary hit circles spawning and approach circles.
1. ✓ Top-only hits (no piercing)
1. ✓ Shaking (only for early-hits)
1. ✓ Judgements
1. ✓ Combos/accents
1. ✓ Texturing/skinning
1. ✓ Configuration/settings
1. ✓ Key indicators
1. ✓ Check stats are scaling properly (HP, CS, OD)
1. ✓ Mods (+HT, +DC, +DT, +NC, +HR, +EZ, +DA, +AT, +CN, +NF, +SD, +PF, +WU, +WD, +RD)
1. ✓ Replays
1. ✓ Kiai visuals
1. ✓ End-game statistics
1. ✓ Difficulty calculation
1. Editor support (at the very least, prevent crashing)

### Extras
These are mods that I think would be really fun, but are pretty annoying to implement (besides +SE, but that's the least fun of the three). I may or may not implement these in the future if I find the time to.
1. Spinner mod (+SP)
1. Sliders mod (+SL)
1. Sliderends mod (+SE)

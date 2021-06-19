<img src="assets/logo.png" alt="soyokaze!" width="300" height="300">

# soyokaze!

[![Version](https://img.shields.io/github/v/release/goodtrailer/soyokaze.svg?color=green&style=flat-square)](https://github.com/goodtrailer/soyokaze/releases/latest)
[![CodeFactor](https://www.codefactor.io/repository/github/goodtrailer/soyokaze/badge/main?style=flat-square)](https://www.codefactor.io/repository/github/goodtrailer/soyokaze/overview/main)
[![License](https://img.shields.io/github/license/goodtrailer/soyokaze.svg?color=blue&style=flat-square)](https://github.com/goodtrailer/soyokaze/blob/master/LICENSE)
[![Downloads](https://img.shields.io/github/downloads/goodtrailer/soyokaze/total.svg?color=orange&style=flat-square)](https://somsubhra.github.io/github-release-stats/?username=goodtrailer&repository=soyokaze&page=1&per_page=0)

An osu! ruleset mimicking Genshin Impact's Ballads of Breeze mini-game. You can also check it out over at [rūrusetto](https://rulesets.info/posts/soyokaze/), a website created by the [Rūrusetto team](https://github.com/Rurusetto) to catelog osu!lazer rulesets.

## Video Showcase (YouTube)
[![Preview Image](assets/preview.png)](https://www.youtube.com/watch?v=hWjG0W7EiAE)

## Fun Maps for soyokaze!
* [Ren feat. Hatsune Miku - Tougetsu, Rinzen ni Kisu. [Touch Your Dreams]](https://osu.ppy.sh/b/2923009) 4.74⭐: Clean jump map for beginners

* [Ito Kanako - Skyclad no Kansokusha [Time Travel]](https://osu.ppy.sh/b/907850) 6.07⭐: A good generic map to get started with, plus some streams
* [Team Nekokan - Can't Defeat Airman [Holy Shit! It's Airman!!]](https://osu.ppy.sh/b/104229) 5.78⭐: The classic AR10 jump map, only now with a focus on reading
* [Rita - Tonitrus [Beyond the World]](https://osu.ppy.sh/b/1935726) 5.71⭐: An alt-map on osu!standard turned single-tap, yet still highly enjoyable
* [Idun Nicoline - Lost Without You (Boxplot Remix) [Wistful]](https://osu.ppy.sh/b/2725039) 6.08⭐: Heavy and stamina-intensive burst map
* [Niji no Conquistador - Zutto Summer de Koishiteru [Summer Love]](https://osu.ppy.sh/b/2625911) 5.89⭐: Overwhelming jump map with difficult triples
* [DUSTCELL - Anemone [Irrational]](https://osu.ppy.sh/b/2593243) 5.29⭐: Finger control is insane, see if you can resist cheesing and achieve an A rank
* [Camellia - fastest crash (Camellia's "paroxysmal" Energetic Hitech Remix) [Terminal Velocity]](https://osu.ppy.sh/b/2730082) 6.87⭐: Fun low BPM tech bursts
* [II-L - SPUTNIK-3 [Beyond OWC]](https://osu.ppy.sh/b/2719326) 7.10⭐: Rhythmic hell, except now even harder to read than ever before
* [Frederic - oddloop [oldloop]](https://osu.ppy.sh/b/1137879) 4.82⭐: Slap on +EZ and then thank the heavens above that soyokaze! has no notelock 
* [xi - Freedom Dive[FOUR DIMENSIONS]](https://osu.ppy.sh/b/129891) 9.48⭐: Maybe acc-able if you use two fingers to tap one key, possibly keyboard viable

## Skinning
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

## To-Do
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

## Extras
These are mods that I think would be really fun, but are pretty annoying to implement (besides +SE, but that's the least fun of the three). I may or may not implement these in the future if I find the time to.
1. Spinner mod (+SP)
1. Sliders mod (+SL)
1. Sliderends mod (+SE)

This one is sorta doable, but no one will actually be able to use it without a custom build of osu!, because the legacy beatmap decoder gets angry at any non-legacy (unofficial) rulesets. I tried finding a solution, but nobody else seemed to be able to work around it. Tau and lazer-swing were the only functional rulesets that I found that implemented custom editors, but neither of them worked without a custom osu! build.
1. Editor support

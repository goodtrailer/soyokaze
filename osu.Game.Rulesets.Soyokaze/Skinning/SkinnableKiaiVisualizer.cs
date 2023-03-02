﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Graphics.Containers;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.Skinning.Defaults;
using osu.Game.Skinning;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Soyokaze.Skinning
{
    public partial class SkinnableKiaiVisualizer : BeatSyncedContainer
    {
        private Bindable<bool> showBindable = new Bindable<bool>();

        private Color4 firstFlashColour;
        private Color4 flashColour;

        private byte firstFlashOpacity;
        private byte flashOpacity;

        private float defaultSpin;
        private float kiaiSpin;

        private KiaiSquaresComposite composite = new KiaiSquaresComposite()
        {
            RelativeSizeAxes = Axes.Both,
            Origin = Anchor.Centre,
            Anchor = Anchor.Centre,
        };

        private bool isFirstFlash = true;
        private int slowKiaiSpinBeatIndex = int.MaxValue;

        [BackgroundDependencyLoader(true)]
        private void load(ISkinSource skin, SoyokazeConfigManager cm)
        {
            AddInternal(composite);

            Color4 defaultColor = skin.GetConfig<SoyokazeSkinColour, Color4>(SoyokazeSkinColour.KiaiVisualizerDefault)?.Value ?? Color4.DarkSlateGray;
            byte defaultOpacity = skin.GetConfig<SoyokazeSkinConfiguration, byte>(SoyokazeSkinConfiguration.KiaiVisualizerDefaultOpacity)?.Value ?? 128;
            composite.Colour = defaultColor.Opacity(defaultOpacity);

            firstFlashColour = skin.GetConfig<SoyokazeSkinColour, Color4>(SoyokazeSkinColour.KiaiVisualizerFirstFlash)?.Value ?? Color4.White;
            firstFlashOpacity = skin.GetConfig<SoyokazeSkinConfiguration, byte>(SoyokazeSkinConfiguration.KiaiVisualizerFirstFlashOpacity)?.Value ?? 255;

            flashColour = skin.GetConfig<SoyokazeSkinColour, Color4>(SoyokazeSkinColour.KiaiVisualizerFlash)?.Value ?? Color4.White;
            flashOpacity = skin.GetConfig<SoyokazeSkinConfiguration, byte>(SoyokazeSkinConfiguration.KiaiVisualizerFlashOpacity)?.Value ?? 152;

            defaultSpin = skin.GetConfig<SoyokazeSkinConfiguration, float>(SoyokazeSkinConfiguration.KiaiVisualizerDefaultSpin)?.Value ?? 1.5f;
            kiaiSpin = skin.GetConfig<SoyokazeSkinConfiguration, float>(SoyokazeSkinConfiguration.KiaiVisualizerKiaiSpin)?.Value ?? -60f;

            cm?.BindWith(SoyokazeConfig.ShowKiaiVisualizer, showBindable);
            showBindable.BindValueChanged(valueChanged => Alpha = valueChanged.NewValue ? 1f : 0f, true);
        }

        protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, ChannelAmplitudes amplitudes)
        {
            int beatsPerMeasure = timingPoint.TimeSignature.Numerator;
            double beatLength = timingPoint.BeatLength;

            if (effectPoint.KiaiMode)
            {
                int fastKiaiSpinBeatLength = Math.Min(beatsPerMeasure * 3 / 4, 1);

                if (isFirstFlash)
                {
                    composite.FlashColour(firstFlashColour.Opacity(firstFlashOpacity), beatLength * 2, Easing.In);
                    isFirstFlash = false;

                    composite.Spin(beatsPerMeasure * beatLength, 0.65f * beatsPerMeasure * kiaiSpin, Easing.Out);
                    slowKiaiSpinBeatIndex = beatIndex + fastKiaiSpinBeatLength;
                }
                else if (beatIndex % beatsPerMeasure == 0)
                {
                    composite.FlashColour(flashColour.Opacity(flashOpacity), beatLength * 2, Easing.In);

                    composite.Spin(beatsPerMeasure * beatLength, 0.65f * beatsPerMeasure * kiaiSpin, Easing.Out);
                    slowKiaiSpinBeatIndex = beatIndex + fastKiaiSpinBeatLength;
                }
                else if (beatIndex >= slowKiaiSpinBeatIndex)
                {
                    composite.Spin(beatsPerMeasure * beatLength, beatsPerMeasure * kiaiSpin);
                }
            }
            else
            {
                composite.Spin(beatLength, defaultSpin);
                isFirstFlash = true;
            }
        }

        private partial class KiaiSquaresComposite : CompositeDrawable
        {
            private SkinnableDrawable[] kiaiSquares = new SkinnableDrawable[2];
            private Bindable<int> screenCenterDistanceBindable = new Bindable<int>();
            private int screenCenterDistance => screenCenterDistanceBindable.Value;

            public KiaiSquaresComposite()
            {
                for (int i = 0; i < 2; i++)
                {
                    kiaiSquares[i] = new SkinnableDrawable(
                        new SoyokazeSkinComponentLookup(SoyokazeSkinComponents.KiaiVisualizerSquare),
                        _ => new DefaultKiaiVisualizerSquare()
                    )
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    };
                }
            }

            [BackgroundDependencyLoader]
            private void load(SoyokazeConfigManager cm)
            {
                AddRangeInternal(kiaiSquares);

                cm.BindWith(SoyokazeConfig.ScreenCenterGap, screenCenterDistanceBindable);
                screenCenterDistanceBindable.BindValueChanged(_ => updatePositions(), true);
            }

            private void updatePositions()
            {
                kiaiSquares[0].Position = new Vector2(-screenCenterDistance, 0);
                kiaiSquares[1].Position = new Vector2(screenCenterDistance, 0);
            }

            public void Pulse(double duration, float scale)
            {
                foreach (SkinnableDrawable kiaiSquare in kiaiSquares)
                {
                    kiaiSquare.ScaleTo(scale, duration * 0.15f, Easing.InQuint).Then().ScaleTo(1f, duration * 0.85f, Easing.OutQuint);
                }
            }

            public void Spin(double duration, float angle, Easing easing = Easing.None)
            {
                kiaiSquares[0].RotateTo(kiaiSquares[0].Rotation + angle, duration, easing);
                kiaiSquares[1].RotateTo(kiaiSquares[1].Rotation - angle, duration, easing);
            }
        }
    }
}

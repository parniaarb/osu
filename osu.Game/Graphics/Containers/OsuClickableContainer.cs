﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Localisation;
using osu.Game.Graphics.UserInterface;
using osuTK;

namespace osu.Game.Graphics.Containers
{
    public partial class OsuClickableContainer : ClickableContainer, IHasTooltip
    {
        private readonly HoverSampleSet sampleSet;

        private readonly Container content = new Container { RelativeSizeAxes = Axes.Both };

        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => Content.ReceivePositionalInputAt(screenSpacePos);

        protected override Container<Drawable> Content => content;

        protected virtual HoverSounds CreateHoverSounds(HoverSampleSet sampleSet) => new HoverClickSounds(sampleSet) { Enabled = { BindTarget = Enabled } };

        public OsuClickableContainer(HoverSampleSet sampleSet = HoverSampleSet.Default)
        {
            this.sampleSet = sampleSet;
        }

        public virtual LocalisableString TooltipText { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            if (AutoSizeAxes != Axes.None)
            {
                content.RelativeSizeAxes = (Axes.Both & ~AutoSizeAxes);
                content.AutoSizeAxes = AutoSizeAxes;
            }

            AddInternal(content);
            Add(CreateHoverSounds(sampleSet));
        }
    }
}

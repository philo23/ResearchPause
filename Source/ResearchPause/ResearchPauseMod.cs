using UnityEngine;
using Verse;

namespace ResearchPause
{
    public class ResearchPauseMod : Mod
    {
        private readonly ResearchPauseSettings settings;

        public ResearchPauseMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<ResearchPauseSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled(
                "Automatically pause when a pawn finishes researching",
                ref settings.pauseOnResearchFinished
            );
            if (!string.IsNullOrEmpty(Patches.QueueModLoaded))
            {
                listingStandard.CheckboxLabeled(
                    "Only pause on no queued research",
                    ref settings.onlyOnNoQueue
                );
            }

            if (Patches.QueueModLoaded == "ResearchPal")
            {
                listingStandard.Label(
                    "Pausing when the research-window is shown can be activated in ResearchPal Forked mod-options.");
            }
            else
            {
                listingStandard.CheckboxLabeled(
                    "Automatically pause while Research window is open",
                    ref settings.pauseOnResearchWindowOpen
                );
            }

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Research Pause";
        }
    }
}
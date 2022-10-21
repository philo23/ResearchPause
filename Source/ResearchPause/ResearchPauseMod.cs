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
            listingStandard.CheckboxLabeled(
                "Automatically pause while Research window is open",
                ref settings.pauseOnResearchWindowOpen
            );
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Research Pause";
        }
    }
}

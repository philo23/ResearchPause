using RimWorld;
using Verse;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace ResearchPause
{
    public class ResearchPauseMod : Mod
    {
        ResearchPauseSettings settings;

        public ResearchPauseMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<ResearchPauseSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
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

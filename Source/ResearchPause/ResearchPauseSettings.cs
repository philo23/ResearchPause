using Verse;

namespace ResearchPause
{
    public class ResearchPauseSettings : ModSettings
    {
        public bool pauseOnResearchFinished = true;
        public bool pauseOnResearchWindowOpen = true;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref pauseOnResearchFinished, "researchFinished", true);
            Scribe_Values.Look(ref pauseOnResearchWindowOpen, "researchWindowOpen", true);

            base.ExposeData();
        }
    }
}

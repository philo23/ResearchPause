using Verse;

namespace ResearchPause
{
    public class TickManager_Helper
    {
        public static void PauseIfUnpaused()
        {
            if (!Find.TickManager.Paused)
            {
                Find.TickManager.TogglePaused();
            }
        }

        public static void UnpausedIfPaused()
        {
            if (Find.TickManager.Paused)
            {
                Find.TickManager.TogglePaused();
            }
        }
    }
}

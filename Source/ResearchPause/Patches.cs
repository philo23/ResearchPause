using RimWorld;
using Verse;
using System.Reflection;
using HarmonyLib;

namespace ResearchPause
{
    [StaticConstructorOnStartup]
    public static class Patches
    {
        static Patches()
        {
            var harmony = new Harmony("shrimpee.researchpause");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(ResearchManager), "FinishProject")]
    class Patch_ResearchManager_FinishProject
    {
        static void Prefix(bool doCompletionDialog)
        {
            bool pauseOnResearchFinished = LoadedModManager
                .GetMod<ResearchPauseMod>()
                .GetSettings<ResearchPauseSettings>()
                .pauseOnResearchFinished;

            if (Current.ProgramState == ProgramState.Playing && pauseOnResearchFinished)
            {
                TickManager_Helper.PauseIfUnpaused();
            }
        }
    }

    [HarmonyPatch(typeof(Window), nameof(Window.PreOpen))]
    class Patch_Window_PreOpen
    {
        static void Prefix(Window __instance)
        {
            bool pauseOnResearchWindowOpen = LoadedModManager
                .GetMod<ResearchPauseMod>()
                .GetSettings<ResearchPauseSettings>()
                .pauseOnResearchWindowOpen;

            if (__instance is MainTabWindow_Research && pauseOnResearchWindowOpen)
            {
                TickManager_Helper.PauseIfUnpaused();
            }
        }
    }

    [HarmonyPatch(typeof(Window), nameof(Window.PreClose))]
    class Patch_Window_PreClose
    {
        static void Prefix(Window __instance)
        {
            bool pauseOnResearchWindowOpen = LoadedModManager
                .GetMod<ResearchPauseMod>()
                .GetSettings<ResearchPauseSettings>()
                .pauseOnResearchWindowOpen;

            if (__instance is MainTabWindow_Research && pauseOnResearchWindowOpen)
            {
                TickManager_Helper.UnpausedIfPaused();
            }
        }
    }
}

using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

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
    internal class Patch_ResearchManager_FinishProject
    {
        private static void Prefix(bool doCompletionDialog)
        {
            if (!LoadedModManager
                .GetMod<ResearchPauseMod>()
                .GetSettings<ResearchPauseSettings>()
                .pauseOnResearchFinished)
            {
                return;
            }

            if (Current.ProgramState == ProgramState.Playing)
            {
                TickManager_Helper.PauseIfUnpaused();
            }
        }
    }

    [HarmonyPatch(typeof(Window), nameof(Window.PreOpen))]
    internal class Patch_Window_PreOpen
    {
        private static void Prefix(Window __instance)
        {
            if (!LoadedModManager
                .GetMod<ResearchPauseMod>()
                .GetSettings<ResearchPauseSettings>()
                .pauseOnResearchWindowOpen)
            {
                return;
            }

            if (__instance is MainTabWindow_Research)
            {
                TickManager_Helper.PauseIfUnpaused();
            }
        }
    }

    [HarmonyPatch(typeof(Window), nameof(Window.PreClose))]
    internal class Patch_Window_PreClose
    {
        private static void Prefix(Window __instance)
        {
            if (!LoadedModManager
                .GetMod<ResearchPauseMod>()
                .GetSettings<ResearchPauseSettings>()
                .pauseOnResearchWindowOpen)
            {
                return;
            }

            if (__instance is MainTabWindow_Research)
            {
                TickManager_Helper.UnpausedIfPaused();
            }
        }
    }
}

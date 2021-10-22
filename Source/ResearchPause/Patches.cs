using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ResearchPause
{
    [StaticConstructorOnStartup]
    public static class Patches
    {
        public static readonly string QueueModLoaded;

        static Patches()
        {
            var harmony = new Harmony("shrimpee.researchpause");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            // Checks if ResearchPal Forked is loaded
            if (ModLister.GetActiveModWithIdentifier("VinaLx.ResearchPalForked") != null)
            {
                QueueModLoaded = "ResearchPal";
                var queueTraverse = Traverse.CreateWithType("ResearchPal.Queue");
                if (!queueTraverse.Methods().Contains("TryStartNext"))
                {
                    Log.Warning(
                        "[ResearchPause]: Could not find method in ResearchPal Forked to patch. Will not be able to use the queue-setting.");
                    QueueModLoaded = null;
                    return;
                }

                var tryStartNextMethodInfo =
                    AccessTools.Method(AccessTools.TypeByName("ResearchPal.Queue"), "TryStartNext");
                harmony.Patch(tryStartNextMethodInfo,
                    postfix: new HarmonyMethod(typeof(Queue_TryStartNext), "Postfix"));
                Log.Message(
                    "[ResearchPause]: Patched ResearchPal Forked to be able to use the queue setting");
                return;
            }

            // Checks if Fluffys Research Tree is loaded
            if (ModLister.GetActiveModWithIdentifier("fluffy.researchtree") != null)
            {
                QueueModLoaded = "ReseachTree";
                var queueTraverse = Traverse.CreateWithType("FluffyResearchTree.Queue");
                if (!queueTraverse.Methods().Contains("TryStartNext"))
                {
                    Log.Warning(
                        "[ResearchPause]: Could not find method in Research Tree to patch. Will not be able to use the queue-setting.");
                    QueueModLoaded = null;
                    return;
                }

                var tryStartNextMethodInfo =
                    AccessTools.Method(AccessTools.TypeByName("FluffyResearchTree.Queue"), "TryStartNext");
                harmony.Patch(tryStartNextMethodInfo,
                    postfix: new HarmonyMethod(typeof(Queue_TryStartNext), "Postfix"));
                Log.Message(
                    "[ResearchPause]: Patched Research Tree to be able to use the queue setting");
            }
        }
    }

    [HarmonyPatch(typeof(ResearchManager), "FinishProject")]
    internal class Patch_ResearchManager_FinishProject
    {
        private static void Prefix(bool doCompletionDialog)
        {
            if (string.IsNullOrEmpty(Patches.QueueModLoaded) && LoadedModManager
                .GetMod<ResearchPauseMod>()
                .GetSettings<ResearchPauseSettings>()
                .onlyOnNoQueue)
            {
                return;
            }

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
            if (Patches.QueueModLoaded == "ResearchPal")
            {
                return;
            }

            if (!LoadedModManager
                .GetMod<ResearchPauseMod>()
                .GetSettings<ResearchPauseSettings>()
                .pauseOnResearchWindowOpen)
            {
                return;
            }

            if (__instance is MainTabWindow_Research || __instance.GetType().Name == "MainTabWindow_ResearchTree")
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
            if (Patches.QueueModLoaded == "ResearchPal")
            {
                return;
            }

            if (!LoadedModManager
                .GetMod<ResearchPauseMod>()
                .GetSettings<ResearchPauseSettings>()
                .pauseOnResearchWindowOpen)
            {
                return;
            }

            if (__instance is MainTabWindow_Research || __instance.GetType().Name == "MainTabWindow_ResearchTree")
            {
                TickManager_Helper.UnpausedIfPaused();
            }
        }
    }

    public class Queue_TryStartNext
    {
        // Postfix that runs after ResearchTree Forked/Fluffys Research Trees function TryStartNext
        // If the ResearchManager has no active project after its run there was nothing in the queue and we can pause if needed
        public static void Postfix()
        {
            if (!LoadedModManager
                .GetMod<ResearchPauseMod>()
                .GetSettings<ResearchPauseSettings>()
                .onlyOnNoQueue)
            {
                return;
            }

            if (!LoadedModManager
                .GetMod<ResearchPauseMod>()
                .GetSettings<ResearchPauseSettings>()
                .pauseOnResearchFinished)
            {
                return;
            }

            if (Find.ResearchManager.currentProj == null)
            {
                TickManager_Helper.PauseIfUnpaused();
            }
        }
    }
}
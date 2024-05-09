using Verse;
using HarmonyLib;

namespace StevesWalls
{
    [StaticConstructorOnStartup]
    public static class StevesWallsMain
    {
        static StevesWallsMain()
        {
            SWLog.Message("[1.5 Update | Older versions will no longer be maintained.]");

            var harmony = new Harmony("com.steveswalls");
            harmony.PatchAll();
        }
    }
}

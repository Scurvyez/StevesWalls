using Verse;
using HarmonyLib;

namespace StevesWalls
{
    [StaticConstructorOnStartup]
    public static class StevesWallsMain
    {
        static StevesWallsMain()
        {
            Log.Message("[<color=#4494E3FF>Steve's Walls</color>] 03/15/2024 " + "<color=#ff8c66>[1.5 Update | Older versions will no longer be maintained.]</color>");

            var harmony = new Harmony("com.steveswalls");
            harmony.PatchAll();
        }
    }
}

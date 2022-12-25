using Verse;
using HarmonyLib;

namespace StevesWalls
{
    [StaticConstructorOnStartup]
    public static class StevesWallsMain
    {
        static StevesWallsMain()
        {
            Log.Message("<color=white>[</color>" + "<color=#4494E3FF>Steve</color>" + "<color=white>]</color>" +
                "<color=white>[</color>" + "<color=#4494E3FF>Steve's Walls</color>" + "<color=white>]</color>" +
                "<color=#4494E3FF>Everything is glowing! Quick, party in the lights!</color>");

            var harmony = new Harmony("com.steveswalls");
            harmony.PatchAll();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;
using HarmonyLib;

namespace StevesWalls
{
    public static class StevesWallsMain
    {
        static StevesWallsMain()
        {
            Log.Message("<color=#4494E3FF>Hope you like glowing stuff!</color>");

            var harmony = new Harmony("com.steveswalls");
            harmony.PatchAll();
        }
    }
}

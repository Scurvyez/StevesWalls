using HarmonyLib;
using RimWorld;
using Verse;
using System.Reflection;

namespace StevesWalls
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            Harmony harmony = new Harmony(id: "rimworld.scurvyez.steveswalls");

            harmony.Patch(original: AccessTools.Method(typeof(CompPowerTrader), "UpdateOverlays"),
                postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(UpdateOverlaysPostfix)));
        }
        
        public static void UpdateOverlaysPostfix(CompPowerTrader __instance)
        {
            if (__instance.parent.def.HasModExtension<GlassWallsExtension>() 
                && __instance.parent.def.GetModExtension<GlassWallsExtension>().canTogglePowerOverlay)
            {
                FieldInfo overlayPowerOffField = AccessTools.Field(typeof(CompPowerTrader), "overlayPowerOff");
                OverlayHandle? overlayPowerOff = overlayPowerOffField.GetValue(__instance) as OverlayHandle?;
                
                if (overlayPowerOff.HasValue)
                {
                    if (!StevesWallsSettings.ShowPowerOffIcon)
                    {
                        __instance.parent.Map.overlayDrawer.Disable(__instance.parent, ref overlayPowerOff);
                    }
                }
            }
        }
    }
}

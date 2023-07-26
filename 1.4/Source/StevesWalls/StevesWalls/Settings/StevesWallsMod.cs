using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace StevesWalls
{
    public class StevesWallsMod : Mod
    {
        StevesWallsSettings settings;
        public static StevesWallsMod mod;

        public StevesWallsMod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<StevesWallsSettings>();

        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            Listing_Standard list = new ();
            Rect viewRect = new (inRect.x, inRect.y, inRect.width, inRect.height);
            list.Begin(viewRect);

            list.Label("<color=#4494E3FF>Alert Walls</color>");
            list.Gap(3.00f);

            float sliderValue = settings._alertPulseIntensity;
            string sliderValueText = sliderValue.ToString("F2"); // Convert the slider value to a string with two decimal places
            list.Label(label: "SW_AlertPulseIntensity".Translate(sliderValueText), tooltip: "SW_AlertPulseIntensityDesc".Translate());
            settings._alertPulseIntensity = list.Slider(settings._alertPulseIntensity, 0f, 1f);

            list.End();
        }

        public override string SettingsCategory()
        {
            return "SW_ModName".Translate();
        }
    }
}

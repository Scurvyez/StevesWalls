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

            //list.CheckboxLabeled("SW_SyncPulseAlert".Translate(), ref settings._syncAlertPulse, "SW_SyncPulseAlertDesc".Translate());

            float pulseIntensitySlider = settings._alertPulseIntensity;
            string pulseIntensitySliderText = pulseIntensitySlider.ToString("F2"); // Convert the slider value to a string with two decimal places
            list.Label(label: "SW_AlertPulseIntensity".Translate(pulseIntensitySliderText), tooltip: "SW_AlertPulseIntensityDesc".Translate());
            settings._alertPulseIntensity = list.Slider(settings._alertPulseIntensity, 0f, 1f);

            float pulseInteravlSlider = settings._alertPulseInterval;
            string pulseInteravlSliderText = pulseInteravlSlider.ToString("F0");
            list.Label(label: "SW_AlertPulseInterval".Translate(pulseInteravlSliderText), tooltip: "SW_AlertPulseIntervalDesc".Translate());
            settings._alertPulseInterval = (int)list.Slider(settings._alertPulseInterval, 60, 720);

            list.End();
        }

        public override string SettingsCategory()
        {
            return "SW_ModName".Translate();
        }
    }
}

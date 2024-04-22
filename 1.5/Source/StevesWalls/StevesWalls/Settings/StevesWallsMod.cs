using UnityEngine;
using Verse;

namespace StevesWalls
{
    public class StevesWallsMod : Mod
    {
        StevesWallsSettings settings;
        public static StevesWallsMod mod;

        // new variables to hold temporary color values
        private Color tempColorManhunter;
        private Color tempColorAncientsFaction;
        private Color tempColorMechFaction;
        private Color tempColorInsectFaction;
        private Color tempColorEntitiesFaction;
        private Color tempColorPiratesFaction;

        // track state for each color wheel
        private bool manhunterDragging = false;
        private bool ancientsFactionDragging = false;
        private bool mechFactionDragging = false;
        private bool insectFactionDragging = false;
        private bool entitiesFactionDragging = false;
        private bool piratesFactionDragging = false;
        
        public StevesWallsMod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<StevesWallsSettings>();

            // initialize temporary color values with settings values
            tempColorManhunter = settings._alertColorManhunter;
            tempColorAncientsFaction = settings._alertColorAncientsFaction;
            tempColorMechFaction = settings._alertColorMechFaction;
            tempColorInsectFaction = settings._alertColorInsectFaction;
            tempColorEntitiesFaction = settings._alertColorEntitiesFaction;
            tempColorPiratesFaction = settings._alertColorPiratesFaction;
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            Listing_Standard list = new();
            Rect viewRect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
            list.Begin(viewRect);

            list.Label("<color=#4494E3FF>Alert Walls</color>");
            list.Gap(3.00f);

            float pulseIntensitySlider = settings._alertPulseIntensity;
            string pulseIntensitySliderText = pulseIntensitySlider.ToString("F2");
            list.Label(label: "SW_AlertPulseIntensity".Translate(pulseIntensitySliderText), tooltip: "SW_AlertPulseIntensityDesc".Translate());
            settings._alertPulseIntensity = list.Slider(settings._alertPulseIntensity, 0f, 1f);

            float pulseIntervalSlider = settings._alertPulseInterval;
            string pulseIntervalSliderText = pulseIntervalSlider.ToString("F0");
            list.Label(label: "SW_AlertPulseInterval".Translate(pulseIntervalSliderText), tooltip: "SW_AlertPulseIntervalDesc".Translate());
            settings._alertPulseInterval = (int)list.Slider(settings._alertPulseInterval, 60, 720);

            list.CheckboxLabeled("SW_UseThreatSpecificAlertColors".Translate(), ref settings._useThreatSpecificAlertColors, "SW_UseThreatSpecificAlertColorsDesc".Translate());

            // all threat-specific color settings
            // 3 options per row
            if (settings._useThreatSpecificAlertColors)
            {
                Listing_Standard list2 = new();
                list2.Begin(viewRect);
                list2.Gap(100f);

                float initialVertOffset = 100f;
                float rowTwoVertOffset = 100f;
                float columnOffset = viewRect.xMax / 2.5f;

                // column 1
                Rect manhunterRect = new Rect(viewRect.x, viewRect.y + initialVertOffset, 1f, 1f);
                DrawSettingWithTexture(manhunterRect, "Manhunters", ref tempColorManhunter, ref manhunterDragging);

                Rect insectRect = new Rect(viewRect.x, viewRect.y + initialVertOffset + rowTwoVertOffset, 1f, 1f);
                DrawSettingWithTexture(insectRect, "Insects", ref tempColorInsectFaction, ref insectFactionDragging);

                // column 2
                Rect ancientsRect = new Rect(viewRect.x + columnOffset, viewRect.y + initialVertOffset, 1f, 1f);
                DrawSettingWithTexture(ancientsRect, "Ancients", ref tempColorAncientsFaction, ref ancientsFactionDragging);

                Rect entitiesRect = new Rect(viewRect.x + columnOffset, viewRect.y + initialVertOffset + rowTwoVertOffset, 1f, 1f);
                DrawSettingWithTexture(entitiesRect, "Entities", ref tempColorEntitiesFaction, ref entitiesFactionDragging);

                // column 3
                Rect mechRect = new Rect(viewRect.x + (columnOffset * 2), viewRect.y + initialVertOffset, 1f, 1f);
                DrawSettingWithTexture(mechRect, "Mechanoids", ref tempColorMechFaction, ref mechFactionDragging);

                Rect piratesRect = new Rect(viewRect.x + (columnOffset * 2), viewRect.y + initialVertOffset + rowTwoVertOffset, 1f, 1f);
                DrawSettingWithTexture(piratesRect, "Pirates", ref tempColorPiratesFaction, ref piratesFactionDragging);

                list2.End();
            }
            else
            {
                // If not using threat-specific colors, revert to settings colors
                tempColorManhunter = settings._alertColorManhunter;
                tempColorAncientsFaction = settings._alertColorAncientsFaction;
                tempColorMechFaction = settings._alertColorMechFaction;
                tempColorInsectFaction = settings._alertColorInsectFaction;
                tempColorEntitiesFaction = settings._alertColorEntitiesFaction;
                tempColorPiratesFaction = settings._alertColorPiratesFaction;
            }

            list.End();
        }

        private static void DrawSettingWithTexture(Rect refRect, string label, ref Color value, ref bool currentlyDragging)
        {
            float textureSize = 70f; // size of the color wheel and box textures
            //Rect labelRect = list.GetRect(textureSize); // get the rect for the label
            float padding = 5f; // padding between the texture and the label

            Widgets.Label(new Rect(refRect.x, refRect.y - 25f, 200f, 20f), label);
            Rect textureRect = new Rect(refRect.x, refRect.y, textureSize, textureSize);
            Widgets.DrawBoxSolid(textureRect, value);
            refRect.x += textureSize + padding;
            refRect.width -= textureSize + padding;
            Rect colorWheelRect = new Rect(refRect.x, refRect.y, textureSize, textureSize);
            Widgets.HSVColorWheel(colorWheelRect, ref value, ref currentlyDragging);
        }

        public override void WriteSettings()
        {
            // update the settings values with the temporary color values
            settings._alertColorManhunter = tempColorManhunter;
            settings._alertColorAncientsFaction = tempColorAncientsFaction;
            settings._alertColorMechFaction = tempColorMechFaction;
            settings._alertColorInsectFaction = tempColorInsectFaction;
            settings._alertColorEntitiesFaction = tempColorEntitiesFaction;
            settings._alertColorPiratesFaction = tempColorPiratesFaction;

            // call the base WriteSettings method to save the settings
            base.WriteSettings();
        }

        public override string SettingsCategory()
        {
            return "SW_ModName".Translate();
        }
    }
}

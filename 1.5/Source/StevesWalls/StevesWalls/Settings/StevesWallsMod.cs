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
        private Color tempColorTribalsFaction;
        private Color tempColorMentalBreakAggro;

        // track state for each color wheel
        private bool manhunterDragging = false;
        private bool ancientsFactionDragging = false;
        private bool mechFactionDragging = false;
        private bool insectFactionDragging = false;
        private bool entitiesFactionDragging = false;
        private bool piratesFactionDragging = false;
        private bool tribalsFactionDragging = false;
        private bool mentalBreakAggroDragging = false;

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
            tempColorTribalsFaction = settings._alertColorTribalsFaction;
            tempColorMentalBreakAggro = settings._alertColorMentalBreakAggro;
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
            settings._alertPulseIntensity = list.Slider(settings._alertPulseIntensity, 0f, 2f);

            float pulseIntervalSlider = settings._alertPulseInterval;
            string pulseIntervalSliderText = pulseIntervalSlider.ToString("F0");
            list.Label(label: "SW_AlertPulseInterval".Translate(pulseIntervalSliderText), tooltip: "SW_AlertPulseIntervalDesc".Translate());
            settings._alertPulseInterval = (int)list.Slider(settings._alertPulseInterval, 60, 720);

            list.CheckboxLabeled("SW_ShowPowerOffIcon".Translate(), ref settings._showPowerOffIcon, "SW_ShowPowerOffIconDesc".Translate());

            list.CheckboxLabeled("SW_UseThreatSpecificAlertColors".Translate(), ref settings._useThreatSpecificAlertColors, "SW_UseThreatSpecificAlertColorsDesc".Translate());
            
            // all threat-specific color settings
            // 3 options per row
            if (settings._useThreatSpecificAlertColors)
            {
                Listing_Standard list2 = new();
                list2.Begin(viewRect);
                list2.Gap(100f);

                float initialVertOffset = 120f;
                float initialHorzOffset = 40f;
                float columnOffset = viewRect.xMax / 4f;

                // column 1
                // manhunters
                // insects
                Rect manhunterRect = new Rect(viewRect.x + initialHorzOffset, viewRect.y + initialVertOffset, 1f, 1f);
                DrawSettingWithTextures(manhunterRect, "Manhunters", ref tempColorManhunter, ref manhunterDragging);

                Rect insectRect = new Rect(viewRect.x + initialHorzOffset, viewRect.y + initialVertOffset + initialVertOffset, 1f, 1f);
                DrawSettingWithTextures(insectRect, "Insects", ref tempColorInsectFaction, ref insectFactionDragging);

                // column 2
                // ancients
                // entities (anomaly)
                Rect ancientsRect = new Rect(viewRect.x + initialHorzOffset + columnOffset, viewRect.y + initialVertOffset, 1f, 1f);
                DrawSettingWithTextures(ancientsRect, "Ancients", ref tempColorAncientsFaction, ref ancientsFactionDragging);

                Rect entitiesRect = new Rect(viewRect.x + initialHorzOffset + columnOffset, viewRect.y + initialVertOffset + initialVertOffset, 1f, 1f);
                DrawSettingWithTextures(entitiesRect, "Entities", ref tempColorEntitiesFaction, ref entitiesFactionDragging);

                // column 3
                // mechanoids
                //pirates
                Rect mechRect = new Rect(viewRect.x + initialHorzOffset + (columnOffset * 2f), viewRect.y + initialVertOffset, 1f, 1f);
                DrawSettingWithTextures(mechRect, "Mechanoids", ref tempColorMechFaction, ref mechFactionDragging);

                Rect piratesRect = new Rect(viewRect.x + initialHorzOffset + (columnOffset * 2f), viewRect.y + initialVertOffset + initialVertOffset, 1f, 1f);
                DrawSettingWithTextures(piratesRect, "Pirates", ref tempColorPiratesFaction, ref piratesFactionDragging);

                // column 4
                // tribals
                // mental breaks aggro
                Rect tribalsRect = new Rect(viewRect.x + initialHorzOffset + (columnOffset * 3f), viewRect.y + initialVertOffset, 1f, 1f);
                DrawSettingWithTextures(tribalsRect, "Tribals", ref tempColorTribalsFaction, ref tribalsFactionDragging);

                Rect mBAggroRect = new Rect(viewRect.x + initialHorzOffset + (columnOffset * 3f), viewRect.y + initialVertOffset + initialVertOffset, 1f, 1f);
                DrawSettingWithTextures(mBAggroRect, "Mental Breaks (Aggro)", ref tempColorMentalBreakAggro, ref mentalBreakAggroDragging);

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
                tempColorTribalsFaction = settings._alertColorTribalsFaction;
                tempColorMentalBreakAggro = settings._alertColorMentalBreakAggro;
            }

            list.End();
        }

        private static void DrawSettingWithTextures(Rect refRect, string label, ref Color value, ref bool currentlyDragging)
        {
            float colorWheelScale = 70f;
            float pulseTexScale = 70f;
            float wallTexScale = 35f;
            float padding = 5f; // padding between the textures and the label

            Widgets.DrawLineHorizontal(refRect.x, refRect.y, colorWheelScale + pulseTexScale); // text underscore
            Widgets.Label(new Rect(refRect.x, refRect.y - 25f, 200f, 25f), label); // our text
            Rect pulseTexRect = new Rect(refRect.x - (wallTexScale / 2), (refRect.y + padding) - (wallTexScale / 2), pulseTexScale + wallTexScale, pulseTexScale + wallTexScale);
            Rect wallTexRect = new Rect(refRect.x + (wallTexScale / 2f), refRect.y + (wallTexScale / 2f), wallTexScale, wallTexScale);

            GUI.DrawTexture(wallTexRect, Assets.WallSectionTex);
            GUI.DrawTexture(pulseTexRect, Assets.PulseEffectTex, ScaleMode.ScaleToFit, true, 1f, value, 0f, 0f);

            refRect.x += pulseTexScale + padding;
            refRect.width -= pulseTexScale + padding;
            Rect colorWheelRect = new Rect(refRect.x, refRect.y + padding, colorWheelScale, colorWheelScale);
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
            settings._alertColorTribalsFaction = tempColorTribalsFaction;
            settings._alertColorMentalBreakAggro = tempColorMentalBreakAggro;

            // call the base WriteSettings method to save the settings
            base.WriteSettings();
        }

        public override string SettingsCategory()
        {
            return "SW_ModName".Translate();
        }
    }
}

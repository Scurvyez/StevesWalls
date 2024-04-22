using UnityEngine;
using Verse;

namespace StevesWalls
{
    public class StevesWallsSettings : ModSettings
    {
        private static StevesWallsSettings _instance;

        public static float AlertPulseIntensity
        {
            get
            {
                return _instance._alertPulseIntensity;
            }
        }

        public static int AlertPulseInterval
        {
            get
            {
                return _instance._alertPulseInterval;
            }
        }

        public static bool UseThreatSpecificAlertColors
        {
            get
            {
                return _instance._useThreatSpecificAlertColors;
            }
        }

        public static Color AlertColorManhunter
        {
            get
            {
                return _instance._alertColorManhunter;
            }
        }

        public static Color AlertColorAncientsFaction
        {
            get
            {
                return _instance._alertColorAncientsFaction;
            }
        }

        public static Color AlertColorMechFaction
        {
            get
            {
                return _instance._alertColorMechFaction;
            }
        }

        public static Color AlertColorInsectFaction
        {
            get
            {
                return _instance._alertColorInsectFaction;
            }
        }

        public static Color AlertColorEntitiesFaction
        {
            get
            {
                return _instance._alertColorEntitiesFaction;
            }
        }

        public static Color AlertColorPiratesFaction
        {
            get
            {
                return _instance._alertColorPiratesFaction;
            }
        }

        public static Color AlertColorTribalsFaction
        {
            get
            {
                return _instance._alertColorTribalsFaction;
            }
        }

        public int _alertPulseInterval = 120;
        public bool _useThreatSpecificAlertColors = false;
        public float _alertPulseIntensity = 0.5f;
        public Color _alertColorManhunter = Color.white;
        public Color _alertColorAncientsFaction = Color.white;
        public Color _alertColorMechFaction = Color.white;
        public Color _alertColorInsectFaction = Color.white;
        public Color _alertColorEntitiesFaction = Color.white;
        public Color _alertColorPiratesFaction = Color.white;
        public Color _alertColorTribalsFaction = Color.white;

        public StevesWallsSettings()
        {
            _instance = this;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref _alertPulseIntensity, "alertPulseIntensity", 0.5f);
            Scribe_Values.Look(ref _alertPulseInterval, "alertPulseInterval", 120);
            Scribe_Values.Look(ref _useThreatSpecificAlertColors, "useThreatSpecificAlertColors", false);
            Scribe_Values.Look(ref _alertColorManhunter, "alertColorManhunter", Color.white);
            Scribe_Values.Look(ref _alertColorAncientsFaction, "alertColorAncientsFaction", Color.white);
            Scribe_Values.Look(ref _alertColorMechFaction, "alertColorMechFaction", Color.white);
            Scribe_Values.Look(ref _alertColorInsectFaction, "alertColorInsectFaction", Color.white);
            Scribe_Values.Look(ref _alertColorEntitiesFaction, "alertColorEntitiesFaction", Color.white);
            Scribe_Values.Look(ref _alertColorPiratesFaction, "alertColorPiratesFaction", Color.white);
            Scribe_Values.Look(ref _alertColorTribalsFaction, "alertColorTribalsFaction", Color.white);
        }
    }
}

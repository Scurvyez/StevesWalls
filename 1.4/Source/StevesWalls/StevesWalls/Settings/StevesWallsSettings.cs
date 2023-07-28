using Verse;

namespace StevesWalls
{
    public class StevesWallsSettings : ModSettings
    {
        private static StevesWallsSettings _instance;

        public static bool SyncAlertPulse
        {
            get
            {
                return _instance._syncAlertPulse;
            }
        }

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

        public float _alertPulseIntensity = 0.6f;
        public int _alertPulseInterval = 120;
        public bool _syncAlertPulse = true;

        public StevesWallsSettings()
        {
            _instance = this;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref _alertPulseIntensity, "alertPulseIntensity", 0.6f);
            Scribe_Values.Look(ref _alertPulseInterval, "alertPulseInterval", 120);
            Scribe_Values.Look(ref _syncAlertPulse, "syncAlertPulse", true);
        }
    }
}

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

        public float _alertPulseIntensity = 0.6f;

        public StevesWallsSettings()
        {
            _instance = this;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref _alertPulseIntensity, "alertPulseIntensity", 0.6f);
        }
    }
}

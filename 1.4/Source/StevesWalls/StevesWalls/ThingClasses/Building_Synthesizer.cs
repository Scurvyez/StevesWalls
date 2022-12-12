using RimWorld;
using Verse;

namespace StevesWalls
{
    public class Building_Synthesizer : Building_WorkTable_HeatPush
    {
        public bool usedThisTick = false;

        public override void UsedThisTick()
        {
            base.UsedThisTick();
            usedThisTick = true;
        }
    }
}

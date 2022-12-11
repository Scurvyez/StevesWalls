using System.Collections.Generic;
using Verse;

namespace StevesWalls
{
    public class CompProperties_BuildingGraphics : CompProperties
    {
        public List<GraphicData> graphicLayersOff = null;
        public List<GraphicData> graphicLayersOn = null;
        public List<GraphicData> graphicLayersOnAndInUse = null;

        public CompProperties_BuildingGraphics()
        {
            compClass = typeof(Comp_BuildingGraphics);
        }

        public override IEnumerable<string> ConfigErrors(ThingDef parentDef)
        {
            foreach (string error in base.ConfigErrors(parentDef))
            {
                yield return error;
            }
            if (graphicLayersOff == null || graphicLayersOn == null)
            {
                yield return "[Steves Walls]Oops! We couldn't find any supplemental graphics, please provide at least one.";
            }
        }
    }
}

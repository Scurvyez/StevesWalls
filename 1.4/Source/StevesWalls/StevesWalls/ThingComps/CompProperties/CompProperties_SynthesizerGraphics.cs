using System.Collections.Generic;
using Verse;

namespace StevesWalls
{
    public class CompProperties_SynthesizerGraphics : CompProperties
    {
        public List<GraphicData> graphicLayersOff = null;
        public List<GraphicData> graphicLayersOn = null;
        public List<GraphicData> graphicLayersOnAndInUse = null;

        public CompProperties_SynthesizerGraphics()
        {
            compClass = typeof(Comp_SynthesizerGraphics);
        }

        public override IEnumerable<string> ConfigErrors(ThingDef parentDef)
        {
            foreach (string error in base.ConfigErrors(parentDef))
            {
                yield return error;
            }

            if (graphicLayersOff == null)
            {
                yield return "<color=#4494E3FF>[Steves Walls]</color> Oops! We couldn't find any graphics for a synthesizer when turned off, please provide at least one.";
            }

            if (graphicLayersOn == null)
            {
                yield return "<color=#4494E3FF>[Steves Walls]</color> Oops! We couldn't find any graphics for a synthesizer when turned on, please provide at least one.";
            }

            if (graphicLayersOnAndInUse == null)
            {
                yield return "<color=#4494E3FF>[Steves Walls]</color> Oops! We couldn't find any graphics for a synthesizer when turned on and in use, please provide at least one.";
            }
        }
    }
}

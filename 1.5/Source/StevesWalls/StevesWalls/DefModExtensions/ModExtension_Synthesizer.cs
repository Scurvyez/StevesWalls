using System.Collections.Generic;
using Verse;

namespace StevesWalls
{
    public class ModExtension_BuildingGraphics : DefModExtension
    {
        public List<GraphicData> graphicsOff = null;
        public List<GraphicData> graphicsOn = null;

        public List<GraphicData> graphicsOffAndEmpty = null;
        public List<GraphicData> graphicsOffAndFull = null;
        public List<GraphicData> graphicsOnAndWorking = null;
    }
}

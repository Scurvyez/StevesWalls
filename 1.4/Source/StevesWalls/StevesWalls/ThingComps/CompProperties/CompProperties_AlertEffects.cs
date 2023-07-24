using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace StevesWalls
{
    public class CompProperties_AlertEffects : CompProperties
    {
        public FleckDef fleckDefRef = null;

        public CompProperties_AlertEffects() => compClass = typeof(Comp_AlertEffects);
    }
}

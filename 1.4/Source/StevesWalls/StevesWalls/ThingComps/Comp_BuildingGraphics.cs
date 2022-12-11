using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;
using UnityEngine;

namespace StevesWalls
{
    public class Comp_BuildingGraphics : ThingComp
    {
        public CompProperties_BuildingGraphics Props => (CompProperties_BuildingGraphics)props;
        private readonly Building_Printer Printer;

        /// <summary>
        /// Renders additional graphics on a parent thing.
        /// XML, drawerType = RealtimeOnly || MapMeshAndRealTime.
        /// </summary>
        public override void PostDraw()
        {
            base.PostDraw();
            CompFlickable compFlickable = parent.GetComp<CompFlickable>();
            CompPowerTrader compPower = parent.GetComp<CompPowerTrader>();

            if (parent.def != null && compFlickable != null && compPower != null)
            {
                // if not powered
                if (!compPower.PowerOn)
                {
                    for (int i = 0; i < Props.graphicLayersOff.Count; i++)
                    {
                        Props.graphicLayersOff[i].Graphic.Draw(parent.DrawPos, parent.Rotation, parent);
                    }
                }
                // if powered
                else
                {
                    // if switched off
                    if (!compFlickable.SwitchIsOn)
                    {
                        for (int i = 0; i < Props.graphicLayersOff.Count; i++)
                        {
                            Props.graphicLayersOff[i].Graphic.Draw(parent.DrawPos, parent.Rotation, parent);
                        }
                    }
                    // if switched on
                    else
                    {
                        // if not in use by a pawn
                        if (!Printer.usedThisTick)
                        {
                            for (int i = 0; i < Props.graphicLayersOn.Count; i++)
                            {
                                Props.graphicLayersOn[i].Graphic.Draw(parent.DrawPos, parent.Rotation, parent);
                            }
                        }
                        // if in use by a pawn
                        else
                        {
                            for (int i = 0; i < Props.graphicLayersOnAndInUse.Count; i++)
                            {
                                Props.graphicLayersOnAndInUse[i].Graphic.Draw(parent.DrawPos, parent.Rotation, parent);
                            }
                        }
                    }
                }
            }
            parent.DefaultGraphic.Draw(parent.DrawPos, parent.Rotation, parent);
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
        }
    }
}

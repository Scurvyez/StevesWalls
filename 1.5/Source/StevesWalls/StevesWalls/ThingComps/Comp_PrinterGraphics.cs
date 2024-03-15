using RimWorld;
using Verse;
using UnityEngine;

namespace StevesWalls
{
    public class Comp_PrinterGraphics : ThingComp
    {
        public CompProperties_PrinterGraphics Props => (CompProperties_PrinterGraphics)props;

        public CompPowerTrader _powerComp;
        public CompFlickable _flickableComp;
        
        public CompPowerTrader PowerComp
        {
            get
            {
                if (_powerComp == null)
                {
                    _powerComp = parent.GetComp<CompPowerTrader>();
                }
                return _powerComp;
            }
        }

        public CompFlickable FlickableComp
        {
            get
            {
                if (_flickableComp == null)
                {
                    _flickableComp = parent.GetComp<CompFlickable>();
                }
                return _flickableComp;
            }
        }

        /// <summary>
        /// Renders additional graphics on a parent thing.
        /// XML, drawerType = RealtimeOnly || MapMeshAndRealTime.
        /// </summary>
        public override void PostDraw()
        {
            base.PostDraw();
            Color color1 = new Color(0.145f, 0.588f, 0.745f, 1f); // for debugging text

            // if not powered
            if (!PowerComp.PowerOn)
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
                if (!FlickableComp.SwitchIsOn)
                {
                    for (int i = 0; i < Props.graphicLayersOff.Count; i++)
                    {
                        Props.graphicLayersOff[i].Graphic.Draw(parent.DrawPos, parent.Rotation, parent);
                    }
                }

                // if switched on
                else
                {
                    // if the building is a printer
                    if (parent is Building_Printer printer)
                    {
                        // if not in use by a pawn
                        if (!printer.usedThisTick)
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

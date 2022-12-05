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
    public class Building_Synthesizer : Building_WorkTable_HeatPush
    {
        public ModExtension_Synthesizer Extension_Synthesizer;

        public override void PostMake()
        {
            base.PostMake();
            Extension_Synthesizer = def.GetModExtension<ModExtension_Synthesizer>();
        }

        public override void Draw()
        {
            CompFlickable compFlickable = GetComp<CompFlickable>();
            CompPowerTrader compPower = GetComp<CompPowerTrader>();

            if (Extension_Synthesizer != null && def != null)
            {
                if (!compPower.PowerOn)
                {
                    for (int i = 0; i < Extension_Synthesizer.graphicsOff.Count; i++)
                    {
                        Extension_Synthesizer.graphicsOff[i].Graphic.Draw(DrawPos, Rotation, this, 0f);
                    }
                }

                else
                {
                    if (!compFlickable.SwitchIsOn)
                    {
                        for (int i = 0; i < Extension_Synthesizer.graphicsOff.Count; i++)
                        {
                            Extension_Synthesizer.graphicsOff[i].Graphic.Draw(DrawPos, Rotation, this, 0f);
                        }
                    }

                    else
                    {
                        for (int i = 0; i < Extension_Synthesizer.graphicsOn.Count; i++)
                        {
                            Extension_Synthesizer.graphicsOn[i].Graphic.Draw(DrawPos, Rotation, this, 0f);
                        }
                    }
                }
            }
        }
    }
}

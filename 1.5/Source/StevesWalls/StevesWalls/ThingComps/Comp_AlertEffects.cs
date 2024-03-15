using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;
using System;

namespace StevesWalls
{
    public class Comp_AlertEffects : ThingComp
    {
        public CompProperties_AlertEffects Props => (CompProperties_AlertEffects)props;
        private CompGlower glowerComp;
        private Color glowerColor;
        private Effecter alertEffect;
        private int ticksUntilNextAlert = 0;
        private int alertIntervalTicks = StevesWallsSettings.AlertPulseInterval;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            glowerComp = parent.GetComp<CompGlower>();
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref ticksUntilNextAlert, "ticksUntilNextAlert", 0);
        }

        public override void CompTick()
        {
            base.CompTick();

            if (glowerComp != null && glowerComp.Glows)
            {
                glowerColor = glowerComp.GlowColor.ToColor;

                // Decrement the ticksUntilNextAlert by one on each tick
                if (ticksUntilNextAlert > 0)
                {
                    ticksUntilNextAlert--;
                }

                // Check if enough ticks have passed to trigger the alertEffect
                if (ticksUntilNextAlert <= 0)
                {
                    if (ShouldPulse())
                    {
                        Emit();
                        ticksUntilNextAlert = alertIntervalTicks;
                    }
                }
            }
        }

        private bool ShouldPulse()
        {
            Map map = parent.Map;

            if (map != null && map.attackTargetsCache != null)
            {
                HashSet<IAttackTarget> targets = map.attackTargetsCache.TargetsHostileToColony;

                foreach (IAttackTarget target in targets)
                {
                    if (target is Pawn pawn && !pawn.Fogged())
                    {
                        if (pawn.health != null && !pawn.health.Dead && !pawn.Downed)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void Emit()
        {
            if (parent.def.defName.IndexOf("AlertGlitterGlassWall", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                alertEffect = SW_DefOf.SW_DefaultWallAlert.Spawn();
                alertEffect.children[0].def.color = glowerColor;
                alertEffect.children[0].def.color.a = StevesWallsSettings.AlertPulseIntensity;
                alertEffect.Trigger(parent, parent, -1);
            }
            StopEmitting();
        }

        public void StopEmitting()
        {
            alertEffect.Cleanup();
            alertEffect = null;
        }
    }
}

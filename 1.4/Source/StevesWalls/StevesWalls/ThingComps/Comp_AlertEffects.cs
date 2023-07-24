using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace StevesWalls
{
    public class Comp_AlertEffects : ThingComp
    {
        public CompProperties_AlertEffects Props => (CompProperties_AlertEffects)props;
        private CompGlower glowerComp;
        private Color exGraphicColor;
        private Effecter alertEffect;
        private FleckDef alertFleckDef;
        private int ticksUntilNextAlert = 0;
        private const int AlertIntervalTicks = 180; // Adjust this value as needed

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            glowerComp = parent.GetComp<CompGlower>();
            alertFleckDef = Props.fleckDefRef;
        }

        public override void CompTick()
        {
            base.CompTick();

            if (glowerComp != null && alertFleckDef != null)
            {
                exGraphicColor = glowerComp.GlowColor.ToColor;
                alertFleckDef.graphicData.color = exGraphicColor;
                //alertFleckDef.graphicData.colorTwo = exGraphicColor;

                Log.Message($"glow color: {exGraphicColor}");
                Log.Message($"alert color: {alertFleckDef.graphicData.color}");

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
                        ticksUntilNextAlert = AlertIntervalTicks;
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
                    if (target is Pawn pawn)
                    {
                        if (pawn.health != null && !pawn.health.Dead && !pawn.Downed)
                        {
                            return true;
                        }
                    }
                }
            }

            // No enemies found, return false
            return false;
        }

        private void Emit()
        {
            alertEffect = SW_DefOf.SW_WallAlert.Spawn();
            alertEffect.Trigger(parent, parent);

            DespawnEffect();
        }

        public void DespawnEffect()
        {
            alertEffect.Cleanup();
            alertEffect = null;
        }
    }
}

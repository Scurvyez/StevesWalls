using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;
using System;
using RimWorld;
using System.Linq;

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
        private HashSet<IAttackTarget> targets = new();
        private List<Color> colors = new();

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
            targets = parent.Map.attackTargetsCache.TargetsHostileToColony;

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
                colors.Clear();

                if (!StevesWallsSettings.UseThreatSpecificAlertColors)
                {
                    alertEffect.children[0].def.color = glowerColor;
                }
                else
                {
                    foreach (IAttackTarget target in targets)
                    {
                        if (target is Pawn pawn && pawn != null)
                        {
                            Log.Message($"Target: {pawn.def.defName}, faction: {pawn.Faction.def}");

                            if (pawn.MentalStateDef == MentalStateDefOf.Manhunter
                                || pawn.MentalStateDef == MentalStateDefOf.ManhunterBloodRain
                                || pawn.MentalStateDef == MentalStateDefOf.ManhunterPermanent)
                            {
                                Color manHunterCol = new();
                                manHunterCol = StevesWallsSettings.AlertColorManhunter;
                                colors.Add(manHunterCol);
                            }
                            if (pawn.Faction.def == FactionDefOf.AncientsHostile)
                            {
                                Color humanlikeCol = new();
                                humanlikeCol = StevesWallsSettings.AlertColorAncientsFaction;
                                colors.Add(humanlikeCol);
                            }
                            if (pawn.Faction.def == FactionDefOf.Mechanoid)
                            {
                                Color mechCol = new();
                                mechCol = StevesWallsSettings.AlertColorMechFaction;
                                colors.Add(mechCol);
                            }
                            if (pawn.Faction.def == FactionDefOf.Insect)
                            {
                                Color insectCol = new();
                                insectCol = StevesWallsSettings.AlertColorInsectFaction;
                                colors.Add(insectCol);
                            }
                            if (pawn.Faction.def == FactionDefOf.Entities)
                            {
                                Color entitiesCol = new();
                                entitiesCol = StevesWallsSettings.AlertColorEntitiesFaction;
                                colors.Add(entitiesCol);
                            }
                            if (pawn.Faction.def == FactionDefOf.Pirate
                                || pawn.Faction.def == FactionDefOf.PirateWaster)
                            {
                                Color piratesCol = new();
                                piratesCol = StevesWallsSettings.AlertColorPiratesFaction;
                                colors.Add(piratesCol);
                            }
                        }
                    }
                }
                
                if (colors.Count > 0)
                {
                    // Calculate the average color component values
                    float avgR = colors.Average(c => c.r);
                    float avgG = colors.Average(c => c.g);
                    float avgB = colors.Average(c => c.b);
                    float avgA = colors.Average(c => c.a);

                    // Create a new color using the average component values
                    Color averageColor = new Color(avgR, avgG, avgB, avgA);

                    alertEffect.children[0].def.color = averageColor;
                }

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

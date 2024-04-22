using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;
using System;
using RimWorld;

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
                    if (target is Pawn pawn && !pawn.Fogged() && pawn.Map != null)
                    {
                        if (!pawn.health.Dead && !pawn.Downed)
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
                        if (target is Pawn pawn && !pawn.Fogged())
                        {
                            if (pawn.Faction == null)
                            {
                                if (pawn.MentalStateDef == MentalStateDefOf.Manhunter
                                || pawn.MentalStateDef == MentalStateDefOf.ManhunterBloodRain
                                || pawn.MentalStateDef == MentalStateDefOf.ManhunterPermanent)
                                {
                                    Color manHunterCol = new();
                                    manHunterCol = StevesWallsSettings.AlertColorManhunter;
                                    colors.Add(manHunterCol);
                                }
                                continue;
                            }
                            else
                            {
                                if (pawn.Faction.def == FactionDefOf.TribeCivil
                                    || pawn.Faction.def == FactionDefOf.TribeRough)
                                {
                                    Color tribeCol = new();
                                    tribeCol = StevesWallsSettings.AlertColorTribalsFaction;
                                    colors.Add(tribeCol);
                                    continue;
                                }
                                if (pawn.Faction.def == FactionDefOf.Mechanoid)
                                {
                                    Color mechCol = new();
                                    mechCol = StevesWallsSettings.AlertColorMechFaction;
                                    colors.Add(mechCol);
                                    continue;
                                }
                                if (pawn.Faction.def == FactionDefOf.Pirate
                                    || pawn.Faction.def == FactionDefOf.PirateWaster)
                                {
                                    Color piratesCol = new();
                                    piratesCol = StevesWallsSettings.AlertColorPiratesFaction;
                                    colors.Add(piratesCol);
                                    continue;
                                }
                                if (pawn.Faction.def == FactionDefOf.Insect)
                                {
                                    Color insectCol = new();
                                    insectCol = StevesWallsSettings.AlertColorInsectFaction;
                                    colors.Add(insectCol);
                                    continue;
                                }
                                if (pawn.Faction.def == FactionDefOf.Entities)
                                {
                                    Color entitiesCol = new();
                                    entitiesCol = StevesWallsSettings.AlertColorEntitiesFaction;
                                    colors.Add(entitiesCol);
                                    continue;
                                }
                                if (pawn.Faction.def == FactionDefOf.AncientsHostile)
                                {
                                    Color humanlikeCol = new();
                                    humanlikeCol = StevesWallsSettings.AlertColorAncientsFaction;
                                    colors.Add(humanlikeCol);
                                }
                            }
                        }
                    }
                }

                Color blendedColor = ColorBlenderUtil.BlendColors(colors);
                alertEffect.children[0].def.color = blendedColor;

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

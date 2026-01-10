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
        
        private CompGlower _glowerComp;
        private Color _glowerColor;
        private Effecter _alertEffect;
        private int _ticksUntilNextAlert = 0;
        private int _alertIntervalTicks = StevesWallsSettings.AlertPulseInterval;
        private HashSet<IAttackTarget> _targets = new();
        private List<Color> _colors = new();
        
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            _glowerComp = parent.GetComp<CompGlower>();
        }
        
        public override void CompTick()
        {
            base.CompTick();
            _targets = parent.Map.attackTargetsCache.TargetsHostileToColony;

            if (_glowerComp is not { Glows: true }) return;
            _glowerColor = _glowerComp.GlowColor.ToColor;
            
            if (_ticksUntilNextAlert > 0)
            {
                _ticksUntilNextAlert--;
            }
                
            if (_ticksUntilNextAlert <= 0)
            {
                if (!ShouldPulse()) return;
                
                Emit();
                _ticksUntilNextAlert = _alertIntervalTicks;
            }
        }
        
        private bool ShouldPulse()
        {
            Map map = parent.Map;

            if (map is { attackTargetsCache: not null })
            {
                foreach (IAttackTarget target in _targets)
                {
                    if (target is not Pawn pawn || pawn.Fogged() || pawn.Map == null) continue;
                    if (!pawn.health.Dead && !pawn.Downed)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void Emit()
        {
            if (parent.def.defName.IndexOf("AlertGlitterGlassWall", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                _alertEffect = SW_DefOf.SW_DefaultWallAlert.Spawn();
                _colors.Clear();

                if (!StevesWallsSettings.UseThreatSpecificAlertColors)
                {
                    _alertEffect.children[0].def.color = _glowerColor;
                }
                else
                {
                    foreach (IAttackTarget target in _targets)
                    {
                        if (target is not Pawn pawn || pawn.Fogged()) continue;
                        if (pawn.Faction == null)
                        {
                            if (pawn.MentalStateDef == MentalStateDefOf.Manhunter
                                || pawn.MentalStateDef == MentalStateDefOf.ManhunterBloodRain
                                || pawn.MentalStateDef == MentalStateDefOf.ManhunterPermanent)
                            {
                                Color manHunterCol = StevesWallsSettings.AlertColorManhunter;
                                _colors.Add(manHunterCol);
                            }
                            else if (pawn.MentalStateDef.category == MentalStateCategory.Aggro)
                            {
                                Color mBAggroCol = StevesWallsSettings.AlertColorMentalBreakAggro;
                                _colors.Add(mBAggroCol);
                            }
                        }
                        else
                        {
                            if (pawn.Faction.def == FactionDefOf.TribeCivil
                                || pawn.Faction.def == FactionDefOf.TribeRough)
                            {
                                Color tribeCol = StevesWallsSettings.AlertColorTribalsFaction;
                                _colors.Add(tribeCol);
                                continue;
                            }
                            if (pawn.Faction.def == FactionDefOf.Mechanoid)
                            {
                                Color mechCol = StevesWallsSettings.AlertColorMechFaction;
                                _colors.Add(mechCol);
                                continue;
                            }
                            if (pawn.Faction.def == FactionDefOf.Pirate
                                || pawn.Faction.def == FactionDefOf.PirateWaster)
                            {
                                Color piratesCol = StevesWallsSettings.AlertColorPiratesFaction;
                                _colors.Add(piratesCol);
                                continue;
                            }
                            if (pawn.Faction.def == FactionDefOf.Insect)
                            {
                                Color insectCol = StevesWallsSettings.AlertColorInsectFaction;
                                _colors.Add(insectCol);
                                continue;
                            }
                            if (pawn.Faction.def == FactionDefOf.Entities)
                            {
                                Color entitiesCol = StevesWallsSettings.AlertColorEntitiesFaction;
                                _colors.Add(entitiesCol);
                                continue;
                            }
                            if (pawn.Faction.def == FactionDefOf.AncientsHostile)
                            {
                                Color humanlikeCol = StevesWallsSettings.AlertColorAncientsFaction;
                                _colors.Add(humanlikeCol);
                            }
                        }
                    }
                }

                Color blendedColor = ColorBlenderUtil.BlendColors(_colors);
                _alertEffect.children[0].def.color = blendedColor;

                _alertEffect.children[0].def.color.a = StevesWallsSettings.AlertPulseIntensity;
                _alertEffect.Trigger(parent, parent, -1);
            }
            
            StopEmitting();
        }

        private void StopEmitting()
        {
            _alertEffect.Cleanup();
            _alertEffect = null;
        }
        
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref _ticksUntilNextAlert, "ticksUntilNextAlert", 0);
        }
    }
}

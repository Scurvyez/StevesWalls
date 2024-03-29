﻿using RimWorld;
using UnityEngine;
using Verse;

namespace StevesWalls
{
    public abstract class Building_PrinterBase : Building_WorkTable
    {
        protected bool usedLastTick = false;
        protected int animationFrame = 0;
        protected float directionalProgress = 0f;
        protected float armProgress = 0f;

        public override void Tick()
        {
            base.Tick();
            if (usedLastTick)
            {
                UpdateAnimationState();
                usedLastTick = false;
            }
        }

        public override void DynamicDrawPhaseAt(DrawPhase phase, Vector3 drawLoc, bool flip = false)
        {
            base.DynamicDrawPhaseAt(phase, drawLoc, flip);
            DrawArm();
        }

        public override void UsedThisTick()
        {
            base.UsedThisTick();
            usedLastTick = true;
        }

        protected virtual void UpdateAnimationState()
        {
            animationFrame++;
            if (animationFrame > 239)
            {
                animationFrame = 0;
            }

            directionalProgress = AnimationUtil.EaseInOutCubic((animationFrame % 120) / 120f);
            if (animationFrame > 119)
            {
                directionalProgress = 1f - directionalProgress;
            }

            int headAnimationFrame = (animationFrame % 80);
            armProgress = AnimationUtil.EaseInOutCubic((headAnimationFrame % 40) / 40f, -0.5f, 0.5f);
            if (headAnimationFrame > 39)
            {
                armProgress *= -1f;
            }
        }

        protected abstract void DrawArm();
    }
}
using UnityEngine;
using Verse;

namespace StevesWalls
{
    [StaticConstructorOnStartup]
    public static class Assets
    {
        public static readonly Material BlankPowerOffMat = MaterialPool.MatFrom("StevesWalls/Effects/BlankPowerOff", ShaderDatabase.MetaOverlay);
        public static readonly Texture2D PulseEffectTex = ContentFinder<Texture2D>.Get("StevesWalls/Effects/AlertEffect");
        public static readonly Texture2D WallSectionTex = ContentFinder<Texture2D>.Get("StevesWalls/Things/Building/Linked/Icons/SW_GlitterGlassWall_MenuIcon");
    }
}

using RimWorld;
using Verse;

namespace StevesWalls
{
	[DefOf]
	public class SW_DefOf
	{
		//public static ThingDef SW_Printer;
		//public static ThingDef SW_Synthesizer;

		public static FleckDef SW_WallAlertFleck;
        public static EffecterDef SW_WallAlert;
		public static ShaderTypeDef MoteGlowPulse;

		public static ThingDef SW_BlueGlitterGlassWall;
        public static ThingDef SW_GreenGlitterGlassWall;
        public static ThingDef SW_YellowGlitterGlassWall;
        public static ThingDef SW_OrangeGlitterGlassWall;
        public static ThingDef SW_RedGlitterGlassWall;
        public static ThingDef SW_PinkGlitterGlassWall;
        public static ThingDef SW_PurpleGlitterGlassWall;

        static SW_DefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(SW_DefOf));
		}
	}
}

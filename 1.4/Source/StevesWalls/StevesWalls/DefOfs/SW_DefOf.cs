using RimWorld;
using Verse;

namespace StevesWalls
{
	[DefOf]
	public class SW_DefOf
	{
        //public static ThingDef SW_Printer;
        //public static ThingDef SW_Synthesizer;

        public static FleckDef SW_BlueWallAlertFleck;
        public static FleckDef SW_GreenWallAlertFleck;
        public static FleckDef SW_YellowWallAlertFleck;
        public static FleckDef SW_OrangeWallAlertFleck;
        public static FleckDef SW_RedWallAlertFleck;
        public static FleckDef SW_PinkWallAlertFleck;
        public static FleckDef SW_PurpleWallAlertFleck;

        public static EffecterDef SW_DefaultWallAlert;
        public static EffecterDef SW_BlueWallAlert;
        public static EffecterDef SW_GreenWallAlert;
        public static EffecterDef SW_YellowWallAlert;
        public static EffecterDef SW_OrangeWallAlert;
        public static EffecterDef SW_RedWallAlert;
        public static EffecterDef SW_PinkWallAlert;
        public static EffecterDef SW_PurpleWallAlert;

        public static ThingDef SW_BlueAlertGlitterGlassWall;
        public static ThingDef SW_GreenAlertGlitterGlassWall;
        public static ThingDef SW_YellowAlertGlitterGlassWall;
        public static ThingDef SW_OrangeAlertGlitterGlassWall;
        public static ThingDef SW_RedAlertGlitterGlassWall;
        public static ThingDef SW_PinkAlertGlitterGlassWall;
        public static ThingDef SW_PurpleAlertGlitterGlassWall;

        static SW_DefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(SW_DefOf));
		}
	}
}

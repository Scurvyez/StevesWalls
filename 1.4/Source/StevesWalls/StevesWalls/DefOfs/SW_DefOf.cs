﻿using RimWorld;
using Verse;

namespace StevesWalls
{
	[DefOf]
	public class SW_DefOf
	{
		//public static ThingDef SW_Printer;
		//public static ThingDef SW_Synthesizer;

		public static EffecterDef SW_WallAlert;
		//public static ThingDef SW_WallAlertMote;

        static SW_DefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(SW_DefOf));
		}
	}
}

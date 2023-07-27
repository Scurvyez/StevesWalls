using RimWorld;
using Verse;

namespace StevesWalls
{
	[DefOf]
	public class SW_DefOf
	{
        public static EffecterDef SW_DefaultWallAlert;

        static SW_DefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(SW_DefOf));
		}
	}
}

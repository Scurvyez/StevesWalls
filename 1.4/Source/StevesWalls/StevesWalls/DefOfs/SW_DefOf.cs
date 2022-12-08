using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace StevesWalls
{
	[DefOf]
	public class SW_DefOf
	{
		public static ThingDef SW_GlitterGlassPrinter;
		public static ThingDef SW_GlitterGlassSynthesizer;

		static SW_DefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(SW_DefOf));
		}
	}
}

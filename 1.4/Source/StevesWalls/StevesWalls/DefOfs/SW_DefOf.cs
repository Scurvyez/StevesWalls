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
		public static ThingDef SW_GlitterGlass3DPrinter;

		static SW_DefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(SW_DefOf));
		}
	}
}

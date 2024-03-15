using Verse;
using UnityEngine;

namespace StevesWalls
{
	[StaticConstructorOnStartup]
	public static class Assets
	{
		public static readonly Material PrinterArmMaterial = 
			MaterialPool.MatFrom("StevesWalls/Things/Building/Workbenches/Printer/Overlays/SW_PrinterArm", ShaderDatabase.Transparent);
		public static readonly float PrinterArm = 3.0f;

		public static readonly Material PrinterHeadMaterial =
			MaterialPool.MatFrom("StevesWalls/Things/Building/Workbenches/Printer/Overlays/SW_PrinterHead", ShaderDatabase.Transparent);
		public static readonly float PrinterHead = 3.0f;
		
		public static readonly Material PrinterBlankMaterial =
			MaterialPool.MatFrom("StevesWalls/Things/Building/Workbenches/Printer/SW_PrinterBlank", ShaderDatabase.Transparent);
	}
}

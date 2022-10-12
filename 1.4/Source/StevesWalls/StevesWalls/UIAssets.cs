using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;
using UnityEngine;

namespace StevesWalls
{
	[StaticConstructorOnStartup]
	public static class UIAssets
	{
		public static readonly Material PrinterArmMaterial = 
			MaterialPool.MatFrom("Things/Building/Workbenches/Overlays/SW_PrinterArm", ShaderDatabase.Transparent);
		public static readonly float PrinterArm = 3.0f;

		public static readonly Material PrinterHeadMaterial =
			MaterialPool.MatFrom("Things/Building/Workbenches/Overlays/SW_PrinterHead", ShaderDatabase.Transparent);
		public static readonly float PrinterHead = 1.0f;

		public static readonly Material PrinterViewWindowMaterial = 
			MaterialPool.MatFrom("Things/Building/Workbenches/Overlays/SW_PrinterViewingWindow", ShaderDatabase.Transparent);
		public static readonly float PrinterViewWindow = 3.0f;

		public static readonly Material PrinterBlankMaterial =
			MaterialPool.MatFrom("Things/Building/Workbenches/Overlays/SW_PrinterBlank", ShaderDatabase.Transparent);
		public static readonly float PrinterBlank = 3.0f;
	}
}

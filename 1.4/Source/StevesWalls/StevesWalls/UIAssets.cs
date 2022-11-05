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
	public static class Assets
	{
		public static readonly Material PrinterArmMaterial = 
			MaterialPool.MatFrom("Things/Building/Workbenches/Overlays/SW_PrinterArm", ShaderDatabase.Transparent);
		public static readonly float PrinterArm = 3.0f;

		public static readonly Material PrinterHeadMaterial =
			MaterialPool.MatFrom("Things/Building/Workbenches/Overlays/SW_PrinterHead", ShaderDatabase.Transparent);
		public static readonly float PrinterHead = 3.0f;

		public static readonly Material PrinterViewWindowMaterial = 
			MaterialPool.MatFrom("Things/Building/Workbenches/Overlays/SW_PrinterViewingWindow", ShaderDatabase.Transparent);

		public static readonly Material PrinterRoofSectionMaterial =
			MaterialPool.MatFrom("Things/Building/Workbenches/Overlays/SW_PrinterRoofSection", ShaderDatabase.Transparent);

		public static readonly Material PrinterFrontWallSectionMaterial =
			MaterialPool.MatFrom("Things/Building/Workbenches/Overlays/SW_PrinterFrontWallSection", ShaderDatabase.Transparent);

		public static readonly Material PrinterMatMaterial =
			MaterialPool.MatFrom("Things/Building/Workbenches/Overlays/SW_PrinterMat", ShaderDatabase.Transparent);

		public static readonly Material PrinterPowerOnIndicator =
			MaterialPool.MatFrom("Things/Building/Workbenches/Overlays/SW_PrinterPowerOnIndicator", ShaderDatabase.Transparent);

		public static readonly Material PrinterBlankMaterial =
			MaterialPool.MatFrom("Things/Building/Workbenches/Overlays/SW_PrinterBlank", ShaderDatabase.Transparent);
	}
}

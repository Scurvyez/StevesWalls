using UnityEngine;
using Verse;

namespace StevesWalls
{
	[StaticConstructorOnStartup]
	public class Building_Printer : Building_PrinterBase
	{
		public float MaxPercent = 0.0f;
		public bool usedThisTick = false;

        public override void UsedThisTick()
        {
            base.UsedThisTick();
			usedThisTick = true;
        }

        public override void DynamicDrawPhaseAt(DrawPhase phase, Vector3 drawLoc, bool flip = false)
        {
			base.DynamicDrawPhaseAt(phase, drawLoc, flip);

			Vector3 matDrawPos = DrawPos;
			Vector3 matDrawSize = new Vector3(3f, 1f, 3f);
			matDrawPos.y = AltitudeLayer.BuildingOnTop.AltitudeFor(1);
			Material matMat;

			switch (Rotation.AsInt)
			{
				default:
					matMat = Assets.PrinterBlankMaterial;
					break;
				case 1:
					matMat = Assets.PrinterBlankMaterial;
					break;
				case 2:
					matMat = Assets.PrinterBlankMaterial;
					break;
				case 3:
					matMat = Assets.PrinterBlankMaterial;
					break;
			}

			// default printer graphic when off and not full
			Matrix4x4 matMatrix = default;
			matMatrix.SetTRS(matDrawPos, Quaternion.identity, matDrawSize);
			Graphics.DrawMesh(MeshPool.plane10, matMatrix, matMat, 0);
		}

		protected override void DrawArm()
		{
			Vector3 armDrawPos = DrawPos;
			Vector3 headDrawPos = armDrawPos;
			Vector3 armDrawSize = new Vector3(1f, 1f, 1f);
			Vector3 headDrawSize = new Vector3(1f, 1f, 1f);

			armDrawPos.y = AltitudeLayer.BuildingOnTop.AltitudeFor(2);
			headDrawPos.y = AltitudeLayer.BuildingOnTop.AltitudeFor(3);

			Material armMat;
			Material headMat;

			switch (base.Rotation.AsInt)
			{
				default:
				case 0:
					armDrawPos.x += -0.00f + 1.40f * directionalProgress;
					headDrawPos.x += -0.00f + 1.40f * directionalProgress;
					headDrawPos.z += armProgress + 0.49f; // ending float for vertical pathing offset
					armMat = Assets.PrinterArmMaterial;
					armDrawSize = new Vector3(Assets.PrinterArm, 0, Assets.PrinterArm);
					headMat = Assets.PrinterHeadMaterial;
					headDrawSize = new Vector3(Assets.PrinterHead, 0, Assets.PrinterHead);
					break;
				case 1:
					armMat = Assets.PrinterBlankMaterial;
					headMat = Assets.PrinterBlankMaterial;
					break;
				case 2:
					armMat = Assets.PrinterBlankMaterial;
					headMat = Assets.PrinterBlankMaterial;
					break;
				case 3:
					armMat = Assets.PrinterBlankMaterial;
					headMat = Assets.PrinterBlankMaterial;
					break;
			}

			// printer arm
			Matrix4x4 matrix = default;
			matrix.SetTRS(armDrawPos, Quaternion.identity, armDrawSize);
			Graphics.DrawMesh(MeshPool.plane10, matrix, armMat, 0);

			// printer head
			matrix = default;
			matrix.SetTRS(headDrawPos, Quaternion.identity, headDrawSize);
			Graphics.DrawMesh(MeshPool.plane10, matrix, headMat, 0);
		}
    }
}

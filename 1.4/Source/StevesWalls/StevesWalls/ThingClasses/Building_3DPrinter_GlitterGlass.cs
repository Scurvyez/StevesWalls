using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using Verse;
using RimWorld;

namespace StevesWalls
{
	[StaticConstructorOnStartup]
	public class Building_3DPrinter_GlitterGlass : Building_3DPrinter
	{
		public float MaxPercent = 0.0f;

		public override void Draw()
        {
			base.Draw();
			
			Vector3 matDrawPos = DrawPos;
			Vector3 frontWallDrawPos = matDrawPos;
			Vector3 viewWindowDrawPos = matDrawPos;
			Vector3 roofDrawPos = matDrawPos;
			Vector3 powerOnIndicatorDrawPos = matDrawPos;
			Vector3 matDrawSize = new Vector3(4f, 1f, 3f);
			Vector3 frontWallDrawSize = matDrawSize;
			Vector3 viewWindowDrawSize = matDrawSize;
			Vector3 roofDrawSize = matDrawSize;
			Vector3 powerOnIndicatorDrawSize = matDrawSize;

			matDrawPos.y = AltitudeLayer.BuildingOnTop.AltitudeFor(1);
			frontWallDrawPos.y = AltitudeLayer.BuildingOnTop.AltitudeFor(5);
			viewWindowDrawPos.y = AltitudeLayer.BuildingOnTop.AltitudeFor(4);
			roofDrawPos.y = AltitudeLayer.BuildingOnTop.AltitudeFor(10);
			powerOnIndicatorDrawPos.y = AltitudeLayer.BuildingOnTop.AltitudeFor(9);

			Material matMat;
			Material frontWallMat;
			Material viewWindowMat;
			Material roofMat;
			Material powerOnIndicatorMat;

			switch (base.Rotation.AsInt)
			{
				default:
					matMat = Assets.PrinterMatMaterial;
					frontWallMat = Assets.PrinterFrontWallSectionMaterial;
					viewWindowMat = Assets.PrinterViewWindowMaterial;
					roofMat = Assets.PrinterRoofSectionMaterial;
					powerOnIndicatorMat = Assets.PrinterPowerOnIndicator;
					break;
				case 1:
					matMat = Assets.PrinterBlankMaterial;
					frontWallMat = Assets.PrinterBlankMaterial;
					viewWindowMat = Assets.PrinterBlankMaterial;
					roofMat = Assets.PrinterBlankMaterial;
					powerOnIndicatorMat = Assets.PrinterBlankMaterial;
					break;
				case 2:
					matMat = Assets.PrinterBlankMaterial;
					frontWallMat = Assets.PrinterBlankMaterial;
					viewWindowMat = Assets.PrinterBlankMaterial;
					roofMat = Assets.PrinterBlankMaterial;
					powerOnIndicatorMat = Assets.PrinterBlankMaterial;
					break;
				case 3:
					matMat = Assets.PrinterBlankMaterial;
					frontWallMat = Assets.PrinterBlankMaterial;
					viewWindowMat = Assets.PrinterBlankMaterial;
					roofMat = Assets.PrinterBlankMaterial;
					powerOnIndicatorMat = Assets.PrinterBlankMaterial;
					break;
			}

			// printing mat inside machine
			Matrix4x4 matMatrix = default;
			matMatrix.SetTRS(matDrawPos, Quaternion.identity, matDrawSize);
			Graphics.DrawMesh(MeshPool.plane10, matMatrix, matMat, 0);

			// front wall
			Matrix4x4 frontWallMatrix = default;
			frontWallMatrix.SetTRS(frontWallDrawPos, Quaternion.identity, frontWallDrawSize);
			Graphics.DrawMesh(MeshPool.plane10, frontWallMatrix, frontWallMat, 0);

			// view window
			Matrix4x4 viewWindowMatrix = default;
			viewWindowMatrix.SetTRS(viewWindowDrawPos, Quaternion.identity, viewWindowDrawSize);
			Graphics.DrawMesh(MeshPool.plane10, viewWindowMatrix, viewWindowMat, 0);

			// roof
			Matrix4x4 roofMatrix = default;
			roofMatrix.SetTRS(roofDrawPos, Quaternion.identity, roofDrawSize);
			Graphics.DrawMesh(MeshPool.plane10, roofMatrix, roofMat, 0);

			// power on indicator
			Matrix4x4 powerOnIndicatorMatrix = default;
			powerOnIndicatorMatrix.SetTRS(powerOnIndicatorDrawPos, Quaternion.identity, powerOnIndicatorDrawSize);
			Graphics.DrawMesh(MeshPool.plane10, powerOnIndicatorMatrix, powerOnIndicatorMat, 0);
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
					armDrawPos.x += -0.25f + 1.35f * directionalProgress;
					headDrawPos.x += -0.25f + 1.35f * directionalProgress;
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

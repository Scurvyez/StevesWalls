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
	public class Building_3DPrinter_GlitterGlass : Building_3DPrinter
	{
		protected override void DrawArm()
		{
			Vector3 drawPos = DrawPos;
			Vector3 pos = drawPos;
			Vector3 size1 = new Vector3(1f, 1f, 2f); // printer arm
			Vector3 size2 = new Vector3(1f, 1f, 2f); // printer head
			Vector3 size3 = new Vector3(1f, 1f, 2f); // printer view window
			Vector3 one = Vector3.one;
			drawPos.y = AltitudeLayer.BuildingOnTop.AltitudeFor(1f);
			pos.y = AltitudeLayer.BuildingOnTop.AltitudeFor(2f);
			Material material1;
			Material material2;
			Material material3;
			switch (base.Rotation.AsInt)
			{
				default:
					drawPos.x += 0.0f + 1.25f * directionalProgress;
					pos.x += 0.0f + 1.25f * directionalProgress;
					pos.z += armProgress;
					material1 = UIAssets.PrinterArmMaterial;
					size1 = new Vector3(UIAssets.PrinterArm, 0, UIAssets.PrinterArm);
					material2 = UIAssets.PrinterHeadMaterial; // MAKE THIS GRAPHIC
					material3 = UIAssets.PrinterViewWindowMaterial;
					size3 = new Vector3(UIAssets.PrinterArm, 0, UIAssets.PrinterArm);
					break;
				case 1:
					material1 = UIAssets.PrinterBlankMaterial;
					material2 = UIAssets.PrinterBlankMaterial;
					material3 = UIAssets.PrinterBlankMaterial;
					break;
				case 2:
					material1 = UIAssets.PrinterBlankMaterial;
					material2 = UIAssets.PrinterBlankMaterial;
					material3 = UIAssets.PrinterBlankMaterial;
					break;
				case 3:
					material1 = UIAssets.PrinterBlankMaterial;
					material2 = UIAssets.PrinterBlankMaterial;
					material3 = UIAssets.PrinterBlankMaterial;
					break;
			}

			// mat 1, printer arm
			Matrix4x4 matrix = default(Matrix4x4);
			matrix.SetTRS(drawPos, Quaternion.identity, size1);
			Graphics.DrawMesh(MeshPool.plane10, matrix, material1, 0);

			// mat 2, printer head
			matrix = default(Matrix4x4);
			matrix.SetTRS(pos, Quaternion.identity, one);
			Graphics.DrawMesh(MeshPool.plane10, matrix, material2, 0);

			// mat 3, printer view window
			matrix = default(Matrix4x4);
			matrix.SetTRS(new Vector3(1, 1), Quaternion.identity, size3);
			Graphics.DrawMesh(MeshPool.plane10, matrix, material3, 0);
		}
	}
}

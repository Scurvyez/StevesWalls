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
	public class Building_Printer : Building_PrinterBase
	{
		public float MaxPercent = 0.0f;
		public bool usedThisTick = false;
		public ModExtension_BuildingGraphics Extension_BuildingGraphics;

		public override void PostMake()
		{
			base.PostMake();
			Extension_BuildingGraphics = def.GetModExtension<ModExtension_BuildingGraphics>();
		}

        public override void UsedThisTick()
        {
            base.UsedThisTick();
			usedThisTick = true;
        }

        public override void Draw()
        {
			base.Draw();

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

			CompFlickable compFlickable = GetComp<CompFlickable>();
			CompPowerTrader compPower = GetComp<CompPowerTrader>();

			if (Extension_BuildingGraphics != null && def != null)
			{
				// if powered
				if (!compPower.PowerOn)
				{
					for (int i = 0; i < Extension_BuildingGraphics.graphicsOff.Count; i++)
					{
						Extension_BuildingGraphics.graphicsOff[i].Graphic.Draw(DrawPos, Rotation, this, 0f);
					}
				}

				// if unpowered
				else
				{
					// if turned off
					if (!compFlickable.SwitchIsOn)
					{
						for (int i = 0; i < Extension_BuildingGraphics.graphicsOff.Count; i++)
						{
							Extension_BuildingGraphics.graphicsOff[i].Graphic.Draw(DrawPos, Rotation, this, 0f);
						}
					}

					// if turned on
					else if (compFlickable.SwitchIsOn)
					{
						// if being used by a pawn
						if (usedThisTick)
                        {
							for (int i = 0; i < Extension_BuildingGraphics.graphicsOnAndWorking.Count; i++)
							{
								Extension_BuildingGraphics.graphicsOnAndWorking[i].Graphic.Draw(DrawPos, Rotation, this, 0f);
							}
						}

                        else
                        {
							for (int i = 0; i < Extension_BuildingGraphics.graphicsOn.Count; i++)
							{
								Extension_BuildingGraphics.graphicsOn[i].Graphic.Draw(DrawPos, Rotation, this, 0f);
							}
						}
					}
				}
			}
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

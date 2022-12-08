using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace StevesWalls
{
	public class StevesWallsMapComponent : MapComponent
	{
		
		private static void FixPrinters(Map map)
		{
			foreach (Building item in map.listerBuildings.AllBuildingsColonistOfDef(SW_DefOf.SW_GlitterGlassPrinter))
			{
				Building_Printer building_3DPrinter = item as Building_Printer;
				if (building_3DPrinter == null && item.Spawned)
				{
					CopyWorkBench(map, item, SW_DefOf.SW_GlitterGlassPrinter);
				}
			}
		}

		private static void FixSynthesizers(Map map)
        {
			foreach (Building item in map.listerBuildings.AllBuildingsColonistOfDef(SW_DefOf.SW_GlitterGlassSynthesizer))
			{
				Building_Synthesizer building_Synthesizer = item as Building_Synthesizer;
				if (building_Synthesizer == null && item.Spawned)
				{
					CopyWorkBench(map, item, SW_DefOf.SW_GlitterGlassSynthesizer);
				}
			}
		}

		private static Thing CopyWorkBench(Map map, Thing oldBuilding, ThingDef def)
		{
			int hitPoints = oldBuilding.HitPoints;
			IntVec3 position = oldBuilding.Position;
			Building_WorkTable to = (Building_WorkTable)ThingMaker.MakeThing(def);
			to.HitPoints = hitPoints;
			to.Rotation = oldBuilding.Rotation;
			to.SetFaction(Faction.OfPlayer);
			CopyBills((Building_WorkTable)oldBuilding, ref to);
			oldBuilding.Destroy();
			GenSpawn.Spawn(to, position, map);
			return to;
		}

		private static void CopyBills(Building_WorkTable from, ref Building_WorkTable to)
		{
			foreach (Bill bill in from.BillStack.Bills)
			{
				to.BillStack.AddBill(bill.Clone());
			}
		}

		public StevesWallsMapComponent(Map map) : base(map)
		{
			
		}

		public override void FinalizeInit()
		{
			base.FinalizeInit();
			FixPrinters(map);
			FixSynthesizers(map);
		}
	}
}

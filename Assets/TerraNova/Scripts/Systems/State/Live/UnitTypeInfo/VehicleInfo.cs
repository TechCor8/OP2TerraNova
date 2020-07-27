using DotNetMissionSDK;

namespace TerraNova.Systems.State.Live.UnitTypeInfo
{
	/// <summary>
	/// Contains player vehicle type info.
	/// </summary>
	public class VehicleInfo : UnitInfoState
	{
		public int MovePoints						{ get; private set; }
		public int TurnRate							{ get; private set; }
		public int CargoCapacity					{ get; private set; }
		public int ProductionRate					{ get; private set; }
		public int RepairAmount						{ get; private set; }


		public VehicleInfo(map_id unitTypeID, int playerID, int hitPoints, int armor, int oreCost, int rareOreCost, int buildTime, int sightRange,
			int movePoints, int turnRate, int cargoCapacity, int productionRate, int repairAmount)
			: base(unitTypeID, playerID, hitPoints, armor, oreCost, rareOreCost, buildTime, sightRange)
		{
			MovePoints			= movePoints;
			TurnRate			= turnRate;
			CargoCapacity		= cargoCapacity;
			ProductionRate		= productionRate;
			RepairAmount		= repairAmount;
		}
	}
}

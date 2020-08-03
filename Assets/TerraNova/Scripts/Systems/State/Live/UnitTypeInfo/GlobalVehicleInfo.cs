using TerraNova.Systems.Constants;

namespace TerraNova.Systems.State.Live.UnitTypeInfo
{
	/// <summary>
	/// Contains global vehicle type info.
	/// </summary>
	public class GlobalVehicleInfo : GlobalUnitInfo
	{
		public VehicleFlags VehicleFlags			{ get; private set; }
		public bool VehicleFactory					{ get { return (VehicleFlags & VehicleFlags.VehicleFactory) != 0; } }
		public bool ArachnidFactory					{ get { return (VehicleFlags & VehicleFlags.ArachnidFactory) != 0; } }
		public bool WeaponEnabled					{ get { return (VehicleFlags & VehicleFlags.WeaponEnabled) != 0; } }

		public TrackType TrackType					{ get; private set; }


		// Modifiable by research
		public int MovePoints						{ get; private set; }
		public int TurnRate							{ get; private set; }
		public int CargoCapacity					{ get; private set; }
		public int ProductionRate					{ get; private set; }
		public int RepairAmount						{ get; private set; }


		public GlobalVehicleInfo(map_id unitTypeID,
			int researchTopic,
			string unitName,
			string codeName,

			int hitPoints,
			int armor,
			int oreCost,
			int rareOreCost,
			int buildTime,
			int sightRange,

			VehicleFlags vehicleFlags,
			TrackType trackType,
			
			int movePoints,
			int turnRate,
			int cargoCapacity,
			int productionRate,
			int repairAmount
			)
			: base(unitTypeID, researchTopic, unitName, codeName,
				  hitPoints, armor, oreCost, rareOreCost, buildTime, sightRange)
		{
			VehicleFlags	= vehicleFlags;
			TrackType		= trackType;

			MovePoints		= movePoints;
			TurnRate		= turnRate;
			CargoCapacity	= cargoCapacity;
			ProductionRate	= productionRate;
			RepairAmount	= repairAmount;
		}
	}
}

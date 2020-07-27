using DotNetMissionSDK;

namespace TerraNova.Systems.State.Live.UnitTypeInfo
{
	/// <summary>
	/// Contains player structure type info.
	/// </summary>
	public class StructureInfo : UnitInfoState
	{
		public int PowerRequired					{ get; private set; }
		public int WorkersRequired					{ get; private set; }
		public int ScientistsRequired				{ get; private set; }
		public int ProductionRate					{ get; private set; }
		public int StorageCapacity					{ get; private set; }
		public int ProductionCapacity				{ get; private set; }
		public int NumStorageBays					{ get; private set; }

		
		public StructureInfo(map_id unitTypeID, int playerID, int hitPoints, int armor, int oreCost, int rareOreCost, int buildTime, int sightRange,
			int powerRequired, int workersRequired, int scientistsRequired, int productionRate, int storageCapacity, int productionCapacity, int numStorageBays)
			: base(unitTypeID, playerID, hitPoints, armor, oreCost, rareOreCost, buildTime, sightRange)
		{
			PowerRequired		= powerRequired;
			WorkersRequired		= workersRequired;
			ScientistsRequired	= scientistsRequired;
			ProductionRate		= productionRate;
			StorageCapacity		= storageCapacity;
			ProductionCapacity	= productionCapacity;
			NumStorageBays		= numStorageBays;
		}
	}
}

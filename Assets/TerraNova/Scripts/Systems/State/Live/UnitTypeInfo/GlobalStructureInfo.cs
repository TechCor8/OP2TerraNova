using TerraNova.Systems.Constants;
using UnityEngine;

namespace TerraNova.Systems.State.Live.UnitTypeInfo
{
	/// <summary>
	/// Contains global structure type info.
	/// </summary>
	public class GlobalStructureInfo : GlobalUnitInfo
	{
		public string ProduceListName				{ get; private set; }
		public int SizeX							{ get; private set; }
		public int SizeY							{ get; private set; }
		public BuildingFlags BuildingFlags			{ get; private set; }
		public bool Tubes							{ get { return (BuildingFlags & BuildingFlags.Tubes) != 0;					} }
		public bool StructureKit					{ get { return (BuildingFlags & BuildingFlags.StructureKit) != 0;			} }
		public bool DockingAll						{ get { return (BuildingFlags & BuildingFlags.DockingAll) != 0;				} }
		public bool DockingTruck					{ get { return (BuildingFlags & BuildingFlags.DockingTruck) != 0;			} }
		public bool DockingConvec					{ get { return (BuildingFlags & BuildingFlags.DockingConvec) != 0;			} }
		public bool DockingEvac						{ get { return (BuildingFlags & BuildingFlags.DockingEvac) != 0;			} }
		public bool CanBeAutoTargeted				{ get { return (BuildingFlags & BuildingFlags.CanBeAutoTargeted) != 0;		} }
		public int ResourcePriority					{ get; private set; }
		public int RareRubble						{ get; private set; }
		public int Rubble							{ get; private set; }
		public int EdenDockPos						{ get; private set; }
		public int PlymDockPos						{ get; private set; }

		// Modifiable by research
		public int PowerRequired					{ get; private set; }
		public int WorkersRequired					{ get; private set; }
		public int ScientistsRequired				{ get; private set; }
		public int StorageCapacity					{ get; private set; }
		public int ProductionCapacity				{ get; private set; }
		public int NumStorageBays					{ get; private set; }
		
		public int ExplosionSize					{ get; private set; }
		

		public Vector2Int GetSize(bool includeBulldozedArea=false)
		{
			Vector2Int result = new Vector2Int(SizeX, SizeY);

			if (includeBulldozedArea)
			{
				result.x += 2;
				result.y += 2;
			}

			return result;
		}

		/// <summary>
		/// Gets a rect representing the unit's size around a center point.
		/// </summary>
		/// <param name="position">The center point of the unit rect.</param>
		/// <param name="includeBulldozedArea">Whether or not to include the bulldozed area.</param>
		/// <returns>The unit rect.</returns>
		public RectInt GetRect(Vector2Int position, bool includeBulldozedArea=false)
		{
			Vector2Int size = GetSize(includeBulldozedArea);

			return new RectInt(position - (size / 2), size);
		}

		public GlobalStructureInfo(map_id unitTypeID,
			int researchTopic,
			string unitName,
			string codeName,

			int hitPoints,
			int armor,
			int oreCost,
			int rareOreCost,
			int buildTime,
			int sightRange,

			string produceListName,
			int sizeX,
			int sizeY,
			BuildingFlags buildingFlags,
			int resourcePriority,
			int rareRubble,
			int rubble,
			int edenDockPos,
			int plymDockPos,

			int powerRequired,
			int workersRequired,
			int scientistsRequired,
			int storageCapacity,
			int productionCapacity,
			int numStorageBays,
			int explosionSize
			)
			: base(unitTypeID, researchTopic, unitName, codeName,
				  hitPoints, armor, oreCost, rareOreCost, buildTime, sightRange)
		{
			ProduceListName		= produceListName;
			SizeX				= sizeX;
			SizeY				= sizeY;
			BuildingFlags		= buildingFlags;
			ResourcePriority	= resourcePriority;
			RareRubble			= rareRubble;
			Rubble				= rubble;
			EdenDockPos			= edenDockPos;
			PlymDockPos			= plymDockPos;

			PowerRequired		= powerRequired;
			WorkersRequired		= workersRequired;
			ScientistsRequired	= scientistsRequired;
			StorageCapacity		= storageCapacity;
			ProductionCapacity	= productionCapacity;
			NumStorageBays		= numStorageBays;
			ExplosionSize		= explosionSize;
		}
	}
}

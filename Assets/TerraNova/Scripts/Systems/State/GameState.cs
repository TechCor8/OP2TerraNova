using DotNetMissionSDK;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TerraNova.Systems.Constants;
using TerraNova.Systems.State.Live.UnitTypeInfo;

namespace TerraNova.Systems.State
{
	/// <summary>
	/// Contains the current live game state.
	/// </summary>
	public static class GameState
	{
		//private static ReadOnlyDictionary<map_id, GlobalStructureInfo> _StructureInfo;
		//private static ReadOnlyDictionary<map_id, GlobalVehicleInfo> _VehicleInfo;
		//private static ReadOnlyDictionary<map_id, GlobalWeaponInfo> _WeaponInfo;
		//private static ReadOnlyDictionary<map_id, GlobalUnitInfo> _StarshipInfo;
		//private static ReadOnlyCollection<GlobalTechInfo> _TechInfo;

		//private Dictionary<int, UnitState> m_Units = new Dictionary<int, UnitState>();

		//private PlayerState[] m_Players;

		//private static StateSnapshot m_LastSnapshot;

		// Snapshot time (From TethysGame.Time() when snapshot was created)
		//public int time															{ get; private set; }
		
		//public bool usesMorale													{ get; private set; }

		// Global Unit Info
		public static ReadOnlyDictionary<map_id, GlobalStructureInfo> StructureInfo	{ get; private set; }
		public static ReadOnlyDictionary<map_id, GlobalVehicleInfo> VehicleInfo		{ get; private set; }
		public static ReadOnlyDictionary<map_id, GlobalWeaponInfo> WeaponInfo		{ get; private set; }
		public static ReadOnlyDictionary<map_id, GlobalUnitInfo> StarshipInfo		{ get; private set; }
		//public static ReadOnlyCollection<GlobalTechInfo> techInfo					{ get; private set; }

		// Player / Unit State
		//public GaiaState gaia													{ get; private set; }
		//public ReadOnlyCollection<PlayerState> players							{ get; private set; }
		
		// Maps
		//public GaiaMap gaiaMap													{ get; private set; }
		//public GameTileMap tileMap												{ get; private set; }
		//public PlayerUnitMap unitMap											{ get; private set; }
		//public PlayerCommandMap commandMap										{ get; private set; }
		//public PlayerStrengthMap strengthMap									{ get; private set; }


		public static bool Initialize()
		{
			// Read info sheets
			Dictionary<map_id, GlobalStructureInfo> buildingInfo = SheetData.ReadBuildingSheet();
			Dictionary<map_id, GlobalVehicleInfo> vehicleInfo = SheetData.ReadVehicleSheet();
			Dictionary<map_id, GlobalWeaponInfo> weaponInfo = SheetData.ReadWeaponSheet();
			Dictionary<map_id, GlobalUnitInfo> starshipInfo = SheetData.ReadStarshipSheet();

			if (buildingInfo == null)// || minesSheet == null || moraleSheet == null || spaceSheet == null || vehiclesSheet == null || weaponsSheet == null)
			{
				return false;
			}

			StructureInfo = new ReadOnlyDictionary<map_id, GlobalStructureInfo>(buildingInfo);
			VehicleInfo = new ReadOnlyDictionary<map_id, GlobalVehicleInfo>(vehicleInfo);
			WeaponInfo = new ReadOnlyDictionary<map_id, GlobalWeaponInfo>(weaponInfo);
			StarshipInfo = new ReadOnlyDictionary<map_id, GlobalUnitInfo>(starshipInfo);

			return true;
		}

		
	}
}

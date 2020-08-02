using DotNetMissionSDK;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TerraNova.Systems.Constants;
using TerraNova.Systems.State.Live.Population;
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
		public static ReadOnlyDictionary<MineInfoKey, GlobalMineInfo> MineInfo		{ get; private set; }
		//public static ReadOnlyCollection<GlobalTechInfo> techInfo					{ get; private set; }

		public static GlobalMoraleInfo MoraleInfo									{ get; private set; }

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
			Dictionary<map_id, GlobalStructureInfo> buildingInfo = SheetReader.ReadBuildingSheet();
			Dictionary<map_id, GlobalVehicleInfo> vehicleInfo = SheetReader.ReadVehicleSheet();
			Dictionary<map_id, GlobalWeaponInfo> weaponInfo = SheetReader.ReadWeaponSheet();
			Dictionary<map_id, GlobalUnitInfo> starshipInfo = SheetReader.ReadStarshipSheet();
			Dictionary<MineInfoKey, GlobalMineInfo> mineInfo = SheetReader.ReadMineSheet();
			GlobalMoraleInfo moraleInfo = SheetReader.ReadMoraleSheet();

			if (buildingInfo == null || vehicleInfo == null || weaponInfo == null || starshipInfo == null || mineInfo == null || moraleInfo == null)
			{
				return false;
			}

			StructureInfo = new ReadOnlyDictionary<map_id, GlobalStructureInfo>(buildingInfo);
			VehicleInfo = new ReadOnlyDictionary<map_id, GlobalVehicleInfo>(vehicleInfo);
			WeaponInfo = new ReadOnlyDictionary<map_id, GlobalWeaponInfo>(weaponInfo);
			StarshipInfo = new ReadOnlyDictionary<map_id, GlobalUnitInfo>(starshipInfo);
			MineInfo = new ReadOnlyDictionary<MineInfoKey, GlobalMineInfo>(mineInfo);
			MoraleInfo = moraleInfo;

			return true;
		}

		
	}
}

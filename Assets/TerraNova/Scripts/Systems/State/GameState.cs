using DotNetMissionSDK.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TerraNova.Systems.Constants;
using TerraNova.Systems.State.Live;
using TerraNova.Systems.State.Live.Population;
using TerraNova.Systems.State.Live.ResearchInfo;
using TerraNova.Systems.State.Live.UnitTypeInfo;

namespace TerraNova.Systems.State
{
	/// <summary>
	/// Contains the current live game state.
	/// </summary>
	public static class GameState
	{
		private static System.Random _Random;

		//private Dictionary<int, UnitState> m_Units = new Dictionary<int, UnitState>();

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
		public static ReadOnlyDictionary<int, GlobalTechInfo> TechInfo				{ get; private set; }

		public static GlobalMoraleInfo MoraleInfo									{ get; private set; }

		// Player / Unit State
		//public GaiaState gaia													{ get; private set; }
		public static ReadOnlyCollection<Player> Players							{ get; private set; }
		
		// Maps
		//public GaiaMap gaiaMap													{ get; private set; }
		//public GameTileMap tileMap												{ get; private set; }
		//public PlayerUnitMap unitMap											{ get; private set; }
		//public PlayerCommandMap commandMap										{ get; private set; }
		//public PlayerStrengthMap strengthMap									{ get; private set; }

		public static int GetRandom(int min, int max)
		{
			return _Random.Next(min, max);
		}


		public static bool Initialize(MissionRoot mission, int randomSeed, out string error)
		{
			string techTreeName = mission.levelDetails.techTreeName;

			// Read info sheets
			Dictionary<map_id, GlobalStructureInfo> buildingInfo = SheetReader.ReadBuildingSheet();
			Dictionary<map_id, GlobalVehicleInfo> vehicleInfo = SheetReader.ReadVehicleSheet();
			Dictionary<map_id, GlobalWeaponInfo> weaponInfo = SheetReader.ReadWeaponSheet();
			Dictionary<map_id, GlobalUnitInfo> starshipInfo = SheetReader.ReadStarshipSheet();
			Dictionary<MineInfoKey, GlobalMineInfo> mineInfo = SheetReader.ReadMineSheet();
			GlobalMoraleInfo moraleInfo = SheetReader.ReadMoraleSheet();
			Dictionary<int, GlobalTechInfo> techInfo = TechSheetReader.ReadTechSheet(techTreeName);

			if (buildingInfo == null || vehicleInfo == null || weaponInfo == null || starshipInfo == null || mineInfo == null || moraleInfo == null || techInfo == null)
			{
				error = "Failed to load sheets.";
				return false;
			}

			StructureInfo = new ReadOnlyDictionary<map_id, GlobalStructureInfo>(buildingInfo);
			VehicleInfo = new ReadOnlyDictionary<map_id, GlobalVehicleInfo>(vehicleInfo);
			WeaponInfo = new ReadOnlyDictionary<map_id, GlobalWeaponInfo>(weaponInfo);
			StarshipInfo = new ReadOnlyDictionary<map_id, GlobalUnitInfo>(starshipInfo);
			MineInfo = new ReadOnlyDictionary<MineInfoKey, GlobalMineInfo>(mineInfo);
			MoraleInfo = moraleInfo;
			TechInfo = new ReadOnlyDictionary<int, GlobalTechInfo>(techInfo);

			// Initialize core
			_Random = new System.Random(randomSeed);

			// Select mission variant (random)
			int missionVariantIndex = GetRandom(0, mission.missionVariants.Count);

			// Combine master variant with selected variant. The master variant is always used as a base.
			MissionVariant missionVariant = GetCombinedDifficultyVariant(mission, missionVariantIndex);
			//GameData tethysGame = missionVariant.tethysGame;

			bool isMultiplayer = (int)mission.levelDetails.missionType <= -4 && (int)mission.levelDetails.missionType >= -8;

			// Initialize players
			Player[] players = new Player[missionVariant.players.Count];
			for (int i=0; i < missionVariant.players.Count; ++i)
			{
				int playerId = missionVariant.players[i].id;

				// Find the player details for this slot
				SceneParameters.Player playerDetails = SceneParameters.Players.FirstOrDefault((scenePlayer) => scenePlayer.PlayerID == playerId);
				if (playerDetails != null)
				{
					// Create player
					int difficulty = playerDetails.Difficulty;
					players[playerId] = new Player(playerId, difficulty, missionVariant.players[i]);
				}
				else if (!isMultiplayer)
				{
					// For non-multiplayer games: If there aren't any player details for this slot, use the local player difficulty.
					SceneParameters.Player localPlayerDetails = SceneParameters.Players.FirstOrDefault((scenePlayer) => scenePlayer.PlayerID == SceneParameters.LocalPlayerID);
					int difficulty = localPlayerDetails.Difficulty;
					players[playerId] = new Player(playerId, difficulty, missionVariant.players[i]);
				}
			}

			Players = new ReadOnlyCollection<Player>(players);

			error = null;
			return true;
		}

		// Gets the final mission variant to use for the current game
		private static MissionVariant GetCombinedDifficultyVariant(MissionRoot root, int missionVariantIndex)
		{
			// Combine master variant with specified variant. The master variant is always used as a base.
			MissionVariant missionVariant = root.masterVariant;
			if (root.missionVariants.Count > 0)
				missionVariant = MissionVariant.Concat(root.masterVariant, root.missionVariants[missionVariantIndex]);

			// Startup Flags
			bool isMultiplayer = (int)root.levelDetails.missionType <= -4 && (int)root.levelDetails.missionType >= -8;
			int localDifficulty = SceneParameters.Players[SceneParameters.LocalPlayerID].Difficulty;

			// Combine master gaia resources with difficulty gaia resources
			if (!isMultiplayer && localDifficulty < missionVariant.tethysDifficulties.Count)
				missionVariant.tethysGame = GameData.Concat(missionVariant.tethysGame, missionVariant.tethysDifficulties[localDifficulty]);

			foreach (PlayerData data in missionVariant.players)
			{
				int difficulty = 0;
				SceneParameters.Player player = SceneParameters.Players.FirstOrDefault((scenePlayer) => scenePlayer.PlayerID == data.id);

				// Get difficulty
				if (player != null)
				{
					difficulty = player.Difficulty;
				}

				// If playing single player, all players get assigned the local player's difficulty
				if (!isMultiplayer && data.id != SceneParameters.LocalPlayerID)
					difficulty = localDifficulty;

				// Add difficulty resources
				if (difficulty < data.difficulties.Count)
					data.resources = PlayerData.ResourceData.Concat(data.resources, data.difficulties[difficulty]);
			}

			return missionVariant;
		}
	}
}

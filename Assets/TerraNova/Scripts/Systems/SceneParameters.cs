using System.Collections.ObjectModel;
using System.IO;

namespace TerraNova.Systems
{
	/// <summary>
	/// Contains the current scene parameters.
	/// </summary>
	public static class SceneParameters
	{
		public enum MissionEndStatus
		{
			Won,
			Lost,
			Aborted
		}

		public sealed class Player
		{
			public int PlayerID								{ get; private set; }
			public int Difficulty							{ get; private set; }

			public Player(int playerID, int difficulty)
			{
				PlayerID = playerID;
				Difficulty = difficulty;
			}
		}

		// Campaign parameters
		public static string CampaignName					{ get; private set; }
		public static int CampaignMissionIndex				{ get; private set; }

		// Mission parameters
		public static string MissionPath					{ get; private set; }
		public static ReadOnlyCollection<Player> Players	{ get; private set; }
		public static int LocalPlayerID						{ get; private set; }
		public static int RandomSeed						{ get; private set; }
		//public static bool IsMultiplayer					{ get; private set; }

		// Mission End parameters
		public static MissionEndStatus MissionStatus	{ get; private set; }

		
		/// <summary>
		/// Used for starting a campaign mission.
		/// </summary>
		public static void SetCampaignMissionStartParameters(int difficulty, string campaignName, int missionIndex)
		{
			CampaignName = campaignName;
			CampaignMissionIndex = missionIndex;

			MissionPath = Path.Combine("Campaign", Path.Combine(campaignName, "Mission" + missionIndex + ".opm"));
			RandomSeed = UnityEngine.Random.Range(0, int.MaxValue);

			// Create local player
			Player[] players = new Player[1];
			players[0] = new Player(0, difficulty);
			Players = new ReadOnlyCollection<Player>(players);

			LocalPlayerID = 0;
			//IsMultiplayer = false;
		}

		/// <summary>
		/// Used for Colony Games, Tutorials, command line arguments, etc. for starting a one-off mission.
		/// </summary>
		public static void SetMissionStartParameters(string missionPath, int difficulty)
		{
			CampaignName = null;
			CampaignMissionIndex = -1;

			MissionPath = missionPath;
			RandomSeed = UnityEngine.Random.Range(0, int.MaxValue);
			
			// Create local player
			Player[] players = new Player[1];
			players[0] = new Player(0, difficulty);
			Players = new ReadOnlyCollection<Player>(players);

			LocalPlayerID = 0;
			//IsMultiplayer = false;
		}

		/// <summary>
		/// Used for multiplayer and skirmish games.
		/// </summary>
		public static void SetMultiplayerStartParameters(string missionPath, int randomSeed, Player[] players, int localPlayerID)
		{
			CampaignName = null;
			CampaignMissionIndex = -1;

			MissionPath = missionPath;
			RandomSeed = randomSeed;
			Players = new ReadOnlyCollection<Player>(players);
			LocalPlayerID = localPlayerID;
			//IsMultiplayer = true;
		}

		/// <summary>
		/// Used when a mission ends to determine what to do next on the menu scene.
		/// </summary>
		public static void SetMissionEndParameters(MissionEndStatus status)
		{
			MissionStatus = status;
		}
	}
}

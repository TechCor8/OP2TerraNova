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

		// Campaign parameters
		public static string CampaignName				{ get; private set; }
		public static int CampaignMissionIndex			{ get; private set; }

		// Mission parameters
		public static string MissionPath				{ get; private set; }
		public static int Difficulty					{ get; private set; }

		// Mission End parameters
		public static MissionEndStatus MissionStatus	{ get; private set; }

		/// <summary>
		/// Used for starting a campaign mission.
		/// </summary>
		public static void SetCampaignMissionStartParameters(int difficulty, string campaignName, int missionIndex)
		{
			Difficulty = difficulty;
			CampaignName = campaignName;
			CampaignMissionIndex = missionIndex;

			MissionPath = Path.Combine("Campaign", Path.Combine(campaignName, "Mission" + missionIndex + ".opm"));
		}

		/// <summary>
		/// Used for Colony Games, Tutorials, command line arguments, etc. for starting a one-off mission.
		/// </summary>
		public static void SetMissionStartParameters(string missionPath, int difficulty)
		{
			CampaignName = null;
			CampaignMissionIndex = -1;

			MissionPath = missionPath;
			Difficulty = difficulty;
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

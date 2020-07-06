using System.Collections.Generic;
using UnityEngine;

namespace TerraNova.Systems.UserData
{
	[System.Serializable]
	public sealed class ColonyGameScoreRecord
	{
		[SerializeField] private long _WinDateTicks			= default;
		[SerializeField] private string _MissionDescription	= default;
		[SerializeField] private int _Difficulty			= default;
		[SerializeField] private int _GameTime				= default;
		[SerializeField] private string _PlayerName			= default;

		public string MissionDescription	{ get => _MissionDescription;	set => _MissionDescription = value;	}
		public int Difficulty				{ get => _Difficulty;			set => _Difficulty = value;			}
		public int GameTime					{ get => _GameTime;				set => _GameTime = value;			}
		public string PlayerName			{ get => _PlayerName;			set => _PlayerName = value;			}
		public System.DateTime WinDate
		{
			get => new System.DateTime(_WinDateTicks);
			set => _WinDateTicks = value.Ticks;
		}

		public ColonyGameScoreRecord(string missionDescription, int difficulty, int gameTime, string playerName)
		{
			_MissionDescription = missionDescription;
			_Difficulty = difficulty;
			_GameTime = gameTime;
			_PlayerName = playerName;
			WinDate = System.DateTime.Now;
		}
	}

	/// <summary>
	/// Manages access and storage of colony game scores.
	/// </summary>
	public static class ColonyGameScores
	{
		[System.Serializable]
		private sealed class ScoreRecordCollection
		{
			public List<ColonyGameScoreRecord> Scores = new List<ColonyGameScoreRecord>();
		}

		/// <summary>
		/// Returns the user's colony game scores.
		/// </summary>
		public static ColonyGameScoreRecord[] GetScores()
		{
			string scoresJson = PlayerPrefs.GetString("ColonyGameScores");
			if (string.IsNullOrEmpty(scoresJson))
			{
				// No scores found
				return new ColonyGameScoreRecord[0];
			}

			// Return the scores
			ScoreRecordCollection scoreCollection = JsonUtility.FromJson<ScoreRecordCollection>(scoresJson);

			return scoreCollection.Scores.ToArray();
		}

		/// <summary>
		/// Adds a colony game score to local storage.
		/// </summary>
		public static void AddScore(string missionDescription, int difficulty, int gameTime, string playerName)
		{
			ScoreRecordCollection scoreCollection = new ScoreRecordCollection();

			// Get current scores
			string scoresJson = PlayerPrefs.GetString("ColonyGameScores");
			if (!string.IsNullOrEmpty(scoresJson))
			{
				scoreCollection = JsonUtility.FromJson<ScoreRecordCollection>(scoresJson);
			}

			// Add score
			scoreCollection.Scores.Add(new ColonyGameScoreRecord(missionDescription, difficulty, gameTime, playerName));
			
			// Set prefs
			PlayerPrefs.SetString("ColonyGameScores", JsonUtility.ToJson(scoreCollection));
			PlayerPrefs.Save();
		}

		/// <summary>
		/// Clears all colony game scores.
		/// </summary>
		public static void ClearScores()
		{
			PlayerPrefs.DeleteKey("ColonyGameScores");
			PlayerPrefs.Save();
		}
	}
}
using TerraNova.Systems.UserData;
using TerraNova.UserInterface.Generic;
using UnityEngine;

namespace TerraNova.UserInterface.MainMenu
{
	public sealed class ColonyGameScoresPopup : MonoBehaviour
	{
		private const string _PrefabPath = "UserInterface/MainMenu/ColonyGameScoresPopup";

		[SerializeField] private ListBox _ScoresListBox		= default;

		private System.Action _OnCloseCB;


		/// <summary>
		/// Creates the "Colony Game Scores" popup.
		/// </summary>
		public static ColonyGameScoresPopup Create(System.Action onCloseCB)
		{
			GameObject goPopup = Instantiate(Resources.Load<GameObject>(_PrefabPath));
			ColonyGameScoresPopup popup = goPopup.GetComponent<ColonyGameScoresPopup>();

			popup._OnCloseCB = onCloseCB;

			return popup;
		}

		public void Show()
		{
			gameObject.SetActive(true);

			//ColonyGameScores.AddScore("Test desc", 1, 58, "TechCor");
			LoadScores();
		}

		private void LoadScores()
		{
			_ScoresListBox.Clear();

			// Add scores to list box
			foreach (ColonyGameScoreRecord record in ColonyGameScores.GetScores())
			{
				string scoreText = record.MissionDescription + ", " + GetDifficultyString(record.Difficulty) + ", " + record.GameTime.ToString("D7");
				scoreText += ", " + record.PlayerName + ", " + record.WinDate.ToString("M-d-yyyy");
				
				_ScoresListBox.AddItem(scoreText);
			}
		}

		private string GetDifficultyString(int difficulty)
		{
			switch (difficulty)
			{
				case 0: return "Easy";
				case 1: return "Normal";
				case 2: return "Hard";
			}

			return "N/A";
		}

		public void OnClick_ClearScores()
		{
			ColonyGameScores.ClearScores();

			LoadScores();
		}

		public void Close()
		{
			Destroy(gameObject);

			// Inform caller that we have closed
			_OnCloseCB?.Invoke();
		}
	}
}

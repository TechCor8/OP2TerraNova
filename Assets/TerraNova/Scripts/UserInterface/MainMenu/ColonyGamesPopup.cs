using DotNetMissionSDK;
using DotNetMissionSDK.Json;
using System.IO;
using TerraNova.Systems;
using TerraNova.UserInterface.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TerraNova.UserInterface.MainMenu
{
	public sealed class ColonyGamesPopup : MonoBehaviour
	{
		private const string _PrefabPath = "UserInterface/MainMenu/ColonyGamesPopup";

		[SerializeField] private ListBox _MissionListBox	= default;
		[SerializeField] private Toggle[] _ToggleDifficulty = default;

		private System.Action _OnCloseCB;


		private sealed class ListBoxItemContents
		{
			public string MissionFilePath		{ get; private set; }
			public MissionRoot MissionData		{ get; private set; }


			public ListBoxItemContents(string filePath, MissionRoot missionData)
			{
				MissionFilePath = filePath;
				MissionData = missionData;
			}
		}


		/// <summary>
		/// Creates the "Colony Games" popup.
		/// </summary>
		public static ColonyGamesPopup Create(System.Action onCloseCB)
		{
			GameObject goPopup = Instantiate(Resources.Load<GameObject>(_PrefabPath));
			ColonyGamesPopup popup = goPopup.GetComponent<ColonyGamesPopup>();

			popup._OnCloseCB = onCloseCB;

			return popup;
		}

		public void Show()
		{
			gameObject.SetActive(true);

			LoadMissions();
		}

		private void LoadMissions()
		{
			_MissionListBox.Clear();

			// Read missions in "ColonyGames" directory
			if (!Directory.Exists("ColonyGames"))
				return;

			// Add missions to list box
			foreach (string file in Directory.EnumerateFiles("ColonyGames", "*.opm", SearchOption.AllDirectories))
			{
				try
				{
					MissionRoot mission = MissionReader.GetMissionData(file);
					_MissionListBox.AddItem(mission.levelDetails.description, new ListBoxItemContents(file, mission));
				}
				catch (System.Exception ex)
				{
					Debug.LogException(ex);
				}
			}
		}

		public void OnClick_Play()
		{
			// Get mission parameters
			ListBoxItemContents itemContents = _MissionListBox.SelectedItemData as ListBoxItemContents;
			int difficultyIndex;
			for (difficultyIndex = 0; difficultyIndex < _ToggleDifficulty.Length; ++difficultyIndex)
			{
				if (_ToggleDifficulty[difficultyIndex].isOn)
					break;
			}

			// Set mission parameters
			SceneParameters.SetMissionStartParameters(itemContents.MissionFilePath, difficultyIndex);

			// Launch game
			Destroy(gameObject);

			SceneManager.LoadScene("Game");
		}

		public void OnClick_Scores()
		{
			ColonyGameScoresPopup popup = ColonyGameScoresPopup.Create(Show);
			popup.Show();

			Hide();
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void Close()
		{
			Destroy(gameObject);

			// Inform caller that we have closed
			_OnCloseCB?.Invoke();
		}
	}
}

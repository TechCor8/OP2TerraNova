using DotNetMissionSDK;
using DotNetMissionSDK.Json;
using System.IO;
using TerraNova.Systems;
using TerraNova.UserInterface.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TerraNova.UserInterface.MainMenu
{
	public sealed class TutorialsPopup : MonoBehaviour
	{
		private const string _PrefabPath = "UserInterface/MainMenu/TutorialsPopup";

		[SerializeField] private ListBox _MissionListBox	= default;

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
		/// Creates the "Tutorials" popup.
		/// </summary>
		public static TutorialsPopup Create(System.Action onCloseCB)
		{
			GameObject goPopup = Instantiate(Resources.Load<GameObject>(_PrefabPath));
			TutorialsPopup popup = goPopup.GetComponent<TutorialsPopup>();

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

			// Read missions in "Tutorials" directory
			if (!Directory.Exists("Tutorials"))
				return;

			// Add missions to list box
			foreach (string file in Directory.EnumerateFiles("Tutorials", "*.opm", SearchOption.AllDirectories))
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
			
			// Set mission parameters
			SceneParameters.SetMissionStartParameters(itemContents.MissionFilePath, 0);

			// Launch game
			Destroy(gameObject);

			SceneManager.LoadScene("Game");
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

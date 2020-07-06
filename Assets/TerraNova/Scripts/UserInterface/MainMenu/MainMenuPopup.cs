using TerraNova.UserInterface.Generic;
using UnityEngine;

namespace TerraNova.UserInterface.MainMenu
{
	/// <summary>
	/// Displays the main menu for the game.
	/// </summary>
	public class MainMenuPopup : MonoBehaviour
	{
		private const string _PrefabPath = "UserInterface/MainMenu/MainMenuPopup";

		/// <summary>
		/// Creates the main menu popup.
		/// </summary>
		public static MainMenuPopup Create()
		{
			GameObject goPopup = Instantiate(Resources.Load<GameObject>(_PrefabPath));
			MainMenuPopup popup = goPopup.GetComponent<MainMenuPopup>();

			return popup;
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void OnClick_Tutorials()
		{
			TutorialsPopup popup = TutorialsPopup.Create(Show);
			popup.Show();

			Hide();
		}

		public void OnClick_NewCampaign()
		{
		}

		public void OnClick_ColonyGames()
		{
			ColonyGamesPopup popup = ColonyGamesPopup.Create(Show);
			popup.Show();

			Hide();
		}

		public void OnClick_Multiplayer()
		{
		}

		public void OnClick_LoadGame()
		{
		}

		public void OnClick_GamePreferences()
		{
		}

		public void OnClick_Help()
		{
			Application.OpenURL("https://Outpost2.net");
		}

		public void OnClick_About()
		{
			InfoPopup popup = InfoPopup.Create("UserInterface/MainMenu/AboutPopup", Show);
			popup.Show();

			Hide();
		}

		public void OnClick_Exit()
		{
			Application.Quit();
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void Close()
		{
			Destroy(gameObject);
		}
	}
}

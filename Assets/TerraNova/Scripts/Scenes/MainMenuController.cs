using TerraNova.Systems.AssetManagement;
using TerraNova.Systems.Audio;
using TerraNova.UserInterface.MainMenu;
using UnityEngine;

namespace TerraNova.Scenes
{
	/// <summary>
	/// Controls the main menu scene.
	/// </summary>
	public class MainMenuController : MonoBehaviour
	{
		[SerializeField] private GameObject _LoadingImage	= default;


		private void Awake()
		{
			AssetManager.Initialize(this, new string[0], OnInitialized);
		}

		private void OnInitialized(bool success)
		{
			if (!success)
			{
				return;
			}

			// Hide loading image
			_LoadingImage.SetActive(false);

			// Open the main menu
			MainMenuPopup.Create().Show();

			// Play menu music
			MusicPlayer.PlayMusic("Plymth22");
		}
	}
}
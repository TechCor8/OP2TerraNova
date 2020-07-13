using TerraNova.Systems.AssetManagement;
using TerraNova.Systems.Audio;
using TerraNova.Systems.Rendering.Animations;
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

			// TEST: Animations
			animations = new OP2Animation_UGUI[2079];
			for (int i=0; i < 2079; ++i)
			{
				OP2Animation_UGUI animation = OP2Animation_UGUI.Create(AssetManager.GetAnimation(i));
				animation.gameObject.SetActive(false);
				animation.transform.SetParent(GameObject.Find("CanvasBackground").transform);
				animation.transform.localScale = Vector3.one;
				animation.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

				animations[i] = animation;
			}
		}

		private OP2Animation_UGUI[] animations;
		private int currentAnim = 0;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				animations[currentAnim].gameObject.SetActive(false);
				animations[++currentAnim].gameObject.SetActive(true);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				animations[currentAnim].gameObject.SetActive(false);
				animations[--currentAnim].gameObject.SetActive(true);
			}
		}
	}
}
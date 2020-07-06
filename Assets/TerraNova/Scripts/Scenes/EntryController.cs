using UnityEngine;
using UnityEngine.SceneManagement;

namespace TerraNova.Scenes
{
	/// <summary>
	/// Controls the "Entry" scene.
	/// </summary>
	public class EntryController : MonoBehaviour
	{
		private void Awake()
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
using TerraNova.Systems.Audio;
using TerraNova.Systems.Data;
using UnityEngine;

namespace TerraNova.Scenes
{
	/// <summary>
	/// Initializes the game state.
	/// </summary>
	public sealed class GameController : MonoBehaviour
	{
		private void Awake()
		{
			// Stop menu music
			MusicPlayer.StopMusic();

			// Play startup voiceover
			SoundPlayer.PlaySound(VoicesTable.CommandControlInitiated);
		}
	}
}
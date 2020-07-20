using TerraNova.Systems.AssetManagement;
using UnityEngine;

namespace TerraNova.Systems.Audio
{
	public class SoundPlayer : MonoBehaviour
	{
		[SerializeField] private AudioSource _SoundSource = default;

		private static SoundPlayer _Instance;

		public static float UserVolume
		{
			get => _Instance._SoundSource.volume;
			set => _Instance._SoundSource.volume = value;
		}

		
		private void Awake()
		{
			if (_Instance != null)
			{
				Destroy(gameObject);
				throw new System.Exception(nameof(SoundPlayer) + " is already on the scene!");
			}

			_Instance = this;
		}


		public static void PlaySound(string clipName)
		{
			if (_Instance == null)
			{
				Debug.LogWarning("Cannot play sound. " + nameof(SoundPlayer) + " is not on the scene!");
				return;
			}

			AudioClip clip = AssetManager.GetSound(clipName);
			if (clip == null)
			{
				Debug.LogWarning("Could not play sound clip: " + clipName);
				return;
			}

			// Play sound
			_Instance._SoundSource.PlayOneShot(clip, 1);
		}

		private void OnDestroy()
		{
			if (_Instance == this)
				_Instance = null;
		}
	}
}

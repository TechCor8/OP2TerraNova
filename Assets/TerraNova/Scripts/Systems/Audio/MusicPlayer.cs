using TerraNova.Systems.AssetManagement;
using UnityEngine;

namespace TerraNova.Systems.Audio
{
	public class MusicPlayer : MonoBehaviour
	{
		[SerializeField] private AudioSource _MusicSource = default;

		private static MusicPlayer _Instance;

		private string _CurrentClipName;


		private void Awake()
		{
			if (_Instance != null)
			{
				Destroy(gameObject);
				throw new System.Exception("MusicPlayer is already on the scene!");
			}

			_Instance = this;
		}


		public static void PlayMusic(string clipName)
		{
			if (_Instance == null)
			{
				Debug.LogWarning("Cannot play music. MusicPlayer is not on the scene!");
				return;
			}

			if (_Instance._CurrentClipName == clipName)
				return;

			AudioClip musicClip = AssetManager.GetMusicClip(clipName);
			if (musicClip == null)
			{
				Debug.LogWarning("Could not play music clip: " + clipName);
				return;
			}

			// Music settings
			_Instance._MusicSource.spatialBlend = 0;
			_Instance._MusicSource.loop = true;

			_Instance._MusicSource.clip = musicClip;
			_Instance._MusicSource.Play();

			_Instance._CurrentClipName = clipName;
		}

		private void OnDestroy()
		{
			if (_Instance == this)
				_Instance = null;
		}
	}
}

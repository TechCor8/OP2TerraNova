using B83.Image.BMP;
using OP2UtilityDotNet;
using OP2UtilityDotNet.Sprite;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TerraNova.Systems.AssetManagement
{
	/// <summary>
	/// Loads and manages game assets.
	/// Serves as the primary access point for referencing game assets.
	/// </summary>
	public static class AssetManager
	{
		/// <summary>
		/// Contains the art meta data for the game.
		/// </summary>
		private static ArtFile _ArtFile;

		/// <summary>
		/// Contains the legacy artwork for the game.
		/// </summary>
		private static OP2BmpLoader _LegacyArt;

		/// <summary>
		/// Manages legacy assets for the game.
		/// </summary>
		private static ResourceManager _LegacyAssets;

		/// <summary>
		/// Contains modded assets for the game.
		/// This will override over legacy artwork when possible.
		/// If asset names conflict, the higher indexed bundle will be used.
		/// </summary>
		private static List<AssetBundle> _ModBundles = new List<AssetBundle>();


		private static Dictionary<int, Texture2D> _TextureLookup = new Dictionary<int, Texture2D>();
		private static Dictionary<string, AudioClip> _SoundLookup = new Dictionary<string, AudioClip>();


		public delegate void OnInitializeCallback(bool success);


		/// <summary>
		/// Loads required assets for the game.
		/// </summary>
		public static void Initialize(MonoBehaviour coroutineOwner, string[] modBundlePaths, OnInitializeCallback onInitCB)
		{
			// Load art file
			try
			{
				// Prepare Legacy manager
				_LegacyAssets = new ResourceManager(".");

				// Get art file
				Stream artPartStream = _LegacyAssets.GetResourceStream("op2_art.prt");
				_ArtFile = ArtFile.Read(artPartStream);
				
			}
			catch (System.Exception ex)
			{
				Debug.LogException(ex);

				// Inform caller of failure
				onInitCB?.Invoke(false);
				return;
			}

			// Load asset bundle
			coroutineOwner.StartCoroutine(LoadAssetsRoutine(modBundlePaths, onInitCB));
		}

		private static IEnumerator LoadAssetsRoutine(string[] modBundlePaths, OnInitializeCallback onInitCB)
		{
			// Load mod bundles
			foreach (string bundlePath in modBundlePaths)
			{
				AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, bundlePath));
				yield return bundleRequest;
				
				if (bundleRequest.assetBundle != null)
					_ModBundles.Add(bundleRequest.assetBundle);
			}

			// Prepare legacy art loader
			try
			{
				_LegacyArt = new OP2BmpLoader("OP2_ART.BMP", _ArtFile);
			}
			catch (System.Exception ex)
			{
				Debug.LogWarning(ex);
			}

			// Load textures
			for (int i=0; i < _ArtFile.imageMetas.Count; ++i)
			{
				Texture2D texture = GetAssetFromModBundle<Texture2D>("Bitmap" + i);

				if (texture != null)
				{
					_TextureLookup[i] = texture;
				}
				else if (_LegacyArt != null)
				{
					if (i == 652)
					{
						i = 652; // DEBUG:
					}

					// Load legacy texture
					try
					{
						Stream bmpStream = _LegacyArt.GetImageStream(i);
						texture = GetTextureFromBMP(bmpStream);
						_TextureLookup[i] = texture;
					}
					catch (System.Exception ex)
					{
						Debug.LogException(ex);
						Debug.LogError("BmpIndex: " + i);
					}
				}
				else
				{
					// Could not find texture!
					Debug.LogError("Could not find texture for index: " + i);

					// Inform caller of failure
					onInitCB?.Invoke(false);
					yield break;
				}

				// TEST:
				GameObject goTest = null;
				if (texture != null)
				{
					goTest = new GameObject("Bitmap" + i);
					UnityEngine.UI.RawImage image = goTest.AddComponent<UnityEngine.UI.RawImage>();
					image.texture = texture;
					image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, texture.width);
					image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, texture.height);
					goTest.transform.SetParent(GameObject.Find("CanvasBackground").transform);
					goTest.transform.localPosition = Vector3.zero;
					goTest.transform.localScale = new Vector3(1,-1,1);
				}

				yield return null;

				if (goTest != null)
					GameObject.Destroy(goTest);
			}
			

			// Inform caller of success
			onInitCB?.Invoke(true);
		}

		// Gets an asset from a mod bundle based on priority order, or returns null, if the asset is not in any mod bundle.
		private static T GetAssetFromModBundle<T>(string assetName) where T : UnityEngine.Object
		{
			for (int i=_ModBundles.Count-1; i >= 0; --i)
			{
				T asset = _ModBundles[i].LoadAsset<T>(assetName);
				if (asset != null)
					return asset;
			}

			return null;
		}

		private static Texture2D GetTextureFromBMP(Stream bmpStream)
		{
			BMPLoader loader = new BMPLoader();
			BMPImage image = loader.LoadBMP(bmpStream);
			return image.ToTexture2D();
		}

		public static Texture2D GetTexture(int index)
		{
			return _TextureLookup[index];
		}

		public static AudioClip GetSound(string soundName)
		{
			AudioClip clip;
			_SoundLookup.TryGetValue(soundName, out clip);
			return clip;
		}

		/// <summary>
		/// Gets a music clip by name.
		/// NOTE: This will load the clip. Expect possible performance impact.
		/// </summary>
		public static AudioClip GetMusicClip(string musicName)
		{
			// Try to get from a mod bundle
			AudioClip clip = GetAssetFromModBundle<AudioClip>(musicName);
			if (clip != null)
				return clip;

			// Try to get from legacy
			byte[] data = _LegacyAssets.GetResource(musicName);
			if (data == null)
				return null;

			return WavUtility.ToAudioClip(data, 0, musicName);
		}
	}
}

using DotNetMissionSDK;
using DotNetMissionSDK.Json;
using OP2UtilityDotNet;
using OP2UtilityDotNet.OP2Map;
using System.IO;
using TerraNova.Systems;
using TerraNova.Systems.Audio;
using TerraNova.Systems.Constants;
//using TerraNova.Systems.GameMap;
using TerraNova.Systems.State;
using TerraNova.UserInterface.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TerraNova.Scenes
{
	/// <summary>
	/// Initializes the game state.
	/// </summary>
	public sealed class GameController : MonoBehaviour
	{
		//[SerializeField] private MapRenderer _MapRenderer	= default;

		
		private void Awake()
		{
			// Stop menu music
			MusicPlayer.StopMusic();

			// Load mission
			MissionRoot mission = LoadMission(SceneParameters.MissionPath);
			if (mission == null)
			{
				InfoPopup.Create("Failed to load mission.", OnLoadFailed);
				return;
			}

			// Load map
			Map map = LoadMap(mission);
			if (mission == null)
			{
				InfoPopup.Create("Failed to load map.", OnLoadFailed);
				return;
			}

			// Load game state
			string error;
			if (!GameState.Initialize(mission, SceneParameters.RandomSeed, out error))
			{
				InfoPopup.Create(error, OnLoadFailed);
				return;
			}

			// Setup Game
			//SetDaylightEverywhere(tethysGame.daylightEverywhere);
			//SetDaylightMoves(tethysGame.daylightMoves);
			//SetInitialLightLevel(tethysGame.initialLightLevel);

			// Initialize map
			//_MapRenderer.Initialize(mission, map, OnLoadMapComplete);
		}

		private MissionRoot LoadMission(string path)
		{
			MissionRoot missionRoot = null;

			// Load mission
			try
			{
				missionRoot = MissionReader.GetMissionData(path);
			}
			catch (System.Exception ex)
			{
				Debug.LogException(ex);
			}

			return missionRoot;
		}

		private Map LoadMap(MissionRoot mission)
		{
			// Load map
			using (ResourceManager resourceManager = new ResourceManager("."))
			{
				using (Stream mapStream = resourceManager.GetResourceStream(mission.levelDetails.mapName, true))
				{
					if (mapStream == null)
						return null;

					return Map.ReadMap(mapStream);
				}
			}
		}

		private void OnLoadMapComplete()
		{
			// Play startup voiceover
			SoundPlayer.PlaySound(VoicesTable.CommandControlInitiated);
		}

		private void OnLoadFailed()
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
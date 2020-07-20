using System.Runtime.InteropServices;
using TerraNova.Systems.Audio;
using TerraNova.UserInterface.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TerraNova.UserInterface.Game
{
	/// <summary>
	/// Controls buttons on the frame.
	/// </summary>
	public class FrameController : MonoBehaviour
	{
		[SerializeField] private GameObject _Focus		= default;
		[SerializeField] private GameObject _NoFocus	= default;

		[SerializeField] private Button _BtnMaximize	= default;
		[SerializeField] private Button _BtnWindowed	= default;

		private float _OriginalTimeScale;

		private const string ClickSound = "beep5";


		private void Awake()
		{
			RefreshFocus();
			RefreshMaximizeButton();
		}

		public void OnClick_Close()
		{
			// Pause game
			_OriginalTimeScale = Time.timeScale;
			Time.timeScale = 0;

			// Confirm quit
			ConfirmPopup.CreateFromResource("UserInterface/Game/QuitPopup", OnQuitPopupClosed);

			SoundPlayer.PlaySound(ClickSound);
		}

		private void OnQuitPopupClosed(bool didConfirm)
		{
			if (didConfirm)
				Application.Quit();
			else
			{
				// Unpause
				Time.timeScale = _OriginalTimeScale;
			}
		}

		public void OnClick_Maximize()
		{
			Screen.fullScreen = !Screen.fullScreen;

			RefreshMaximizeButton();

			SoundPlayer.PlaySound(ClickSound);
		}

#if UNITY_STANDALONE_WIN
		[DllImport("user32.dll")] private static extern bool ShowWindow(System.IntPtr hwnd, int nCmdShow);
		[DllImport("user32.dll")] private static extern System.IntPtr GetActiveWindow();
 
		public void OnClick_Minimize()
		{
			ShowWindow(GetActiveWindow(), 2);

			SoundPlayer.PlaySound(ClickSound);
		}
#else
		public void OnClick_Minimize()
		{
			Debug.LogWarning("Minimize not supported on this platform!");

			SoundPlayer.PlaySound(ClickSound);
		}
#endif

		private void Update()
		{
			// Detect full screen change
			if (Screen.fullScreen != _BtnWindowed.gameObject.activeSelf)
			{
				RefreshMaximizeButton();
			}
		}

		private void OnApplicationFocus(bool focus)
		{
			RefreshFocus();
		}

		private void RefreshFocus()
		{
			_Focus.gameObject.SetActive(Application.isFocused);
			_NoFocus.gameObject.SetActive(!Application.isFocused);
		}

		private void RefreshMaximizeButton()
		{
			_BtnMaximize.gameObject.SetActive(!Screen.fullScreen);
			_BtnWindowed.gameObject.SetActive(Screen.fullScreen);
		}
	}
}
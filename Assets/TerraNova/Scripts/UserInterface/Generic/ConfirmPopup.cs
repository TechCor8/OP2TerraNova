using UnityEngine;

namespace TerraNova.UserInterface.Generic
{
	/// <summary>
	/// Represents a confirmation popup.
	/// </summary>
	public class ConfirmPopup : MonoBehaviour
	{
		public delegate void OnConfirmCallback(bool didConfirm);

		private OnConfirmCallback _OnClosedCB;


		public static ConfirmPopup CreateFromResource(string prefabPath, OnConfirmCallback onClosedCB)
		{
			GameObject goPopup = Instantiate(Resources.Load<GameObject>(prefabPath));
			ConfirmPopup popup = goPopup.GetComponent<ConfirmPopup>();

			popup._OnClosedCB = onClosedCB;

			return popup;
		}

		public void OnClick_Yes()
		{
			_OnClosedCB?.Invoke(true);

			Destroy(gameObject);
		}

		public void OnClick_No()
		{
			_OnClosedCB?.Invoke(false);

			Destroy(gameObject);
		}
	}
}
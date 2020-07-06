using UnityEngine;

namespace TerraNova.UserInterface.Generic
{
	/// <summary>
	/// Displays an informational popup.
	/// Informational popups have a single button that closes the popup.
	/// </summary>
	public class InfoPopup : MonoBehaviour
	{
		private System.Action _OnCloseCB;


		/// <summary>
		/// Creates an info popup from a resource path.
		/// </summary>
		public static InfoPopup Create(string prefabPath, System.Action onCloseCB)
		{
			GameObject goPopup = Instantiate(Resources.Load<GameObject>(prefabPath));
			InfoPopup popup = goPopup.GetComponent<InfoPopup>();

			popup._OnCloseCB = onCloseCB;

			return popup;
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Close()
		{
			Destroy(gameObject);

			// Inform caller that we have closed
			_OnCloseCB?.Invoke();
		}
	}
}

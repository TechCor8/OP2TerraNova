using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TerraNova.UserInterface.Utility
{
	/// <summary>
	/// Makes a window draggable.
	/// </summary>
	public class DraggableWindow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[SerializeField] private RectTransform m_DraggableWindow		= default;


		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		public void OnDrag(PointerEventData eventData)
		{
			CanvasScaler canvas = m_DraggableWindow.GetComponentInParent<CanvasScaler>();
			Vector2 refResolution = canvas.referenceResolution;

			Vector2 ratio = new Vector2(Screen.width / refResolution.x, Screen.height / refResolution.y);
			float matchRatio = Mathf.Lerp(ratio.x, ratio.y, canvas.matchWidthOrHeight);

			// Drag the window
			m_DraggableWindow.anchoredPosition += new Vector2(eventData.delta.x / matchRatio, eventData.delta.y / matchRatio);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			CanvasScaler canvas = m_DraggableWindow.GetComponentsInParent<CanvasScaler>(true)[0];
			Vector2 refResolution = canvas.referenceResolution;

			Vector3[] corners = new Vector3[4];
			m_DraggableWindow.GetWorldCorners(corners);
			Rect windowBounds = new Rect();
			windowBounds.min = new Vector2(corners[0].x / Screen.width, corners[0].y / Screen.height);
			windowBounds.max = new Vector2(corners[2].x / Screen.width, corners[2].y / Screen.height);

			// Keep the window inside the screen bounds
			Vector2 offsetNeeded = new Vector2();

			if (windowBounds.xMin < 0) offsetNeeded.x = windowBounds.xMin;
			if (windowBounds.yMin < 0) offsetNeeded.y = windowBounds.yMin;
			if (windowBounds.xMax > 1) offsetNeeded.x = windowBounds.xMax - 1;
			if (windowBounds.yMax > 1) offsetNeeded.y = windowBounds.yMax - 1;

			m_DraggableWindow.anchoredPosition -= new Vector2(offsetNeeded.x * refResolution.x, offsetNeeded.y * refResolution.y);
		}

		private void OnRectTransformDimensionsChange()
		{
			// If window has changed size, keep it in bounds
			OnEndDrag(null);
		}
	}
}

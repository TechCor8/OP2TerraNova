using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TerraNova.UserInterface.Generic
{
	public class ListBox : MonoBehaviour
	{
		[SerializeField] private ScrollRect _ScrollView		= default;
		[SerializeField] private RectTransform _Container	= default;

		[SerializeField] private GameObject _ItemPrefab		= default;

		public int SelectedIndex		{ get => GetSelectedItemIndex();	}
		public object SelectedItemData	{ get => GetSelectedItemData();		}


		private void Awake()
		{
			Clear();
		}

		public void AddItem(string label, object itemData=null)
		{
			GameObject goItem = Instantiate(_ItemPrefab);
			ListBoxItem item = goItem.GetComponent<ListBoxItem>();

			goItem.transform.SetParent(_Container);
			goItem.transform.localScale = Vector3.one;

			item.Initialize(label, itemData);

			goItem.SetActive(true);
		}

		public void Clear()
		{
			// Destroy all items in container
			foreach (Transform t in _Container)
			{
				Destroy(t.gameObject);
			}
		}

		public void OnSelect_Item()
		{
			// Get selected item and visible rect
			Toggle selectedToggle = _Container.GetComponentsInChildren<Toggle>().First(toggle => toggle.isOn);
			RectTransform selectedRectTransform = selectedToggle.GetComponent<RectTransform>();

			// Auto-scroll to item outside of viewport
			Vector3[] corners = new Vector3[4];
			selectedRectTransform.GetWorldCorners(corners);
			Rect selectedRect = new Rect();
			selectedRect.min = corners[0];
			selectedRect.max = corners[2];
			
			_ScrollView.viewport.GetWorldCorners(corners);
			Rect viewportRect = new Rect();
			viewportRect.min = corners[0];
			viewportRect.max = corners[2];

			_Container.GetWorldCorners(corners);
			Rect containerRect = new Rect();
			containerRect.min = corners[0];
			containerRect.max = corners[2];

			if (selectedRect.yMin < viewportRect.yMin)
			{
				_ScrollView.verticalScrollbar.value = (selectedRect.yMin - containerRect.yMin) / (containerRect.height - viewportRect.height);
			}

			if (selectedRect.yMax > viewportRect.yMax)
			{
				_ScrollView.verticalScrollbar.value = (selectedRect.yMax - containerRect.yMin - viewportRect.height) / (containerRect.height - viewportRect.height);
			}
		}

		private int GetSelectedItemIndex()
		{
			Toggle selectedToggle = _Container.GetComponentsInChildren<Toggle>().First(toggle => toggle.isOn);
			
			return selectedToggle.transform.GetSiblingIndex();
		}

		private object GetSelectedItemData()
		{
			Toggle selectedToggle = _Container.GetComponentsInChildren<Toggle>().First(toggle => toggle.isOn);
			ListBoxItem item = selectedToggle.GetComponent<ListBoxItem>();

			return item.ItemData;
		}
	}
}
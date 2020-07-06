using UnityEngine;
using UnityEngine.UI;

namespace TerraNova.UserInterface.Generic
{
	/// <summary>
	/// Represents a ListBox item.
	/// Sets the item label and stores item data.
	/// </summary>
	public class ListBoxItem : MonoBehaviour
	{
		[SerializeField] private Text _Label = default;

		public object ItemData { get; set; }


		public void Initialize(string label, object itemData)
		{
			_Label.text = label;

			ItemData = itemData;
		}
	}
}
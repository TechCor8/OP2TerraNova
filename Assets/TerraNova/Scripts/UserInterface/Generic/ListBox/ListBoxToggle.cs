using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ListBoxToggle : Toggle
{
	// We want to always turn on the toggle when selecting via keyboard or on mouse down.
	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);

		isOn = true;
	}
}

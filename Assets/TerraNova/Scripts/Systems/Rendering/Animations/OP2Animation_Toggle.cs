using TerraNova.Systems.AssetManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TerraNova.Systems.Rendering.Animations
{
	/// <summary>
	/// Toggle button created from an OP2 animation.
	/// </summary>
	public class OP2Animation_Toggle : MonoBehaviour
	{
		[SerializeField] private int _AnimationIndex	= default;

		[SerializeField] private ToggleGroup _Group		= default;

		protected OP2UtilityDotNet.Sprite.Animation _OP2Animation;
		private Toggle _ToggleButton;


		private void Awake()
		{
			// Initialize animation
			OP2UtilityDotNet.Sprite.Animation animationData = AssetManager.GetAnimation(_AnimationIndex);
			if (animationData == null)
			{
				Debug.LogWarning("Animation data not found for index: " + _AnimationIndex);
				return;
			}

			_OP2Animation = animationData;

			// Create toggle
			_ToggleButton = gameObject.AddComponent<Toggle>();
			_ToggleButton.group = _Group;

			// Background image
			//GameObject goBackground = new GameObject("Background");
			//goBackground.transform.SetParent(transform);
			Image backgroundImage = gameObject.GetComponent<Image>();
			if (backgroundImage == null)
				backgroundImage = gameObject.AddComponent<Image>();
			backgroundImage.sprite = GetSpriteForFrame(0);
			//backgroundImage.rectTransform.anchoredPosition = Vector2.zero;
			backgroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, backgroundImage.sprite.rect.width);
			backgroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, backgroundImage.sprite.rect.height);
			_ToggleButton.targetGraphic = backgroundImage;

			_ToggleButton.transition = Selectable.Transition.SpriteSwap;
			_ToggleButton.toggleTransition = Toggle.ToggleTransition.None;

			SpriteState state = new SpriteState();

			state.highlightedSprite = backgroundImage.sprite;
			state.pressedSprite = backgroundImage.sprite;
			state.selectedSprite = backgroundImage.sprite;
			state.disabledSprite = GetSpriteForFrame(2);

			_ToggleButton.spriteState = state;

			// Checkmark image
			GameObject goCheckmark = new GameObject("Checkmark");
			goCheckmark.transform.SetParent(transform);
			Image checkImage = goCheckmark.AddComponent<Image>();
			checkImage.sprite = GetSpriteForFrame(1);
			checkImage.rectTransform.anchoredPosition = Vector2.zero;
			checkImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, checkImage.sprite.rect.width);
			checkImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, checkImage.sprite.rect.height);
			_ToggleButton.graphic = checkImage;

			// Set toggle button size to match image size
			GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, backgroundImage.sprite.rect.width);
			GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, backgroundImage.sprite.rect.height);

			// No update for toggle buttons
			enabled = false;
		}

		private Sprite GetSpriteForFrame(int frameIndex)
		{
			// Get sprite
			return AssetManager.GetSprite(_OP2Animation.frames[frameIndex].layers[0].bitmapIndex);
		}
	}
}

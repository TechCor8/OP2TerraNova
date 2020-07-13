using System.Collections.Generic;
using System.Linq;
using TerraNova.Systems.AssetManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TerraNova.Systems.Rendering.Animations
{
	/// <summary>
	/// This class loads an OP2 animation from the art file and renders it.
	/// The class also supports image overrides for HD texture support.
	/// </summary>
	public class OP2Animation_UGUI : MonoBehaviour
	{
		private OP2UtilityDotNet.Sprite.Animation _OP2Animation;
		private Image[] _RenderLayers;

		private float _CurrentTime;
		private int _CurrentFrame;

		private const float _FramesPerSecond = 30;
		private const float _SecondsPerFrame = 1.0f / _FramesPerSecond;



		/// <summary>
		/// Creates an OP2Animation object.
		/// Initializes automatically.
		/// </summary>
		public static OP2Animation_UGUI Create(OP2UtilityDotNet.Sprite.Animation op2Animation, string name="OP2Animation")
		{
			GameObject root = new GameObject(name, typeof(RectTransform));
			RectTransform rectTransform = root.GetComponent<RectTransform>();
			rectTransform.anchorMin = new Vector2(0, 1);
			rectTransform.anchorMax = new Vector2(0, 1);
			rectTransform.pivot = new Vector2(0, 1);

			OP2Animation_UGUI animation = root.AddComponent<OP2Animation_UGUI>();
			animation.Initialize(op2Animation);

			return animation;
		}

		private void Awake()
		{
			// Disable update if animation has not been initialized
			if (_OP2Animation == null)
				enabled = false;
		}

		public void Initialize(OP2UtilityDotNet.Sprite.Animation op2Animation)
		{
			if (_OP2Animation != null)
			{
				Release();
			}

			_OP2Animation = op2Animation;

			// Get max layers for animation
			int maxLayers = _OP2Animation.frames.Max((frame) => frame.layers.Count);

			// Add layers
			_RenderLayers = new Image[maxLayers];

			for (int i=0; i < maxLayers; ++i)
			{
				GameObject goLayer = new GameObject("Layer" + i);
				Image image = goLayer.AddComponent<Image>();

				image.rectTransform.SetParent(transform);

				image.rectTransform.localScale = Vector3.one;
				image.rectTransform.localRotation = Quaternion.identity;

				image.rectTransform.anchorMin = new Vector2(0, 1);
				image.rectTransform.anchorMax = new Vector2(0, 1);
				image.rectTransform.pivot = new Vector2(0, 1);
				image.rectTransform.anchoredPosition = Vector2.zero;

				_RenderLayers[i] = image;
			}
			
			enabled = true;

			// Show first frame
			SetFrame(0);
		}

		private void Update()
		{
			int currentFrame = _CurrentFrame;

			// Update frame time
			_CurrentTime += Time.deltaTime;
			while (_CurrentTime >= _SecondsPerFrame)
			{
				_CurrentTime -= _SecondsPerFrame;
				++_CurrentFrame;

				if (_CurrentFrame >= _OP2Animation.frames.Count)
					_CurrentFrame -= _OP2Animation.frames.Count;
			}

			if (currentFrame != _CurrentFrame)
			{
				SetFrame(_CurrentFrame);
			}
		}

		private void SetFrame(int frameIndex)
		{
			_CurrentFrame = frameIndex;

			// Update rendered layers
			List<OP2UtilityDotNet.Sprite.Animation.Frame.Layer> layers = _OP2Animation.frames[frameIndex].layers;

			for (int i=0; i < _RenderLayers.Length; ++i)
			{
				if (i >= layers.Count)
				{
					// No sprite for layer
					_RenderLayers[i].enabled = false;
					continue;
				}

				// Get sprite
				Sprite frameSprite = AssetManager.GetSprite(layers[i].bitmapIndex);
				if (frameSprite == null)
				{
					// No sprite for layer
					_RenderLayers[i].enabled = false;
					continue;
				}

				// Set sprite
				_RenderLayers[i].sprite = frameSprite;
				_RenderLayers[i].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, frameSprite.rect.width);
				_RenderLayers[i].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, frameSprite.rect.height);
				_RenderLayers[i].enabled = true;

				_RenderLayers[i].rectTransform.anchoredPosition = new Vector2(layers[i].pixelOffset.x, -layers[i].pixelOffset.y);
			}

			// TODO: Frame offset?
			//_OP2Animation.pixelDisplacement
		}

		public void Release()
		{
			// Destroy layers
			foreach (Image renderImage in _RenderLayers)
			{
				Destroy(renderImage.gameObject);
			}

			_OP2Animation = null;
			_RenderLayers = null;

			_CurrentFrame = 0;
			_CurrentTime = 0;

			enabled = false; // Disable update
		}
	}
}

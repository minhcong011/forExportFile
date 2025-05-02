// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.SpriteConfig
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class SpriteConfig
	{
		public SpriteConfig()
		{
			this.enabled = true;
			this.color = Color.white;
			this.rotation = 0f;
			this.scale = 1f;
			this.duration = 0.1f;
			this.baseTransitionTime = 0.1f;
			this.colorTransitionFactor = 1f;
			this.scaleTransitionFactor = 1f;
			this.rotationTransitionFactor = 1f;
			this.offsetTransitionFactor = 1f;
			this.oneShotState = false;
		}

		public SpriteConfig(Sprite sprite, Color color) : this()
		{
			this.sprite = sprite;
			this.color = color;
		}

		public SpriteConfig(bool enabled, bool oneShot, float scale) : this()
		{
			this.scale = scale;
			this.enabled = enabled;
			this.oneShotState = oneShot;
			this.resetOffset = oneShot;
			this.resetScale = oneShot;
			this.resetRotation = oneShot;
		}

		public bool enabled;

		public Sprite sprite;

		public Color color;

		public float scale;

		public float rotation;

		public Vector2 offset;

		public bool resetOffset;

		public bool resetRotation;

		public bool resetScale;

		[NonSerialized]
		public bool oneShotState;

		public float duration;

		public float baseTransitionTime;

		public float colorTransitionFactor;

		public float scaleTransitionFactor;

		public float rotationTransitionFactor;

		public float offsetTransitionFactor;
	}
}

// dnSpy decompiler from Assembly-CSharp.dll class: Lean.LeanLocalizedSpriteRenderer
using System;
using UnityEngine;

namespace Lean
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(SpriteRenderer))]
	[AddComponentMenu("Lean/Localized Sprite Renderer")]
	public class LeanLocalizedSpriteRenderer : LeanLocalizedBehaviour
	{
		public override void UpdateTranslation(LeanTranslation translation)
		{
			SpriteRenderer component = base.GetComponent<SpriteRenderer>();
			if (translation != null)
			{
				component.sprite = (translation.Object as Sprite);
			}
			else if (this.AllowFallback)
			{
				component.sprite = this.FallbackSprite;
			}
		}

		public bool AllowFallback;

		public Sprite FallbackSprite;
	}
}

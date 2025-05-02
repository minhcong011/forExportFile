// dnSpy decompiler from Assembly-CSharp.dll class: Lean.LeanLocalizedImage
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lean
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Image))]
	[AddComponentMenu("Lean/Localized Image")]
	public class LeanLocalizedImage : LeanLocalizedBehaviour
	{
		public override void UpdateTranslation(LeanTranslation translation)
		{
			Image component = base.GetComponent<Image>();
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

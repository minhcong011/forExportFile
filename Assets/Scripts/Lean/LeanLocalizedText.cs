// dnSpy decompiler from Assembly-CSharp.dll class: Lean.LeanLocalizedText
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lean
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Text))]
	[AddComponentMenu("Lean/Localized Text")]
	public class LeanLocalizedText : LeanLocalizedBehaviour
	{
		public override void UpdateTranslation(LeanTranslation translation)
		{
			Text component = base.GetComponent<Text>();
			if (translation != null)
			{
				component.text = translation.Text;
			}
			else if (this.AllowFallback)
			{
				component.text = this.FallbackText;
			}
		}

		public bool AllowFallback;

		public string FallbackText;
	}
}

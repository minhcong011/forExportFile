// dnSpy decompiler from Assembly-CSharp.dll class: Lean.LeanLocalizedAudioSource
using System;
using UnityEngine;

namespace Lean
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(AudioSource))]
	[AddComponentMenu("Lean/Localized Audio Source")]
	public class LeanLocalizedAudioSource : LeanLocalizedBehaviour
	{
		public override void UpdateTranslation(LeanTranslation translation)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (translation != null)
			{
				component.clip = (translation.Object as AudioClip);
			}
			else if (this.AllowFallback)
			{
				component.clip = this.FallbackAudioClip;
			}
		}

		public bool AllowFallback;

		public AudioClip FallbackAudioClip;
	}
}

// dnSpy decompiler from Assembly-CSharp.dll class: Lean.LeanLocalizedBehaviour
using System;
using UnityEngine;

namespace Lean
{
	public abstract class LeanLocalizedBehaviour : MonoBehaviour
	{
		public abstract void UpdateTranslation(LeanTranslation translation);

		public void SetPhraseName(string newPhraseName)
		{
			if (this.PhraseName != newPhraseName)
			{
				this.PhraseName = newPhraseName;
				this.UpdateLocalization();
			}
		}

		public void UpdateLocalization()
		{
			this.UpdateTranslation(LeanLocalization.GetTranslation(this.PhraseName));
		}

		protected virtual void OnEnable()
		{
			LeanLocalization.OnLocalizationChanged = (Action)Delegate.Combine(LeanLocalization.OnLocalizationChanged, new Action(this.UpdateLocalization));
			this.UpdateLocalization();
		}

		protected virtual void OnDisable()
		{
			LeanLocalization.OnLocalizationChanged = (Action)Delegate.Remove(LeanLocalization.OnLocalizationChanged, new Action(this.UpdateLocalization));
		}

		[LeanPhraseName]
		public string PhraseName;
	}
}

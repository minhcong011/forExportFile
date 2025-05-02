// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.RPG.CharacterAction
using System;
using UnityEngine;

namespace ControlFreak2.Demos.RPG
{
	public class CharacterAction : MonoBehaviour
	{
		private void OnEnable()
		{
			this.animator = base.GetComponent<Animator>();
		}

		private void Update()
		{
			if (!string.IsNullOrEmpty(this.actionButonName) && CF2Input.GetButtonDown(this.actionButonName))
			{
				this.PerformAction();
			}
		}

		public void PerformAction()
		{
			if (this.levelData == null)
			{
				return;
			}
			InteractiveObjectBase interactiveObjectBase = this.levelData.FindInteractiveObjectFor(this);
			if (interactiveObjectBase != null)
			{
				if (!string.IsNullOrEmpty(this.actionAnimatorTrigger) && this.animator != null)
				{
					this.animator.SetTrigger(this.actionAnimatorTrigger);
				}
				interactiveObjectBase.OnCharacterAction(this);
			}
		}

		public LevelData levelData;

		public string actionButonName = "Action";

		public string actionAnimatorTrigger = "Action";

		private Animator animator;
	}
}

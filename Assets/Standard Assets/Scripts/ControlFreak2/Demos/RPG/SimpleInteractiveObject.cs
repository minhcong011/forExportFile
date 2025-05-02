// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.RPG.SimpleInteractiveObject
using System;
using UnityEngine;

namespace ControlFreak2.Demos.RPG
{
	public class SimpleInteractiveObject : InteractiveObjectBase
	{
		public override void OnCharacterAction(CharacterAction chara)
		{
			this.isActivated = !this.isActivated;
			if (this.targetRenderer != null)
			{
				this.targetRenderer.material.color = ((!this.isActivated) ? Color.white : this.activatedColor);
			}
			if (this.soundEffect != null)
			{
				AudioSource.PlayClipAtPoint(this.soundEffect, base.transform.position);
			}
		}

		public Color activatedColor = Color.green;

		public Renderer targetRenderer;

		public AudioClip soundEffect;

		private bool isActivated;
	}
}

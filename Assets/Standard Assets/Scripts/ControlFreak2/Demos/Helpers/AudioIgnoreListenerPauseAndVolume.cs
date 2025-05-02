// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.Helpers.AudioIgnoreListenerPauseAndVolume
using System;
using UnityEngine;

namespace ControlFreak2.Demos.Helpers
{
	public class AudioIgnoreListenerPauseAndVolume : MonoBehaviour
	{
		private void OnEnable()
		{
			if (this.targetSources == null || this.targetSources.Length == 0)
			{
				this.targetSources = base.GetComponents<AudioSource>();
			}
			for (int i = 0; i < this.targetSources.Length; i++)
			{
				if (this.targetSources[i] != null)
				{
					this.targetSources[i].ignoreListenerPause = this.ignoreListenerPause;
					this.targetSources[i].ignoreListenerVolume = this.ignoreListenerVolume;
				}
			}
		}

		public AudioSource[] targetSources;

		public bool ignoreListenerVolume = true;

		public bool ignoreListenerPause = true;
	}
}

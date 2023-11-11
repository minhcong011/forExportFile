using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x02000021 RID: 33
	public class ParticleSystemMultiplier : MonoBehaviour
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000045C4 File Offset: 0x000027C4
		private void Start()
		{
			foreach (ParticleSystem particleSystem in base.GetComponentsInChildren<ParticleSystem>())
			{
				ParticleSystem.MainModule main = particleSystem.main;
				main.startSizeMultiplier *= this.multiplier;
				main.startSpeedMultiplier *= this.multiplier;
				main.startLifetimeMultiplier *= Mathf.Lerp(this.multiplier, 1f, 0.5f);
				particleSystem.Clear();
				particleSystem.Play();
			}
		}

		// Token: 0x0400009A RID: 154
		public float multiplier = 1f;
	}
}

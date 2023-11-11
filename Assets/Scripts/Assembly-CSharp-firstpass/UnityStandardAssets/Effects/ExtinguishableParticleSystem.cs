using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x0200001E RID: 30
	public class ExtinguishableParticleSystem : MonoBehaviour
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00004317 File Offset: 0x00002517
		private void Start()
		{
			this.m_Systems = base.GetComponentsInChildren<ParticleSystem>();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004328 File Offset: 0x00002528
		public void Extinguish()
		{
			ParticleSystem[] systems = this.m_Systems;
			for (int i = 0; i < systems.Length; i++)
			{
                ParticleSystem.EmissionModule e = systems[i].emission;

                e.enabled = false;
			}
		}

		// Token: 0x0400008F RID: 143
		public float multiplier = 1f;

		// Token: 0x04000090 RID: 144
		private ParticleSystem[] m_Systems;
	}
}

using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x02000020 RID: 32
	public class Hose : MonoBehaviour
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000044E4 File Offset: 0x000026E4
		private void Update()
		{
			this.m_Power = Mathf.Lerp(this.m_Power, Input.GetMouseButton(0) ? this.maxPower : this.minPower, Time.deltaTime * this.changeSpeed);
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				this.systemRenderer.enabled = !this.systemRenderer.enabled;
			}
			foreach (ParticleSystem particleSystem in this.hoseWaterSystems)
			{
                ParticleSystem.MainModule e = particleSystem.main;
                ParticleSystem.EmissionModule f = particleSystem.emission;

               e.startSpeed = this.m_Power;
			f.enabled = (this.m_Power > this.minPower * 1.1f);
			}
		}

		// Token: 0x04000094 RID: 148
		public float maxPower = 20f;

		// Token: 0x04000095 RID: 149
		public float minPower = 5f;

		// Token: 0x04000096 RID: 150
		public float changeSpeed = 5f;

		// Token: 0x04000097 RID: 151
		public ParticleSystem[] hoseWaterSystems;

		// Token: 0x04000098 RID: 152
		public Renderer systemRenderer;

		// Token: 0x04000099 RID: 153
		private float m_Power;
	}
}

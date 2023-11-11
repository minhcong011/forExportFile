using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200000B RID: 11
	public class DynamicShadowSettings : MonoBehaviour
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002A97 File Offset: 0x00000C97
		private void Start()
		{
			this.m_OriginalStrength = this.sunLight.shadowStrength;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002AAC File Offset: 0x00000CAC
		private void Update()
		{
			Ray ray = new Ray(Camera.main.transform.position, -Vector3.up);
			float num = base.transform.position.y;
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				num = raycastHit.distance;
			}
			if (Mathf.Abs(num - this.m_SmoothHeight) > 1f)
			{
				this.m_SmoothHeight = Mathf.SmoothDamp(this.m_SmoothHeight, num, ref this.m_ChangeSpeed, this.adaptTime);
			}
			float num2 = Mathf.InverseLerp(this.minHeight, this.maxHeight, this.m_SmoothHeight);
			QualitySettings.shadowDistance = Mathf.Lerp(this.minShadowDistance, this.maxShadowDistance, num2);
			this.sunLight.shadowBias = Mathf.Lerp(this.minShadowBias, this.maxShadowBias, 1f - (1f - num2) * (1f - num2));
			this.sunLight.shadowStrength = Mathf.Lerp(this.m_OriginalStrength, 0f, num2);
		}

		// Token: 0x04000021 RID: 33
		public Light sunLight;

		// Token: 0x04000022 RID: 34
		public float minHeight = 10f;

		// Token: 0x04000023 RID: 35
		public float minShadowDistance = 80f;

		// Token: 0x04000024 RID: 36
		public float minShadowBias = 1f;

		// Token: 0x04000025 RID: 37
		public float maxHeight = 1000f;

		// Token: 0x04000026 RID: 38
		public float maxShadowDistance = 10000f;

		// Token: 0x04000027 RID: 39
		public float maxShadowBias = 0.1f;

		// Token: 0x04000028 RID: 40
		public float adaptTime = 1f;

		// Token: 0x04000029 RID: 41
		private float m_SmoothHeight;

		// Token: 0x0400002A RID: 42
		private float m_ChangeSpeed;

		// Token: 0x0400002B RID: 43
		private float m_OriginalStrength = 1f;
	}
}

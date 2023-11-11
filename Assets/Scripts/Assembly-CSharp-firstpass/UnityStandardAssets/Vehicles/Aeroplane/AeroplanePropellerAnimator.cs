using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000049 RID: 73
	public class AeroplanePropellerAnimator : MonoBehaviour
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00009224 File Offset: 0x00007424
		private void Awake()
		{
			this.m_Plane = base.GetComponent<AeroplaneController>();
			this.m_PropellorModelRenderer = this.m_PropellorModel.GetComponent<Renderer>();
			this.m_PropellorBlurRenderer = this.m_PropellorBlur.GetComponent<Renderer>();
			this.m_PropellorBlur.parent = this.m_PropellorModel;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009270 File Offset: 0x00007470
		private void Update()
		{
			this.m_PropellorModel.Rotate(0f, this.m_MaxRpm * this.m_Plane.Throttle * Time.deltaTime * 60f, 0f);
			int num = 0;
			if (this.m_Plane.Throttle > this.m_ThrottleBlurStart)
			{
				num = Mathf.FloorToInt(Mathf.InverseLerp(this.m_ThrottleBlurStart, this.m_ThrottleBlurEnd, this.m_Plane.Throttle) * (float)(this.m_PropellorBlurTextures.Length - 1));
			}
			if (num != this.m_PropellorBlurState)
			{
				this.m_PropellorBlurState = num;
				if (this.m_PropellorBlurState == 0)
				{
					this.m_PropellorModelRenderer.enabled = true;
					this.m_PropellorBlurRenderer.enabled = false;
					return;
				}
				this.m_PropellorModelRenderer.enabled = false;
				this.m_PropellorBlurRenderer.enabled = true;
				this.m_PropellorBlurRenderer.material.mainTexture = this.m_PropellorBlurTextures[this.m_PropellorBlurState];
			}
		}

		// Token: 0x04000194 RID: 404
		[SerializeField]
		private Transform m_PropellorModel;

		// Token: 0x04000195 RID: 405
		[SerializeField]
		private Transform m_PropellorBlur;

		// Token: 0x04000196 RID: 406
		[SerializeField]
		private Texture2D[] m_PropellorBlurTextures;

		// Token: 0x04000197 RID: 407
		[SerializeField]
		[Range(0f, 1f)]
		private float m_ThrottleBlurStart = 0.25f;

		// Token: 0x04000198 RID: 408
		[SerializeField]
		[Range(0f, 1f)]
		private float m_ThrottleBlurEnd = 0.5f;

		// Token: 0x04000199 RID: 409
		[SerializeField]
		private float m_MaxRpm = 2000f;

		// Token: 0x0400019A RID: 410
		private AeroplaneController m_Plane;

		// Token: 0x0400019B RID: 411
		private int m_PropellorBlurState = -1;

		// Token: 0x0400019C RID: 412
		private const float k_RpmToDps = 60f;

		// Token: 0x0400019D RID: 413
		private Renderer m_PropellorModelRenderer;

		// Token: 0x0400019E RID: 414
		private Renderer m_PropellorBlurRenderer;
	}
}

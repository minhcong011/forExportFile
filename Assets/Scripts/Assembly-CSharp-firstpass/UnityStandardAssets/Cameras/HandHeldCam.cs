using System;
using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	// Token: 0x0200005A RID: 90
	public class HandHeldCam : LookatTarget
	{
		// Token: 0x060001FC RID: 508 RVA: 0x0000B950 File Offset: 0x00009B50
		protected override void FollowTarget(float deltaTime)
		{
			base.FollowTarget(deltaTime);
			float num = Mathf.PerlinNoise(0f, Time.time * this.m_SwaySpeed) - 0.5f;
			float num2 = Mathf.PerlinNoise(0f, Time.time * this.m_SwaySpeed + 100f) - 0.5f;
			num *= this.m_BaseSwayAmount;
			num2 *= this.m_BaseSwayAmount;
			float num3 = Mathf.PerlinNoise(0f, Time.time * this.m_SwaySpeed) - 0.5f + this.m_TrackingBias;
			float num4 = Mathf.PerlinNoise(0f, Time.time * this.m_SwaySpeed + 100f) - 0.5f + this.m_TrackingBias;
			num3 *= -this.m_TrackingSwayAmount * this.m_FollowVelocity.x;
			num4 *= this.m_TrackingSwayAmount * this.m_FollowVelocity.y;
			base.transform.Rotate(num + num3, num2 + num4, 0f);
		}

		// Token: 0x04000235 RID: 565
		[SerializeField]
		private float m_SwaySpeed = 0.5f;

		// Token: 0x04000236 RID: 566
		[SerializeField]
		private float m_BaseSwayAmount = 0.5f;

		// Token: 0x04000237 RID: 567
		[SerializeField]
		private float m_TrackingSwayAmount = 0.5f;

		// Token: 0x04000238 RID: 568
		[Range(-1f, 1f)]
		[SerializeField]
		private float m_TrackingBias;
	}
}

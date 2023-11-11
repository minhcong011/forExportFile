using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
	// Token: 0x0200005F RID: 95
	public class Camera2DFollow : MonoBehaviour
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000C138 File Offset: 0x0000A338
		private void Start()
		{
			this.m_LastTargetPosition = this.target.position;
			this.m_OffsetZ = (base.transform.position - this.target.position).z;
			base.transform.parent = null;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000C188 File Offset: 0x0000A388
		private void Update()
		{
			float x = (this.target.position - this.m_LastTargetPosition).x;
			if (Mathf.Abs(x) > this.lookAheadMoveThreshold)
			{
				this.m_LookAheadPos = this.lookAheadFactor * Vector3.right * Mathf.Sign(x);
			}
			else
			{
				this.m_LookAheadPos = Vector3.MoveTowards(this.m_LookAheadPos, Vector3.zero, Time.deltaTime * this.lookAheadReturnSpeed);
			}
			Vector3 vector = this.target.position + this.m_LookAheadPos + Vector3.forward * this.m_OffsetZ;
			Vector3 position = Vector3.SmoothDamp(base.transform.position, vector, ref this.m_CurrentVelocity, this.damping);
			base.transform.position = position;
			this.m_LastTargetPosition = this.target.position;
		}

		// Token: 0x04000257 RID: 599
		public Transform target;

		// Token: 0x04000258 RID: 600
		public float damping = 1f;

		// Token: 0x04000259 RID: 601
		public float lookAheadFactor = 3f;

		// Token: 0x0400025A RID: 602
		public float lookAheadReturnSpeed = 0.5f;

		// Token: 0x0400025B RID: 603
		public float lookAheadMoveThreshold = 0.1f;

		// Token: 0x0400025C RID: 604
		private float m_OffsetZ;

		// Token: 0x0400025D RID: 605
		private Vector3 m_LastTargetPosition;

		// Token: 0x0400025E RID: 606
		private Vector3 m_CurrentVelocity;

		// Token: 0x0400025F RID: 607
		private Vector3 m_LookAheadPos;
	}
}

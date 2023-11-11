using System;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace UnityStandardAssets.Characters.FirstPerson
{
	// Token: 0x02000054 RID: 84
	public class HeadBob : MonoBehaviour
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x0000A923 File Offset: 0x00008B23
		private void Start()
		{
			this.motionBob.Setup(this.Camera, this.StrideInterval);
			this.m_OriginalCameraPosition = this.Camera.transform.localPosition;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A954 File Offset: 0x00008B54
		private void Update()
		{
			Vector3 localPosition;
			if (this.rigidbodyFirstPersonController.Velocity.magnitude > 0f && this.rigidbodyFirstPersonController.Grounded)
			{
				this.Camera.transform.localPosition = this.motionBob.DoHeadBob(this.rigidbodyFirstPersonController.Velocity.magnitude * (this.rigidbodyFirstPersonController.Running ? this.RunningStrideLengthen : 1f));
				localPosition = this.Camera.transform.localPosition;
				localPosition.y = this.Camera.transform.localPosition.y - this.jumpAndLandingBob.Offset();
			}
			else
			{
				localPosition = this.Camera.transform.localPosition;
				localPosition.y = this.m_OriginalCameraPosition.y - this.jumpAndLandingBob.Offset();
			}
			this.Camera.transform.localPosition = localPosition;
			if (!this.m_PreviouslyGrounded && this.rigidbodyFirstPersonController.Grounded)
			{
				base.StartCoroutine(this.jumpAndLandingBob.DoBobCycle());
			}
			this.m_PreviouslyGrounded = this.rigidbodyFirstPersonController.Grounded;
		}

		// Token: 0x040001F9 RID: 505
		public Camera Camera;

		// Token: 0x040001FA RID: 506
		public CurveControlledBob motionBob = new CurveControlledBob();

		// Token: 0x040001FB RID: 507
		public LerpControlledBob jumpAndLandingBob = new LerpControlledBob();

		// Token: 0x040001FC RID: 508
		public RigidbodyFirstPersonController rigidbodyFirstPersonController;

		// Token: 0x040001FD RID: 509
		public float StrideInterval;

		// Token: 0x040001FE RID: 510
		[Range(0f, 1f)]
		public float RunningStrideLengthen;

		// Token: 0x040001FF RID: 511
		private bool m_PreviouslyGrounded;

		// Token: 0x04000200 RID: 512
		private Vector3 m_OriginalCameraPosition;
	}
}

using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x0200004D RID: 77
	public class LandingGear : MonoBehaviour
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00009724 File Offset: 0x00007924
		private void Start()
		{
			this.m_Plane = base.GetComponent<AeroplaneController>();
			this.m_Animator = base.GetComponent<Animator>();
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000974C File Offset: 0x0000794C
		private void Update()
		{
			if (this.m_State == LandingGear.GearState.Lowered && this.m_Plane.Altitude > this.raiseAtAltitude && this.m_Rigidbody.velocity.y > 0f)
			{
				this.m_State = LandingGear.GearState.Raised;
			}
			if (this.m_State == LandingGear.GearState.Raised && this.m_Plane.Altitude < this.lowerAtAltitude && this.m_Rigidbody.velocity.y < 0f)
			{
				this.m_State = LandingGear.GearState.Lowered;
			}
			this.m_Animator.SetInteger("GearState", (int)this.m_State);
		}

		// Token: 0x040001AE RID: 430
		public float raiseAtAltitude = 40f;

		// Token: 0x040001AF RID: 431
		public float lowerAtAltitude = 40f;

		// Token: 0x040001B0 RID: 432
		private LandingGear.GearState m_State = LandingGear.GearState.Lowered;

		// Token: 0x040001B1 RID: 433
		private Animator m_Animator;

		// Token: 0x040001B2 RID: 434
		private Rigidbody m_Rigidbody;

		// Token: 0x040001B3 RID: 435
		private AeroplaneController m_Plane;

		// Token: 0x0200008A RID: 138
		private enum GearState
		{
			// Token: 0x04000300 RID: 768
			Raised = -1,
			// Token: 0x04000301 RID: 769
			Lowered = 1
		}
	}
}

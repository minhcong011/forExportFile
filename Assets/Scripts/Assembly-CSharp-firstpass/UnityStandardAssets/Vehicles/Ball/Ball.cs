using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
	// Token: 0x0200004E RID: 78
	public class Ball : MonoBehaviour
	{
		// Token: 0x060001AA RID: 426 RVA: 0x00009808 File Offset: 0x00007A08
		private void Start()
		{
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			base.GetComponent<Rigidbody>().maxAngularVelocity = this.m_MaxAngularVelocity;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00009828 File Offset: 0x00007A28
		public void Move(Vector3 moveDirection, bool jump)
		{
			if (this.m_UseTorque)
			{
				this.m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0f, -moveDirection.x) * this.m_MovePower);
			}
			else
			{
				this.m_Rigidbody.AddForce(moveDirection * this.m_MovePower);
			}
			if (Physics.Raycast(base.transform.position, -Vector3.up, 1f) && jump)
			{
				this.m_Rigidbody.AddForce(Vector3.up * this.m_JumpPower, ForceMode.Impulse);
			}
		}

		// Token: 0x040001B4 RID: 436
		[SerializeField]
		private float m_MovePower = 5f;

		// Token: 0x040001B5 RID: 437
		[SerializeField]
		private bool m_UseTorque = true;

		// Token: 0x040001B6 RID: 438
		[SerializeField]
		private float m_MaxAngularVelocity = 25f;

		// Token: 0x040001B7 RID: 439
		[SerializeField]
		private float m_JumpPower = 2f;

		// Token: 0x040001B8 RID: 440
		private const float k_GroundRayLength = 1f;

		// Token: 0x040001B9 RID: 441
		private Rigidbody m_Rigidbody;
	}
}

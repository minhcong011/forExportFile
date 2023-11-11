using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x0200003F RID: 63
	public class CarSelfRighting : MonoBehaviour
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00008027 File Offset: 0x00006227
		private void Start()
		{
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00008038 File Offset: 0x00006238
		private void Update()
		{
			if (base.transform.up.y > 0f || this.m_Rigidbody.velocity.magnitude > this.m_VelocityThreshold)
			{
				this.m_LastOkTime = Time.time;
			}
			if (Time.time > this.m_LastOkTime + this.m_WaitTime)
			{
				this.RightCar();
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000809C File Offset: 0x0000629C
		private void RightCar()
		{
			base.transform.position += Vector3.up;
			base.transform.rotation = Quaternion.LookRotation(base.transform.forward);
		}

		// Token: 0x04000145 RID: 325
		[SerializeField]
		private float m_WaitTime = 3f;

		// Token: 0x04000146 RID: 326
		[SerializeField]
		private float m_VelocityThreshold = 1f;

		// Token: 0x04000147 RID: 327
		private float m_LastOkTime;

		// Token: 0x04000148 RID: 328
		private Rigidbody m_Rigidbody;
	}
}

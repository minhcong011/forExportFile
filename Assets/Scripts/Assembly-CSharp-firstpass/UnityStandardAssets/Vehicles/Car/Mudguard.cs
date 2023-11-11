using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x02000041 RID: 65
	public class Mudguard : MonoBehaviour
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00008136 File Offset: 0x00006336
		private void Start()
		{
			this.m_OriginalRotation = base.transform.localRotation;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008149 File Offset: 0x00006349
		private void Update()
		{
			base.transform.localRotation = this.m_OriginalRotation * Quaternion.Euler(0f, this.carController.CurrentSteerAngle, 0f);
		}

		// Token: 0x0400014A RID: 330
		public CarController carController;

		// Token: 0x0400014B RID: 331
		private Quaternion m_OriginalRotation;
	}
}

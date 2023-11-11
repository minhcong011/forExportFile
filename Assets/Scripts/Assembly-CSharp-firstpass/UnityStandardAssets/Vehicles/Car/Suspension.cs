using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x02000043 RID: 67
	public class Suspension : MonoBehaviour
	{
		// Token: 0x06000158 RID: 344 RVA: 0x0000818A File Offset: 0x0000638A
		private void Start()
		{
			this.m_TargetOriginalPosition = this.wheel.transform.localPosition;
			this.m_Origin = base.transform.localPosition;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000081B3 File Offset: 0x000063B3
		private void Update()
		{
			base.transform.localPosition = this.m_Origin + (this.wheel.transform.localPosition - this.m_TargetOriginalPosition);
		}

		// Token: 0x0400014D RID: 333
		public GameObject wheel;

		// Token: 0x0400014E RID: 334
		private Vector3 m_TargetOriginalPosition;

		// Token: 0x0400014F RID: 335
		private Vector3 m_Origin;
	}
}

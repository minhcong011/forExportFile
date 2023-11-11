using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x02000039 RID: 57
	public class BrakeLight : MonoBehaviour
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00006DB4 File Offset: 0x00004FB4
		private void Start()
		{
			this.m_Renderer = base.GetComponent<Renderer>();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006DC2 File Offset: 0x00004FC2
		private void Update()
		{
			this.m_Renderer.enabled = (this.car.BrakeInput > 0f);
		}

		// Token: 0x040000F4 RID: 244
		public CarController car;

		// Token: 0x040000F5 RID: 245
		private Renderer m_Renderer;
	}
}

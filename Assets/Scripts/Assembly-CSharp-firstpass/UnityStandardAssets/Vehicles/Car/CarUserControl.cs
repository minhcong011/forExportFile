using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x02000040 RID: 64
	[RequireComponent(typeof(CarController))]
	public class CarUserControl : MonoBehaviour
	{
		// Token: 0x06000150 RID: 336 RVA: 0x000080F2 File Offset: 0x000062F2
		private void Awake()
		{
			this.m_Car = base.GetComponent<CarController>();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008100 File Offset: 0x00006300
		private void FixedUpdate()
		{
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			float axis2 = CrossPlatformInputManager.GetAxis("Vertical");
			this.m_Car.Move(axis, axis2, axis2, 0f);
		}

		// Token: 0x04000149 RID: 329
		private CarController m_Car;
	}
}

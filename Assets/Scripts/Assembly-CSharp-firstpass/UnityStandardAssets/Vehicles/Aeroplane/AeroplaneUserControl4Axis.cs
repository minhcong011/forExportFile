using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x0200004B RID: 75
	[RequireComponent(typeof(AeroplaneController))]
	public class AeroplaneUserControl4Axis : MonoBehaviour
	{
		// Token: 0x0600019F RID: 415 RVA: 0x000094A4 File Offset: 0x000076A4
		private void Awake()
		{
			this.m_Aeroplane = base.GetComponent<AeroplaneController>();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000094B4 File Offset: 0x000076B4
		private void FixedUpdate()
		{
			float axis = CrossPlatformInputManager.GetAxis("Mouse X");
			float axis2 = CrossPlatformInputManager.GetAxis("Mouse Y");
			this.m_AirBrakes = CrossPlatformInputManager.GetButton("Fire1");
			this.m_Yaw = CrossPlatformInputManager.GetAxis("Horizontal");
			this.m_Throttle = CrossPlatformInputManager.GetAxis("Vertical");
			this.AdjustInputForMobileControls(ref axis, ref axis2, ref this.m_Throttle);
			this.m_Aeroplane.Move(axis, axis2, this.m_Yaw, this.m_Throttle, this.m_AirBrakes);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009538 File Offset: 0x00007738
		private void AdjustInputForMobileControls(ref float roll, ref float pitch, ref float throttle)
		{
			float num = roll * this.maxRollAngle * 0.0174532924f;
			float num2 = pitch * this.maxPitchAngle * 0.0174532924f;
			roll = Mathf.Clamp(num - this.m_Aeroplane.RollAngle, -1f, 1f);
			pitch = Mathf.Clamp(num2 - this.m_Aeroplane.PitchAngle, -1f, 1f);
		}

		// Token: 0x040001A2 RID: 418
		public float maxRollAngle = 80f;

		// Token: 0x040001A3 RID: 419
		public float maxPitchAngle = 80f;

		// Token: 0x040001A4 RID: 420
		private AeroplaneController m_Aeroplane;

		// Token: 0x040001A5 RID: 421
		private float m_Throttle;

		// Token: 0x040001A6 RID: 422
		private bool m_AirBrakes;

		// Token: 0x040001A7 RID: 423
		private float m_Yaw;
	}
}

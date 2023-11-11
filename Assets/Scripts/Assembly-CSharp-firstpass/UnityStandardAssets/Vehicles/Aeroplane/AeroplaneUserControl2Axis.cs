﻿using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x0200004A RID: 74
	[RequireComponent(typeof(AeroplaneController))]
	public class AeroplaneUserControl2Axis : MonoBehaviour
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00009389 File Offset: 0x00007589
		private void Awake()
		{
			this.m_Aeroplane = base.GetComponent<AeroplaneController>();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00009398 File Offset: 0x00007598
		private void FixedUpdate()
		{
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			float axis2 = CrossPlatformInputManager.GetAxis("Vertical");
			bool button = CrossPlatformInputManager.GetButton("Fire1");
			float throttleInput = (float)(button ? -1 : 1);
			this.AdjustInputForMobileControls(ref axis, ref axis2, ref throttleInput);
			this.m_Aeroplane.Move(axis, axis2, 0f, throttleInput, button);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000093F0 File Offset: 0x000075F0
		private void AdjustInputForMobileControls(ref float roll, ref float pitch, ref float throttle)
		{
			float num = roll * this.maxRollAngle * 0.0174532924f;
			float num2 = pitch * this.maxPitchAngle * 0.0174532924f;
			roll = Mathf.Clamp(num - this.m_Aeroplane.RollAngle, -1f, 1f);
			pitch = Mathf.Clamp(num2 - this.m_Aeroplane.PitchAngle, -1f, 1f);
			float num3 = throttle * 0.5f + 0.5f;
			throttle = Mathf.Clamp(num3 - this.m_Aeroplane.Throttle, -1f, 1f);
		}

		// Token: 0x0400019F RID: 415
		public float maxRollAngle = 80f;

		// Token: 0x040001A0 RID: 416
		public float maxPitchAngle = 80f;

		// Token: 0x040001A1 RID: 417
		private AeroplaneController m_Aeroplane;
	}
}

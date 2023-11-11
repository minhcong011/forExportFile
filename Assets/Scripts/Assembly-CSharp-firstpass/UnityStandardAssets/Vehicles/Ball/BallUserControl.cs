using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Ball
{
	// Token: 0x0200004F RID: 79
	public class BallUserControl : MonoBehaviour
	{
		// Token: 0x060001AD RID: 429 RVA: 0x000098F2 File Offset: 0x00007AF2
		private void Awake()
		{
			this.ball = base.GetComponent<Ball>();
			if (Camera.main != null)
			{
				this.cam = Camera.main.transform;
				return;
			}
			Debug.LogWarning("Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00009928 File Offset: 0x00007B28
		private void Update()
		{
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			float axis2 = CrossPlatformInputManager.GetAxis("Vertical");
			this.jump = CrossPlatformInputManager.GetButton("Jump");
			if (this.cam != null)
			{
				this.camForward = Vector3.Scale(this.cam.forward, new Vector3(1f, 0f, 1f)).normalized;
				this.move = (axis2 * this.camForward + axis * this.cam.right).normalized;
				return;
			}
			this.move = (axis2 * Vector3.forward + axis * Vector3.right).normalized;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000099F5 File Offset: 0x00007BF5
		private void FixedUpdate()
		{
			this.ball.Move(this.move, this.jump);
			this.jump = false;
		}

		// Token: 0x040001BA RID: 442
		private Ball ball;

		// Token: 0x040001BB RID: 443
		private Vector3 move;

		// Token: 0x040001BC RID: 444
		private Transform cam;

		// Token: 0x040001BD RID: 445
		private Vector3 camForward;

		// Token: 0x040001BE RID: 446
		private bool jump;
	}
}

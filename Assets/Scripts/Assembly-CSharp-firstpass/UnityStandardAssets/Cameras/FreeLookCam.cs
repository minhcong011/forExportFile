using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Cameras
{
	// Token: 0x02000059 RID: 89
	public class FreeLookCam : PivotBasedCameraRig
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x0000B6A0 File Offset: 0x000098A0
		protected override void Awake()
		{
			base.Awake();
			Cursor.lockState = (this.m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None);
			Cursor.visible = !this.m_LockCursor;
			this.m_PivotEulers = this.m_Pivot.rotation.eulerAngles;
			this.m_PivotTargetRot = this.m_Pivot.transform.localRotation;
			this.m_TransformTargetRot = base.transform.localRotation;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000B712 File Offset: 0x00009912
		protected void Update()
		{
			this.HandleRotationMovement();
			if (this.m_LockCursor && Input.GetMouseButtonUp(0))
			{
				Cursor.lockState = (this.m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None);
				Cursor.visible = !this.m_LockCursor;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B749 File Offset: 0x00009949
		private void OnDisable()
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000B757 File Offset: 0x00009957
		protected override void FollowTarget(float deltaTime)
		{
			if (this.m_Target == null)
			{
				return;
			}
			base.transform.position = Vector3.Lerp(base.transform.position, this.m_Target.position, deltaTime * this.m_MoveSpeed);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000B798 File Offset: 0x00009998
		private void HandleRotationMovement()
		{
			if (Time.timeScale < 1.401298E-45f)
			{
				return;
			}
			float axis = CrossPlatformInputManager.GetAxis("Mouse X");
			float axis2 = CrossPlatformInputManager.GetAxis("Mouse Y");
			this.m_LookAngle += axis * this.m_TurnSpeed;
			this.m_TransformTargetRot = Quaternion.Euler(0f, this.m_LookAngle, 0f);
			if (this.m_VerticalAutoReturn)
			{
				this.m_TiltAngle = ((axis2 > 0f) ? Mathf.Lerp(0f, -this.m_TiltMin, axis2) : Mathf.Lerp(0f, this.m_TiltMax, -axis2));
			}
			else
			{
				this.m_TiltAngle -= axis2 * this.m_TurnSpeed;
				this.m_TiltAngle = Mathf.Clamp(this.m_TiltAngle, -this.m_TiltMin, this.m_TiltMax);
			}
			this.m_PivotTargetRot = Quaternion.Euler(this.m_TiltAngle, this.m_PivotEulers.y, this.m_PivotEulers.z);
			if (this.m_TurnSmoothing > 0f)
			{
				this.m_Pivot.localRotation = Quaternion.Slerp(this.m_Pivot.localRotation, this.m_PivotTargetRot, this.m_TurnSmoothing * Time.deltaTime);
				base.transform.localRotation = Quaternion.Slerp(base.transform.localRotation, this.m_TransformTargetRot, this.m_TurnSmoothing * Time.deltaTime);
				return;
			}
			this.m_Pivot.localRotation = this.m_PivotTargetRot;
			base.transform.localRotation = this.m_TransformTargetRot;
		}

		// Token: 0x04000228 RID: 552
		[SerializeField]
		private float m_MoveSpeed = 1f;

		// Token: 0x04000229 RID: 553
		[Range(0f, 10f)]
		[SerializeField]
		private float m_TurnSpeed = 1.5f;

		// Token: 0x0400022A RID: 554
		[SerializeField]
		private float m_TurnSmoothing;

		// Token: 0x0400022B RID: 555
		[SerializeField]
		private float m_TiltMax = 75f;

		// Token: 0x0400022C RID: 556
		[SerializeField]
		private float m_TiltMin = 45f;

		// Token: 0x0400022D RID: 557
		[SerializeField]
		private bool m_LockCursor;

		// Token: 0x0400022E RID: 558
		[SerializeField]
		private bool m_VerticalAutoReturn;

		// Token: 0x0400022F RID: 559
		private float m_LookAngle;

		// Token: 0x04000230 RID: 560
		private float m_TiltAngle;

		// Token: 0x04000231 RID: 561
		private const float k_LookDistance = 100f;

		// Token: 0x04000232 RID: 562
		private Vector3 m_PivotEulers;

		// Token: 0x04000233 RID: 563
		private Quaternion m_PivotTargetRot;

		// Token: 0x04000234 RID: 564
		private Quaternion m_TransformTargetRot;
	}
}

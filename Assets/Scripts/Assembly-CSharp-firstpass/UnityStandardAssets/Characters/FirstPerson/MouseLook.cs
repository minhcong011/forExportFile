using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	public class MouseLook
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		public void Init(Transform character, Transform camera)
		{
			this.m_CharacterTargetRot = character.localRotation;
			this.m_CameraTargetRot = camera.localRotation;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		public void LookRotation(Transform character, Transform camera)
		{
			float y = CrossPlatformInputManager.GetAxis("Mouse X") * this.XSensitivity;
			float num = CrossPlatformInputManager.GetAxis("Mouse Y") * this.YSensitivity;
			this.m_CharacterTargetRot *= Quaternion.Euler(0f, y, 0f);
			this.m_CameraTargetRot *= Quaternion.Euler(-num, 0f, 0f);
			if (this.clampVerticalRotation)
			{
				this.m_CameraTargetRot = this.ClampRotationAroundXAxis(this.m_CameraTargetRot);
			}
			if (this.smooth)
			{
				character.localRotation = Quaternion.Slerp(character.localRotation, this.m_CharacterTargetRot, this.smoothTime * Time.deltaTime);
				camera.localRotation = Quaternion.Slerp(camera.localRotation, this.m_CameraTargetRot, this.smoothTime * Time.deltaTime);
			}
			else
			{
				character.localRotation = this.m_CharacterTargetRot;
				camera.localRotation = this.m_CameraTargetRot;
			}
			this.UpdateCursorLock();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		public void SetCursorLock(bool value)
		{
			this.lockCursor = value;
			if (!this.lockCursor)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000ABDD File Offset: 0x00008DDD
		public void UpdateCursorLock()
		{
			if (this.lockCursor)
			{
				this.InternalLockUpdate();
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		private void InternalLockUpdate()
		{
			if (Input.GetKeyUp(KeyCode.Escape))
			{
				this.m_cursorIsLocked = false;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				this.m_cursorIsLocked = true;
			}
			if (this.m_cursorIsLocked)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				return;
			}
			if (!this.m_cursorIsLocked)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000AC48 File Offset: 0x00008E48
		private Quaternion ClampRotationAroundXAxis(Quaternion q)
		{
			q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1f;
			float num = 114.59156f * Mathf.Atan(q.x);
			num = Mathf.Clamp(num, this.MinimumX, this.MaximumX);
			q.x = Mathf.Tan(0.008726646f * num);
			return q;
		}

		// Token: 0x04000201 RID: 513
		public float XSensitivity = 2f;

		// Token: 0x04000202 RID: 514
		public float YSensitivity = 2f;

		// Token: 0x04000203 RID: 515
		public bool clampVerticalRotation = true;

		// Token: 0x04000204 RID: 516
		public float MinimumX = -90f;

		// Token: 0x04000205 RID: 517
		public float MaximumX = 90f;

		// Token: 0x04000206 RID: 518
		public bool smooth;

		// Token: 0x04000207 RID: 519
		public float smoothTime = 5f;

		// Token: 0x04000208 RID: 520
		public bool lockCursor = true;

		// Token: 0x04000209 RID: 521
		private Quaternion m_CharacterTargetRot;

		// Token: 0x0400020A RID: 522
		private Quaternion m_CameraTargetRot;

		// Token: 0x0400020B RID: 523
		private bool m_cursorIsLocked = true;
	}
}

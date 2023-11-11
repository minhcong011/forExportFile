using System;
using UnityEngine;

// Token: 0x02000071 RID: 113
[Serializable]
public class MouseLook
{
	// Token: 0x060001EC RID: 492 RVA: 0x0000E70C File Offset: 0x0000C90C
	public void Init(Transform character, Transform camera)
	{
		this.m_CharacterTargetRot = character.localRotation;
		this.m_CameraTargetRot = camera.localRotation;
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000E728 File Offset: 0x0000C928
	public void LookRotation(Transform character, Transform camera)
	{
		if (VariblesGlobal.MouseLookControll)
		{
			float y = this.LookAxis.x * VariblesGlobal.Sensitivity;
			float num = this.LookAxis.y * VariblesGlobal.Sensitivity;
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
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000E82E File Offset: 0x0000CA2E
	public void SetCursorLock(bool value)
	{
		this.lockCursor = value;
		if (!this.lockCursor)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000E84B File Offset: 0x0000CA4B
	public void UpdateCursorLock()
	{
		if (this.lockCursor)
		{
			this.InternalLockUpdate();
		}
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000E85C File Offset: 0x0000CA5C
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

	// Token: 0x060001F1 RID: 497 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
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

	// Token: 0x04000226 RID: 550
	public bool clampVerticalRotation = true;

	// Token: 0x04000227 RID: 551
	public float MinimumX = -90f;

	// Token: 0x04000228 RID: 552
	public float MaximumX = 90f;

	// Token: 0x04000229 RID: 553
	public bool smooth;

	// Token: 0x0400022A RID: 554
	public float smoothTime = 5f;

	// Token: 0x0400022B RID: 555
	public bool lockCursor = true;

	// Token: 0x0400022C RID: 556
	private Quaternion m_CharacterTargetRot;

	// Token: 0x0400022D RID: 557
	private Quaternion m_CameraTargetRot;

	// Token: 0x0400022E RID: 558
	private bool m_cursorIsLocked = true;

	// Token: 0x0400022F RID: 559
	[HideInInspector]
	public Vector2 LookAxis;

	// Token: 0x04000230 RID: 560
	private float CameraY;
}

using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	// Token: 0x02000052 RID: 82
	[RequireComponent(typeof(ThirdPersonCharacter))]
	public class ThirdPersonUserControl : MonoBehaviour
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x0000A12F File Offset: 0x0000832F
		private void Start()
		{
			if (Camera.main != null)
			{
				this.m_Cam = Camera.main.transform;
			}
			else
			{
				Debug.LogWarning("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", base.gameObject);
			}
			this.m_Character = base.GetComponent<ThirdPersonCharacter>();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000A16C File Offset: 0x0000836C
		private void Update()
		{
			if (!this.m_Jump)
			{
				this.m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000A188 File Offset: 0x00008388
		private void FixedUpdate()
		{
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			float axis2 = CrossPlatformInputManager.GetAxis("Vertical");
			bool key = Input.GetKey(KeyCode.C);
			if (this.m_Cam != null)
			{
				this.m_CamForward = Vector3.Scale(this.m_Cam.forward, new Vector3(1f, 0f, 1f)).normalized;
				this.m_Move = axis2 * this.m_CamForward + axis * this.m_Cam.right;
			}
			else
			{
				this.m_Move = axis2 * Vector3.forward + axis * Vector3.right;
			}
			this.m_Character.Move(this.m_Move, key, this.m_Jump);
			this.m_Jump = false;
		}

		// Token: 0x040001D6 RID: 470
		private ThirdPersonCharacter m_Character;

		// Token: 0x040001D7 RID: 471
		private Transform m_Cam;

		// Token: 0x040001D8 RID: 472
		private Vector3 m_CamForward;

		// Token: 0x040001D9 RID: 473
		private Vector3 m_Move;

		// Token: 0x040001DA RID: 474
		private bool m_Jump;
	}
}

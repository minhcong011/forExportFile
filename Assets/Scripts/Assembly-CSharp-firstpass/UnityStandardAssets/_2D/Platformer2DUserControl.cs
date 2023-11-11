using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
	// Token: 0x02000061 RID: 97
	[RequireComponent(typeof(PlatformerCharacter2D))]
	public class Platformer2DUserControl : MonoBehaviour
	{
		// Token: 0x06000216 RID: 534 RVA: 0x0000C453 File Offset: 0x0000A653
		private void Awake()
		{
			this.m_Character = base.GetComponent<PlatformerCharacter2D>();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000C461 File Offset: 0x0000A661
		private void Update()
		{
			if (!this.m_Jump)
			{
				this.m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000C47C File Offset: 0x0000A67C
		private void FixedUpdate()
		{
			bool key = Input.GetKey(KeyCode.LeftControl);
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			this.m_Character.Move(axis, key, this.m_Jump);
			this.m_Jump = false;
		}

		// Token: 0x04000267 RID: 615
		private PlatformerCharacter2D m_Character;

		// Token: 0x04000268 RID: 616
		private bool m_Jump;
	}
}

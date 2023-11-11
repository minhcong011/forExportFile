using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput.PlatformSpecific
{
	// Token: 0x02000038 RID: 56
	public class StandaloneInput : VirtualInput
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00006D77 File Offset: 0x00004F77
		public override float GetAxis(string name, bool raw)
		{
			if (!raw)
			{
				return Input.GetAxis(name);
			}
			return Input.GetAxisRaw(name);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006D89 File Offset: 0x00004F89
		public override bool GetButton(string name)
		{
			return Input.GetButton(name);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006D91 File Offset: 0x00004F91
		public override bool GetButtonDown(string name)
		{
			return Input.GetButtonDown(name);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006D99 File Offset: 0x00004F99
		public override bool GetButtonUp(string name)
		{
			return Input.GetButtonUp(name);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006DA1 File Offset: 0x00004FA1
		public override void SetButtonDown(string name)
		{
			throw new Exception(" This is not possible to be called for standalone input. Please check your platform and code where this is called");
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006DA1 File Offset: 0x00004FA1
		public override void SetButtonUp(string name)
		{
			throw new Exception(" This is not possible to be called for standalone input. Please check your platform and code where this is called");
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006DA1 File Offset: 0x00004FA1
		public override void SetAxisPositive(string name)
		{
			throw new Exception(" This is not possible to be called for standalone input. Please check your platform and code where this is called");
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006DA1 File Offset: 0x00004FA1
		public override void SetAxisNegative(string name)
		{
			throw new Exception(" This is not possible to be called for standalone input. Please check your platform and code where this is called");
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006DA1 File Offset: 0x00004FA1
		public override void SetAxisZero(string name)
		{
			throw new Exception(" This is not possible to be called for standalone input. Please check your platform and code where this is called");
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006DA1 File Offset: 0x00004FA1
		public override void SetAxis(string name, float value)
		{
			throw new Exception(" This is not possible to be called for standalone input. Please check your platform and code where this is called");
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006DAD File Offset: 0x00004FAD
		public override Vector3 MousePosition()
		{
			return Input.mousePosition;
		}
	}
}

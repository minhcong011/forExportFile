using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x02000031 RID: 49
	public class InputAxisScrollbar : MonoBehaviour
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x000033A1 File Offset: 0x000015A1
		private void Update()
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000627E File Offset: 0x0000447E
		public void HandleInput(float value)
		{
			CrossPlatformInputManager.SetAxis(this.axis, value * 2f - 1f);
		}

		// Token: 0x040000CF RID: 207
		public string axis;
	}
}

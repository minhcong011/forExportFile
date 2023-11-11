using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x0200002F RID: 47
	public class ButtonHandler : MonoBehaviour
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000033A1 File Offset: 0x000015A1
		private void OnEnable()
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000060CC File Offset: 0x000042CC
		public void SetDownState()
		{
			CrossPlatformInputManager.SetButtonDown(this.Name);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000060D9 File Offset: 0x000042D9
		public void SetUpState()
		{
			CrossPlatformInputManager.SetButtonUp(this.Name);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000060E6 File Offset: 0x000042E6
		public void SetAxisPositiveState()
		{
			CrossPlatformInputManager.SetAxisPositive(this.Name);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000060F3 File Offset: 0x000042F3
		public void SetAxisNeutralState()
		{
			CrossPlatformInputManager.SetAxisZero(this.Name);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006100 File Offset: 0x00004300
		public void SetAxisNegativeState()
		{
			CrossPlatformInputManager.SetAxisNegative(this.Name);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000033A1 File Offset: 0x000015A1
		public void Update()
		{
		}

		// Token: 0x040000CB RID: 203
		public string Name;
	}
}

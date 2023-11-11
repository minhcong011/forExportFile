using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class LerpControlledBob
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002D81 File Offset: 0x00000F81
		public float Offset()
		{
			return this.m_Offset;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002D89 File Offset: 0x00000F89
		public IEnumerator DoBobCycle()
		{
			float t = 0f;
			while (t < this.BobDuration)
			{
				this.m_Offset = Mathf.Lerp(0f, this.BobAmount, t / this.BobDuration);
				t += Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}
			t = 0f;
			while (t < this.BobDuration)
			{
				this.m_Offset = Mathf.Lerp(this.BobAmount, 0f, t / this.BobDuration);
				t += Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}
			this.m_Offset = 0f;
			yield break;
		}

		// Token: 0x0400003A RID: 58
		public float BobDuration;

		// Token: 0x0400003B RID: 59
		public float BobAmount;

		// Token: 0x0400003C RID: 60
		private float m_Offset;
	}
}

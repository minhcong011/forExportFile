using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000007 RID: 7
	public class AutoMoveAndRotate : MonoBehaviour
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000261B File Offset: 0x0000081B
		private void Start()
		{
			this.m_LastRealTime = Time.realtimeSinceStartup;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002628 File Offset: 0x00000828
		private void Update()
		{
			float d = Time.deltaTime;
			if (this.ignoreTimescale)
			{
				d = Time.realtimeSinceStartup - this.m_LastRealTime;
				this.m_LastRealTime = Time.realtimeSinceStartup;
			}
			base.transform.Translate(this.moveUnitsPerSecond.value * d, this.moveUnitsPerSecond.space);
			base.transform.Rotate(this.rotateDegreesPerSecond.value * d, this.moveUnitsPerSecond.space);
		}

		// Token: 0x04000008 RID: 8
		public AutoMoveAndRotate.Vector3andSpace moveUnitsPerSecond;

		// Token: 0x04000009 RID: 9
		public AutoMoveAndRotate.Vector3andSpace rotateDegreesPerSecond;

		// Token: 0x0400000A RID: 10
		public bool ignoreTimescale;

		// Token: 0x0400000B RID: 11
		private float m_LastRealTime;

		// Token: 0x02000067 RID: 103
		[Serializable]
		public class Vector3andSpace
		{
			// Token: 0x04000280 RID: 640
			public Vector3 value;

			// Token: 0x04000281 RID: 641
			public Space space = Space.Self;
		}
	}
}

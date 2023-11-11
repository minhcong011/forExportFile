using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000017 RID: 23
	public class TimedObjectDestructor : MonoBehaviour
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000035B3 File Offset: 0x000017B3
		private void Awake()
		{
			base.Invoke("DestroyNow", this.m_TimeOut);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000035C6 File Offset: 0x000017C6
		private void DestroyNow()
		{
			if (this.m_DetachChildren)
			{
				base.transform.DetachChildren();
			}
			Object.DestroyObject(base.gameObject);
		}

		// Token: 0x0400005C RID: 92
		[SerializeField]
		private float m_TimeOut = 1f;

		// Token: 0x0400005D RID: 93
		[SerializeField]
		private bool m_DetachChildren;
	}
}

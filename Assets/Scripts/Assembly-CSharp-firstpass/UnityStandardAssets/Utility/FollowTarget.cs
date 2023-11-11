using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200000C RID: 12
	public class FollowTarget : MonoBehaviour
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002C13 File Offset: 0x00000E13
		private void LateUpdate()
		{
			base.transform.position = this.target.position + this.offset;
		}

		// Token: 0x0400002C RID: 44
		public Transform target;

		// Token: 0x0400002D RID: 45
		public Vector3 offset = new Vector3(0f, 7.5f, 0f);
	}
}

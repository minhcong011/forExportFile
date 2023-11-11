using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000008 RID: 8
	public class CameraRefocus
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000026A9 File Offset: 0x000008A9
		public CameraRefocus(Camera camera, Transform parent, Vector3 origCameraPos)
		{
			this.m_OrigCameraPos = origCameraPos;
			this.Camera = camera;
			this.Parent = parent;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026C6 File Offset: 0x000008C6
		public void ChangeCamera(Camera camera)
		{
			this.Camera = camera;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000026CF File Offset: 0x000008CF
		public void ChangeParent(Transform parent)
		{
			this.Parent = parent;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026D8 File Offset: 0x000008D8
		public void GetFocusPoint()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(this.Parent.transform.position + this.m_OrigCameraPos, this.Parent.transform.forward, out raycastHit, 100f))
			{
				this.Lookatpoint = raycastHit.point;
				this.m_Refocus = true;
				return;
			}
			this.m_Refocus = false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000273A File Offset: 0x0000093A
		public void SetFocusPoint()
		{
			if (this.m_Refocus)
			{
				this.Camera.transform.LookAt(this.Lookatpoint);
			}
		}

		// Token: 0x0400000C RID: 12
		public Camera Camera;

		// Token: 0x0400000D RID: 13
		public Vector3 Lookatpoint;

		// Token: 0x0400000E RID: 14
		public Transform Parent;

		// Token: 0x0400000F RID: 15
		private Vector3 m_OrigCameraPos;

		// Token: 0x04000010 RID: 16
		private bool m_Refocus;
	}
}

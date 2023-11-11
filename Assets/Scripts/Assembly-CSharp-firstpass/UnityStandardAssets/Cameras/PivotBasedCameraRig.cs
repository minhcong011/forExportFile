using System;
using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	// Token: 0x0200005C RID: 92
	public abstract class PivotBasedCameraRig : AbstractTargetFollower
	{
		// Token: 0x06000201 RID: 513 RVA: 0x0000BC27 File Offset: 0x00009E27
		protected virtual void Awake()
		{
			this.m_Cam = base.GetComponentInChildren<Camera>().transform;
			this.m_Pivot = this.m_Cam.parent;
		}

		// Token: 0x0400023E RID: 574
		protected Transform m_Cam;

		// Token: 0x0400023F RID: 575
		protected Transform m_Pivot;

		// Token: 0x04000240 RID: 576
		protected Vector3 m_LastTargetPosition;
	}
}

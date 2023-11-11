using System;
using UnityEngine;

namespace UnityStandardAssets.SceneUtils
{
	// Token: 0x020000B1 RID: 177
	public class PlaceTargetWithMouse : MonoBehaviour
	{
		// Token: 0x06000337 RID: 823 RVA: 0x00014144 File Offset: 0x00012344
		private void Update()
		{
			if (!Input.GetMouseButtonDown(0))
			{
				return;
			}
			RaycastHit raycastHit;
			if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit))
			{
				return;
			}
			base.transform.position = raycastHit.point + raycastHit.normal * this.surfaceOffset;
			if (this.setTargetOn != null)
			{
				this.setTargetOn.SendMessage("SetTarget", base.transform);
			}
		}

		// Token: 0x04000378 RID: 888
		public float surfaceOffset = 1.5f;

		// Token: 0x04000379 RID: 889
		public GameObject setTargetOn;
	}
}

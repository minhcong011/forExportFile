using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200000A RID: 10
	public class DragRigidbody : MonoBehaviour
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000293C File Offset: 0x00000B3C
		private void Update()
		{
			if (!Input.GetMouseButtonDown(0))
			{
				return;
			}
			Camera camera = this.FindCamera();
			RaycastHit raycastHit = default(RaycastHit);
			if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition).origin, camera.ScreenPointToRay(Input.mousePosition).direction, out raycastHit, 100f, -5))
			{
				return;
			}
			if (!raycastHit.rigidbody || raycastHit.rigidbody.isKinematic)
			{
				return;
			}
			if (!this.m_SpringJoint)
			{
				GameObject gameObject = new GameObject("Rigidbody dragger");
				Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
				this.m_SpringJoint = gameObject.AddComponent<SpringJoint>();
				rigidbody.isKinematic = true;
			}
			this.m_SpringJoint.transform.position = raycastHit.point;
			this.m_SpringJoint.anchor = Vector3.zero;
			this.m_SpringJoint.spring = 50f;
			this.m_SpringJoint.damper = 5f;
			this.m_SpringJoint.maxDistance = 0.2f;
			this.m_SpringJoint.connectedBody = raycastHit.rigidbody;
			base.StartCoroutine("DragObject", raycastHit.distance);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A66 File Offset: 0x00000C66
		private IEnumerator DragObject(float distance)
		{
			float oldDrag = this.m_SpringJoint.connectedBody.drag;
			float oldAngularDrag = this.m_SpringJoint.connectedBody.angularDrag;
			this.m_SpringJoint.connectedBody.drag = 10f;
			this.m_SpringJoint.connectedBody.angularDrag = 5f;
			Camera mainCamera = this.FindCamera();
			while (Input.GetMouseButton(0))
			{
				Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
				this.m_SpringJoint.transform.position = ray.GetPoint(distance);
				yield return null;
			}
			if (this.m_SpringJoint.connectedBody)
			{
				this.m_SpringJoint.connectedBody.drag = oldDrag;
				this.m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
				this.m_SpringJoint.connectedBody = null;
			}
			yield break;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002A7C File Offset: 0x00000C7C
		private Camera FindCamera()
		{
			if (base.GetComponent<Camera>())
			{
				return base.GetComponent<Camera>();
			}
			return Camera.main;
		}

		// Token: 0x0400001A RID: 26
		private const float k_Spring = 50f;

		// Token: 0x0400001B RID: 27
		private const float k_Damper = 5f;

		// Token: 0x0400001C RID: 28
		private const float k_Drag = 10f;

		// Token: 0x0400001D RID: 29
		private const float k_AngularDrag = 5f;

		// Token: 0x0400001E RID: 30
		private const float k_Distance = 0.2f;

		// Token: 0x0400001F RID: 31
		private const bool k_AttachToCenterOfMass = false;

		// Token: 0x04000020 RID: 32
		private SpringJoint m_SpringJoint;
	}
}

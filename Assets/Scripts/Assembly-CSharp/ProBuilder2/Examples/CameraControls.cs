using System;
using UnityEngine;

namespace ProBuilder2.Examples
{
	// Token: 0x020000B3 RID: 179
	public class CameraControls : MonoBehaviour
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0001427F File Offset: 0x0001247F
		private void Start()
		{
			this.distance = Vector3.Distance(base.transform.position, Vector3.zero);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001429C File Offset: 0x0001249C
		private void LateUpdate()
		{
			Vector3 eulerAngles = base.transform.localRotation.eulerAngles;
			eulerAngles.z = 0f;
			if (Input.GetMouseButton(0))
			{
				float axis = Input.GetAxis("Mouse X");
				float num = -Input.GetAxis("Mouse Y");
				eulerAngles.x += num * this.orbitSpeed;
				eulerAngles.y += axis * this.orbitSpeed;
				this.dir.x = axis;
				this.dir.y = num;
				this.dir.Normalize();
			}
			else
			{
				eulerAngles.y += Time.deltaTime * this.idleRotation * this.dir.x;
				eulerAngles.x += Time.deltaTime * Mathf.PerlinNoise(Time.time, 0f) * this.idleRotation * this.dir.y;
			}
			base.transform.localRotation = Quaternion.Euler(eulerAngles);
			base.transform.position = base.transform.localRotation * (Vector3.forward * -this.distance);
			if (Input.GetAxis("Mouse ScrollWheel") != 0f)
			{
				float axis2 = Input.GetAxis("Mouse ScrollWheel");
				this.distance -= axis2 * (this.distance / 40f) * (this.zoomSpeed * 1000f) * Time.deltaTime;
				this.distance = Mathf.Clamp(this.distance, 10f, 40f);
				base.transform.position = base.transform.localRotation * (Vector3.forward * -this.distance);
			}
		}

		// Token: 0x04000380 RID: 896
		private const string INPUT_MOUSE_SCROLLWHEEL = "Mouse ScrollWheel";

		// Token: 0x04000381 RID: 897
		private const string INPUT_MOUSE_X = "Mouse X";

		// Token: 0x04000382 RID: 898
		private const string INPUT_MOUSE_Y = "Mouse Y";

		// Token: 0x04000383 RID: 899
		private const float MIN_CAM_DISTANCE = 10f;

		// Token: 0x04000384 RID: 900
		private const float MAX_CAM_DISTANCE = 40f;

		// Token: 0x04000385 RID: 901
		[Range(2f, 15f)]
		public float orbitSpeed = 6f;

		// Token: 0x04000386 RID: 902
		[Range(0.3f, 2f)]
		public float zoomSpeed = 0.8f;

		// Token: 0x04000387 RID: 903
		private float distance;

		// Token: 0x04000388 RID: 904
		public float idleRotation = 1f;

		// Token: 0x04000389 RID: 905
		private Vector2 dir = new Vector2(0.8f, 0.2f);
	}
}

// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.Helpers.SplitScreenCamAddOn
using System;
using UnityEngine;

namespace ControlFreak2.Demos.Helpers
{
	[ExecuteInEditMode]
	public class SplitScreenCamAddOn : MonoBehaviour
	{
		private void OnEnable()
		{
			this.cam = base.GetComponent<Camera>();
		}

		private void OnDisable()
		{
			if (this.cam != null)
			{
				this.cam.ResetProjectionMatrix();
			}
		}

		private void LateUpdate()
		{
			if (this.cam == null)
			{
				return;
			}
			float num = this.cam.rect.width * (float)Screen.width;
			float num2 = this.cam.rect.height * (float)Screen.height;
			Vector2 vector = new Vector3(Mathf.Abs(Mathf.Sin(0.0174532924f * this.rotationAngle)), Mathf.Abs(Mathf.Cos(0.0174532924f * this.rotationAngle)));
			Vector2 vector2 = new Vector2(num * vector.x + num2 * vector.y, num2 * vector.x + num * vector.y);
			float aspect = vector2.x / vector2.y;
			Matrix4x4 matrix4x;
			if (this.cam.orthographic)
			{
				matrix4x = Matrix4x4.Ortho(-vector2.x * this.cam.orthographicSize, vector2.x * this.cam.orthographicSize, -vector2.y * this.cam.orthographicSize, vector2.y * this.cam.orthographicSize, this.cam.nearClipPlane, this.cam.farClipPlane);
			}
			else
			{
				matrix4x = Matrix4x4.Perspective(this.cam.fieldOfView, aspect, this.cam.nearClipPlane, this.cam.farClipPlane);
			}
			if (this.preMult)
			{
				matrix4x = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, this.rotationAngle), Vector3.one) * matrix4x;
			}
			else
			{
				matrix4x *= Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, this.rotationAngle), Vector3.one);
			}
			this.cam.projectionMatrix = matrix4x;
		}

		public float rotationAngle;

		private Camera cam;

		public bool preMult = true;
	}
}

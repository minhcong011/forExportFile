// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.Cameras.SimpleFollowCam
using System;
using UnityEngine;

namespace ControlFreak2.Demos.Cameras
{
	public class SimpleFollowCam : MonoBehaviour
	{
		private void OnEnable()
		{
			this.cam = base.GetComponent<Camera>();
			if (this.cam != null && this.cam.orthographic)
			{
				this.camZoomFactor = 0.5f;
			}
			if (this.targetTransform != null)
			{
				this.targetOfs = base.transform.position - this.targetTransform.position;
			}
		}

		private void Update()
		{
			if (!string.IsNullOrEmpty(this.camZoomDeltaAxis))
			{
				this.camZoomFactor += CF2Input.GetAxis(this.camZoomDeltaAxis);
			}
			this.camZoomFactor = Mathf.Clamp01(this.camZoomFactor);
		}

		private void FixedUpdate()
		{
			if (this.targetTransform == null)
			{
				return;
			}
			Vector3 vector = this.targetTransform.position + this.targetOfs;
			if (this.cam != null && this.cam.orthographic)
			{
				this.cam.orthographicSize = CFUtils.SmoothTowards(this.cam.orthographicSize, Mathf.Lerp(this.orthoZoomInSize, this.orthoZoomOutSize, this.camZoomFactor), this.smoothingTime, CFUtils.realDeltaTimeClamped, 0.0001f, 0.75f);
			}
			else
			{
				vector -= base.transform.forward * (this.camZoomFactor * this.perspZoomOutOffset);
			}
			if (this.smoothingTime > 0.001f)
			{
				vector = Vector3.SmoothDamp(base.transform.position, vector, ref this.smoothPosVel, this.smoothingTime);
			}
			base.transform.position = vector;
		}

		public Transform targetTransform;

		public float smoothingTime = 0.1f;

		private Vector3 targetOfs;

		private Vector3 smoothPosVel;

		private float camZoomFactor;

		public float perspZoomOutOffset = 10f;

		public float orthoZoomInSize = 2f;

		public float orthoZoomOutSize = 5f;

		public string camZoomDeltaAxis = "Cam-Zoom-Delta";

		private Camera cam;
	}
}

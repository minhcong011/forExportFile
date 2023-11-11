using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	public class FOVKick
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002C58 File Offset: 0x00000E58
		public void Setup(Camera camera)
		{
			this.CheckStatus(camera);
			this.Camera = camera;
			this.originalFov = camera.fieldOfView;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002C74 File Offset: 0x00000E74
		private void CheckStatus(Camera camera)
		{
			if (camera == null)
			{
				throw new Exception("FOVKick camera is null, please supply the camera to the constructor");
			}
			if (this.IncreaseCurve == null)
			{
				throw new Exception("FOVKick Increase curve is null, please define the curve for the field of view kicks");
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C9D File Offset: 0x00000E9D
		public void ChangeCamera(Camera camera)
		{
			this.Camera = camera;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002CA6 File Offset: 0x00000EA6
		public IEnumerator FOVKickUp()
		{
			float t = Mathf.Abs((this.Camera.fieldOfView - this.originalFov) / this.FOVIncrease);
			while (t < this.TimeToIncrease)
			{
				this.Camera.fieldOfView = this.originalFov + this.IncreaseCurve.Evaluate(t / this.TimeToIncrease) * this.FOVIncrease;
				t += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			yield break;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002CB5 File Offset: 0x00000EB5
		public IEnumerator FOVKickDown()
		{
			float t = Mathf.Abs((this.Camera.fieldOfView - this.originalFov) / this.FOVIncrease);
			while (t > 0f)
			{
				this.Camera.fieldOfView = this.originalFov + this.IncreaseCurve.Evaluate(t / this.TimeToDecrease) * this.FOVIncrease;
				t -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			this.Camera.fieldOfView = this.originalFov;
			yield break;
		}

		// Token: 0x0400002E RID: 46
		public Camera Camera;

		// Token: 0x0400002F RID: 47
		[HideInInspector]
		public float originalFov;

		// Token: 0x04000030 RID: 48
		public float FOVIncrease = 3f;

		// Token: 0x04000031 RID: 49
		public float TimeToIncrease = 1f;

		// Token: 0x04000032 RID: 50
		public float TimeToDecrease = 1f;

		// Token: 0x04000033 RID: 51
		public AnimationCurve IncreaseCurve;
	}
}

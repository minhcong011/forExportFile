using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000014 RID: 20
	public class SimpleMouseRotator : MonoBehaviour
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002FEB File Offset: 0x000011EB
		private void Start()
		{
			this.m_OriginalRotation = base.transform.localRotation;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003000 File Offset: 0x00001200
		private void Update()
		{
			base.transform.localRotation = this.m_OriginalRotation;
			if (this.relative)
			{
				float num = CrossPlatformInputManager.GetAxis("Mouse X");
				float num2 = CrossPlatformInputManager.GetAxis("Mouse Y");
				if (this.m_TargetAngles.y > 180f)
				{
					this.m_TargetAngles.y = this.m_TargetAngles.y - 360f;
					this.m_FollowAngles.y = this.m_FollowAngles.y - 360f;
				}
				if (this.m_TargetAngles.x > 180f)
				{
					this.m_TargetAngles.x = this.m_TargetAngles.x - 360f;
					this.m_FollowAngles.x = this.m_FollowAngles.x - 360f;
				}
				if (this.m_TargetAngles.y < -180f)
				{
					this.m_TargetAngles.y = this.m_TargetAngles.y + 360f;
					this.m_FollowAngles.y = this.m_FollowAngles.y + 360f;
				}
				if (this.m_TargetAngles.x < -180f)
				{
					this.m_TargetAngles.x = this.m_TargetAngles.x + 360f;
					this.m_FollowAngles.x = this.m_FollowAngles.x + 360f;
				}
				if (this.autoZeroHorizontalOnMobile)
				{
					this.m_TargetAngles.y = Mathf.Lerp(-this.rotationRange.y * 0.5f, this.rotationRange.y * 0.5f, num * 0.5f + 0.5f);
				}
				else
				{
					this.m_TargetAngles.y = this.m_TargetAngles.y + num * this.rotationSpeed;
				}
				if (this.autoZeroVerticalOnMobile)
				{
					this.m_TargetAngles.x = Mathf.Lerp(-this.rotationRange.x * 0.5f, this.rotationRange.x * 0.5f, num2 * 0.5f + 0.5f);
				}
				else
				{
					this.m_TargetAngles.x = this.m_TargetAngles.x + num2 * this.rotationSpeed;
				}
				this.m_TargetAngles.y = Mathf.Clamp(this.m_TargetAngles.y, -this.rotationRange.y * 0.5f, this.rotationRange.y * 0.5f);
				this.m_TargetAngles.x = Mathf.Clamp(this.m_TargetAngles.x, -this.rotationRange.x * 0.5f, this.rotationRange.x * 0.5f);
			}
			else
			{
				float num = Input.mousePosition.x;
				float num2 = Input.mousePosition.y;
				this.m_TargetAngles.y = Mathf.Lerp(-this.rotationRange.y * 0.5f, this.rotationRange.y * 0.5f, num / (float)Screen.width);
				this.m_TargetAngles.x = Mathf.Lerp(-this.rotationRange.x * 0.5f, this.rotationRange.x * 0.5f, num2 / (float)Screen.height);
			}
			this.m_FollowAngles = Vector3.SmoothDamp(this.m_FollowAngles, this.m_TargetAngles, ref this.m_FollowVelocity, this.dampingTime);
			base.transform.localRotation = this.m_OriginalRotation * Quaternion.Euler(-this.m_FollowAngles.x, this.m_FollowAngles.y, 0f);
		}

		// Token: 0x0400004C RID: 76
		public Vector2 rotationRange = new Vector3(70f, 70f);

		// Token: 0x0400004D RID: 77
		public float rotationSpeed = 10f;

		// Token: 0x0400004E RID: 78
		public float dampingTime = 0.2f;

		// Token: 0x0400004F RID: 79
		public bool autoZeroVerticalOnMobile = true;

		// Token: 0x04000050 RID: 80
		public bool autoZeroHorizontalOnMobile;

		// Token: 0x04000051 RID: 81
		public bool relative = true;

		// Token: 0x04000052 RID: 82
		private Vector3 m_TargetAngles;

		// Token: 0x04000053 RID: 83
		private Vector3 m_FollowAngles;

		// Token: 0x04000054 RID: 84
		private Vector3 m_FollowVelocity;

		// Token: 0x04000055 RID: 85
		private Quaternion m_OriginalRotation;
	}
}

using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class CurveControlledBob
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000275C File Offset: 0x0000095C
		public void Setup(Camera camera, float bobBaseInterval)
		{
			this.m_BobBaseInterval = bobBaseInterval;
			this.m_OriginalCameraPosition = camera.transform.localPosition;
			this.m_Time = this.Bobcurve[this.Bobcurve.length - 1].time;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027A8 File Offset: 0x000009A8
		public Vector3 DoHeadBob(float speed)
		{
			float x = this.m_OriginalCameraPosition.x + this.Bobcurve.Evaluate(this.m_CyclePositionX) * this.HorizontalBobRange;
			float y = this.m_OriginalCameraPosition.y + this.Bobcurve.Evaluate(this.m_CyclePositionY) * this.VerticalBobRange;
			this.m_CyclePositionX += speed * Time.deltaTime / this.m_BobBaseInterval;
			this.m_CyclePositionY += speed * Time.deltaTime / this.m_BobBaseInterval * this.VerticaltoHorizontalRatio;
			if (this.m_CyclePositionX > this.m_Time)
			{
				this.m_CyclePositionX -= this.m_Time;
			}
			if (this.m_CyclePositionY > this.m_Time)
			{
				this.m_CyclePositionY -= this.m_Time;
			}
			return new Vector3(x, y, 0f);
		}

		// Token: 0x04000011 RID: 17
		public float HorizontalBobRange = 0.33f;

		// Token: 0x04000012 RID: 18
		public float VerticalBobRange = 0.33f;

		// Token: 0x04000013 RID: 19
		public AnimationCurve Bobcurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(0.5f, 1f),
			new Keyframe(1f, 0f),
			new Keyframe(1.5f, -1f),
			new Keyframe(2f, 0f)
		});

		// Token: 0x04000014 RID: 20
		public float VerticaltoHorizontalRatio = 1f;

		// Token: 0x04000015 RID: 21
		private float m_CyclePositionX;

		// Token: 0x04000016 RID: 22
		private float m_CyclePositionY;

		// Token: 0x04000017 RID: 23
		private float m_BobBaseInterval;

		// Token: 0x04000018 RID: 24
		private Vector3 m_OriginalCameraPosition;

		// Token: 0x04000019 RID: 25
		private float m_Time;
	}
}

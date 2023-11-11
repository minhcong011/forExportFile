using System;
using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	// Token: 0x0200005E RID: 94
	public class TargetFieldOfView : AbstractTargetFollower
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000BFC5 File Offset: 0x0000A1C5
		protected override void Start()
		{
			base.Start();
			this.m_BoundSize = TargetFieldOfView.MaxBoundsExtent(this.m_Target, this.m_IncludeEffectsInSize);
			this.m_Cam = base.GetComponentInChildren<Camera>();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		protected override void FollowTarget(float deltaTime)
		{
			float magnitude = (this.m_Target.position - base.transform.position).magnitude;
			float target = Mathf.Atan2(this.m_BoundSize, magnitude) * 57.29578f * this.m_ZoomAmountMultiplier;
			this.m_Cam.fieldOfView = Mathf.SmoothDamp(this.m_Cam.fieldOfView, target, ref this.m_FovAdjustVelocity, this.m_FovAdjustTime);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000C063 File Offset: 0x0000A263
		public override void SetTarget(Transform newTransform)
		{
			base.SetTarget(newTransform);
			this.m_BoundSize = TargetFieldOfView.MaxBoundsExtent(newTransform, this.m_IncludeEffectsInSize);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000C080 File Offset: 0x0000A280
		public static float MaxBoundsExtent(Transform obj, bool includeEffects)
		{
			Renderer[] componentsInChildren = obj.GetComponentsInChildren<Renderer>();
			Bounds bounds = default(Bounds);
			bool flag = false;
			foreach (Renderer renderer in componentsInChildren)
			{
				if (!(renderer is TrailRenderer) && !(renderer is ParticleSystemRenderer))
				{
					if (!flag)
					{
						flag = true;
						bounds = renderer.bounds;
					}
					else
					{
						bounds.Encapsulate(renderer.bounds);
					}
				}
			}
			return Mathf.Max(new float[]
			{
				bounds.extents.x,
				bounds.extents.y,
				bounds.extents.z
			});
		}

		// Token: 0x04000250 RID: 592
		[SerializeField]
		private float m_FovAdjustTime = 1f;

		// Token: 0x04000251 RID: 593
		[SerializeField]
		private float m_ZoomAmountMultiplier = 2f;

		// Token: 0x04000252 RID: 594
		[SerializeField]
		private bool m_IncludeEffectsInSize;

		// Token: 0x04000253 RID: 595
		private float m_BoundSize;

		// Token: 0x04000254 RID: 596
		private float m_FovAdjustVelocity;

		// Token: 0x04000255 RID: 597
		private Camera m_Cam;

		// Token: 0x04000256 RID: 598
		private Transform m_LastTarget;
	}
}

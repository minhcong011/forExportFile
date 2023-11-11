﻿using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	// Token: 0x0200005D RID: 93
	public class ProtectCameraFromWallClip : MonoBehaviour
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000BC53 File Offset: 0x00009E53
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000BC5B File Offset: 0x00009E5B
		public bool protecting { get; private set; }

		// Token: 0x06000205 RID: 517 RVA: 0x0000BC64 File Offset: 0x00009E64
		private void Start()
		{
			this.m_Cam = base.GetComponentInChildren<Camera>().transform;
			this.m_Pivot = this.m_Cam.parent;
			this.m_OriginalDist = this.m_Cam.localPosition.magnitude;
			this.m_CurrentDist = this.m_OriginalDist;
			this.m_RayHitComparer = new ProtectCameraFromWallClip.RayHitComparer();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		private void LateUpdate()
		{
			float num = this.m_OriginalDist;
			this.m_Ray.origin = this.m_Pivot.position + this.m_Pivot.forward * this.sphereCastRadius;
			this.m_Ray.direction = -this.m_Pivot.forward;
			Collider[] array = Physics.OverlapSphere(this.m_Ray.origin, this.sphereCastRadius);
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].isTrigger && (!(array[i].attachedRigidbody != null) || !array[i].attachedRigidbody.CompareTag(this.dontClipTag)))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				this.m_Ray.origin = this.m_Ray.origin + this.m_Pivot.forward * this.sphereCastRadius;
				this.m_Hits = Physics.RaycastAll(this.m_Ray, this.m_OriginalDist - this.sphereCastRadius);
			}
			else
			{
				this.m_Hits = Physics.SphereCastAll(this.m_Ray, this.sphereCastRadius, this.m_OriginalDist + this.sphereCastRadius);
			}
			Array.Sort(this.m_Hits, this.m_RayHitComparer);
			float num2 = float.PositiveInfinity;
			for (int j = 0; j < this.m_Hits.Length; j++)
			{
				if (this.m_Hits[j].distance < num2 && !this.m_Hits[j].collider.isTrigger && (!(this.m_Hits[j].collider.attachedRigidbody != null) || !this.m_Hits[j].collider.attachedRigidbody.CompareTag(this.dontClipTag)))
				{
					num2 = this.m_Hits[j].distance;
					num = -this.m_Pivot.InverseTransformPoint(this.m_Hits[j].point).z;
					flag2 = true;
				}
			}
			if (flag2)
			{
				Debug.DrawRay(this.m_Ray.origin, -this.m_Pivot.forward * (num + this.sphereCastRadius), Color.red);
			}
			this.protecting = flag2;
			this.m_CurrentDist = Mathf.SmoothDamp(this.m_CurrentDist, num, ref this.m_MoveVelocity, (this.m_CurrentDist > num) ? this.clipMoveTime : this.returnTime);
			this.m_CurrentDist = Mathf.Clamp(this.m_CurrentDist, this.closestDistance, this.m_OriginalDist);
			this.m_Cam.localPosition = -Vector3.forward * this.m_CurrentDist;
		}

		// Token: 0x04000241 RID: 577
		public float clipMoveTime = 0.05f;

		// Token: 0x04000242 RID: 578
		public float returnTime = 0.4f;

		// Token: 0x04000243 RID: 579
		public float sphereCastRadius = 0.1f;

		// Token: 0x04000244 RID: 580
		public bool visualiseInEditor;

		// Token: 0x04000245 RID: 581
		public float closestDistance = 0.5f;

		// Token: 0x04000247 RID: 583
		public string dontClipTag = "Player";

		// Token: 0x04000248 RID: 584
		private Transform m_Cam;

		// Token: 0x04000249 RID: 585
		private Transform m_Pivot;

		// Token: 0x0400024A RID: 586
		private float m_OriginalDist;

		// Token: 0x0400024B RID: 587
		private float m_MoveVelocity;

		// Token: 0x0400024C RID: 588
		private float m_CurrentDist;

		// Token: 0x0400024D RID: 589
		private Ray m_Ray;

		// Token: 0x0400024E RID: 590
		private RaycastHit[] m_Hits;

		// Token: 0x0400024F RID: 591
		private ProtectCameraFromWallClip.RayHitComparer m_RayHitComparer;

		// Token: 0x0200008E RID: 142
		public class RayHitComparer : IComparer
		{
			// Token: 0x06000298 RID: 664 RVA: 0x0000D820 File Offset: 0x0000BA20
			public int Compare(object x, object y)
			{
				return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
			}
		}
	}
}

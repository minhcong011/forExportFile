using System;
using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	// Token: 0x02000057 RID: 87
	public abstract class AbstractTargetFollower : MonoBehaviour
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000B2A6 File Offset: 0x000094A6
		protected virtual void Start()
		{
			if (this.m_AutoTargetPlayer)
			{
				this.FindAndTargetPlayer();
			}
			if (this.m_Target == null)
			{
				return;
			}
			this.targetRigidbody = this.m_Target.GetComponent<Rigidbody>();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000B2D8 File Offset: 0x000094D8
		private void FixedUpdate()
		{
			if (this.m_AutoTargetPlayer && (this.m_Target == null || !this.m_Target.gameObject.activeSelf))
			{
				this.FindAndTargetPlayer();
			}
			if (this.m_UpdateType == AbstractTargetFollower.UpdateType.FixedUpdate)
			{
				this.FollowTarget(Time.deltaTime);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B328 File Offset: 0x00009528
		private void LateUpdate()
		{
			if (this.m_AutoTargetPlayer && (this.m_Target == null || !this.m_Target.gameObject.activeSelf))
			{
				this.FindAndTargetPlayer();
			}
			if (this.m_UpdateType == AbstractTargetFollower.UpdateType.LateUpdate)
			{
				this.FollowTarget(Time.deltaTime);
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000B378 File Offset: 0x00009578
		public void ManualUpdate()
		{
			if (this.m_AutoTargetPlayer && (this.m_Target == null || !this.m_Target.gameObject.activeSelf))
			{
				this.FindAndTargetPlayer();
			}
			if (this.m_UpdateType == AbstractTargetFollower.UpdateType.ManualUpdate)
			{
				this.FollowTarget(Time.deltaTime);
			}
		}

		// Token: 0x060001EF RID: 495
		protected abstract void FollowTarget(float deltaTime);

		// Token: 0x060001F0 RID: 496 RVA: 0x0000B3C8 File Offset: 0x000095C8
		public void FindAndTargetPlayer()
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
			if (gameObject)
			{
				this.SetTarget(gameObject.transform);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000B3F4 File Offset: 0x000095F4
		public virtual void SetTarget(Transform newTransform)
		{
			this.m_Target = newTransform;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000B3FD File Offset: 0x000095FD
		public Transform Target
		{
			get
			{
				return this.m_Target;
			}
		}

		// Token: 0x04000218 RID: 536
		[SerializeField]
		protected Transform m_Target;

		// Token: 0x04000219 RID: 537
		[SerializeField]
		private bool m_AutoTargetPlayer = true;

		// Token: 0x0400021A RID: 538
		[SerializeField]
		private AbstractTargetFollower.UpdateType m_UpdateType;

		// Token: 0x0400021B RID: 539
		protected Rigidbody targetRigidbody;

		// Token: 0x0200008D RID: 141
		public enum UpdateType
		{
			// Token: 0x04000310 RID: 784
			FixedUpdate,
			// Token: 0x04000311 RID: 785
			LateUpdate,
			// Token: 0x04000312 RID: 786
			ManualUpdate
		}
	}
}

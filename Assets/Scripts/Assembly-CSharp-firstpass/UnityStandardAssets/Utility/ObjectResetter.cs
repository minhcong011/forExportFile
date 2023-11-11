using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000010 RID: 16
	public class ObjectResetter : MonoBehaviour
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002DA0 File Offset: 0x00000FA0
		private void Start()
		{
			this.originalStructure = new List<Transform>(base.GetComponentsInChildren<Transform>());
			this.originalPosition = base.transform.position;
			this.originalRotation = base.transform.rotation;
			this.Rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002DEC File Offset: 0x00000FEC
		public void DelayedReset(float delay)
		{
			base.StartCoroutine(this.ResetCoroutine(delay));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002DFC File Offset: 0x00000FFC
		public IEnumerator ResetCoroutine(float delay)
		{
			yield return new WaitForSeconds(delay);
			foreach (Transform transform in base.GetComponentsInChildren<Transform>())
			{
				if (!this.originalStructure.Contains(transform))
				{
					transform.parent = null;
				}
			}
			base.transform.position = this.originalPosition;
			base.transform.rotation = this.originalRotation;
			if (this.Rigidbody)
			{
				this.Rigidbody.velocity = Vector3.zero;
				this.Rigidbody.angularVelocity = Vector3.zero;
			}
			base.SendMessage("Reset");
			yield break;
		}

		// Token: 0x0400003D RID: 61
		private Vector3 originalPosition;

		// Token: 0x0400003E RID: 62
		private Quaternion originalRotation;

		// Token: 0x0400003F RID: 63
		private List<Transform> originalStructure;

		// Token: 0x04000040 RID: 64
		private Rigidbody Rigidbody;
	}
}

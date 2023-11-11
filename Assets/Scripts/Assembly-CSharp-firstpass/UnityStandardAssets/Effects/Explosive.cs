using System.Collections;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace UnityStandardAssets.Effects
{
	// Token: 0x0200001D RID: 29
	public class Explosive : MonoBehaviour
	{
		// Token: 0x06000069 RID: 105 RVA: 0x000042BA File Offset: 0x000024BA
		private void Start()
		{
			this.m_ObjectResetter = base.GetComponent<ObjectResetter>();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000042C8 File Offset: 0x000024C8
		private IEnumerator OnCollisionEnter(Collision col)
		{
			if (base.enabled && col.contacts.Length != 0 && (Vector3.Project(col.relativeVelocity, col.contacts[0].normal).magnitude > this.detonationImpactVelocity || this.m_Exploded) && !this.m_Exploded)
			{
				Object.Instantiate<Transform>(this.explosionPrefab, col.contacts[0].point, Quaternion.LookRotation(col.contacts[0].normal));
				this.m_Exploded = true;
				base.SendMessage("Immobilize");
				if (this.reset)
				{
					this.m_ObjectResetter.DelayedReset(this.resetTimeDelay);
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000042DE File Offset: 0x000024DE
		public void Reset()
		{
			this.m_Exploded = false;
		}

		// Token: 0x04000088 RID: 136
		public Transform explosionPrefab;

		// Token: 0x04000089 RID: 137
		public float detonationImpactVelocity = 10f;

		// Token: 0x0400008A RID: 138
		public float sizeMultiplier = 1f;

		// Token: 0x0400008B RID: 139
		public bool reset = true;

		// Token: 0x0400008C RID: 140
		public float resetTimeDelay = 10f;

		// Token: 0x0400008D RID: 141
		private bool m_Exploded;

		// Token: 0x0400008E RID: 142
		private ObjectResetter m_ObjectResetter;
	}
}

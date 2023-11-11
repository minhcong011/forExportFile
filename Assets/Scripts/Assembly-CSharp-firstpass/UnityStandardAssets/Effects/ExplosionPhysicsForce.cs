using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x0200001C RID: 28
	public class ExplosionPhysicsForce : MonoBehaviour
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00004298 File Offset: 0x00002498
		private IEnumerator Start()
		{
			yield return null;
			float multiplier = base.GetComponent<ParticleSystemMultiplier>().multiplier;
			float num = 10f * multiplier;
			Collider[] array = Physics.OverlapSphere(base.transform.position, num);
			List<Rigidbody> list = new List<Rigidbody>();
			foreach (Collider collider in array)
			{
				if (collider.attachedRigidbody != null && !list.Contains(collider.attachedRigidbody))
				{
					list.Add(collider.attachedRigidbody);
				}
			}
			using (List<Rigidbody>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Rigidbody rigidbody = enumerator.Current;
					rigidbody.AddExplosionForce(this.explosionForce * multiplier, base.transform.position, num, 1f * multiplier, ForceMode.Impulse);
				}
				yield break;
			}
			yield break;
		}

		// Token: 0x04000087 RID: 135
		public float explosionForce = 4f;
	}
}

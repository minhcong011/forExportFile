using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x0200001B RID: 27
	public class ExplosionFireAndDebris : MonoBehaviour
	{
		// Token: 0x06000064 RID: 100 RVA: 0x0000425D File Offset: 0x0000245D
		private IEnumerator Start()
		{
			float multiplier = base.GetComponent<ParticleSystemMultiplier>().multiplier;
			int num = 0;
			while ((float)num < (float)this.numDebrisPieces * multiplier)
			{
				Transform original = this.debrisPrefabs[Random.Range(0, this.debrisPrefabs.Length)];
				Vector3 position = base.transform.position + Random.insideUnitSphere * 3f * multiplier;
				Quaternion rotation = Random.rotation;
				Object.Instantiate<Transform>(original, position, rotation);
				num++;
			}
			yield return null;
			float num2 = 10f * multiplier;
			foreach (Collider collider in Physics.OverlapSphere(base.transform.position, num2))
			{
				if (this.numFires > 0)
				{
					Ray ray = new Ray(base.transform.position, collider.transform.position - base.transform.position);
					RaycastHit raycastHit;
					if (collider.Raycast(ray, out raycastHit, num2))
					{
						this.AddFire(collider.transform, raycastHit.point, raycastHit.normal);
						this.numFires--;
					}
				}
			}
			float num3 = 0f;
			while (this.numFires > 0 && num3 < num2)
			{
				RaycastHit raycastHit2;
				if (Physics.Raycast(new Ray(base.transform.position + Vector3.up, Random.onUnitSphere), out raycastHit2, num3))
				{
					this.AddFire(null, raycastHit2.point, raycastHit2.normal);
					this.numFires--;
				}
				num3 += num2 * 0.1f;
			}
			yield break;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000426C File Offset: 0x0000246C
		private void AddFire(Transform t, Vector3 pos, Vector3 normal)
		{
			pos += normal * 0.5f;
			Object.Instantiate<Transform>(this.firePrefab, pos, Quaternion.identity).parent = t;
		}

		// Token: 0x04000083 RID: 131
		public Transform[] debrisPrefabs;

		// Token: 0x04000084 RID: 132
		public Transform firePrefab;

		// Token: 0x04000085 RID: 133
		public int numDebrisPieces;

		// Token: 0x04000086 RID: 134
		public int numFires;
	}
}

using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000011 RID: 17
	public class ParticleSystemDestroyer : MonoBehaviour
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002E12 File Offset: 0x00001012
		private IEnumerator Start()
		{
			ParticleSystem[] systems = base.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem particleSystem in systems)
			{
				this.m_MaxLifetime = Mathf.Max(particleSystem.main.startLifetime.constant, this.m_MaxLifetime);
			}
			float stopTime = Time.time + Random.Range(this.minDuration, this.maxDuration);
			while (Time.time < stopTime && !this.m_EarlyStop)
			{
				yield return null;
			}
			Debug.Log("stopping " + base.name);
			ParticleSystem[] array = systems;
			for (int i = 0; i < array.Length; i++)
			{
                ParticleSystem.EmissionModule e = array[i].emission;

                e.enabled = false;
			}
			base.BroadcastMessage("Extinguish", SendMessageOptions.DontRequireReceiver);
			yield return new WaitForSeconds(this.m_MaxLifetime);
			Object.Destroy(base.gameObject);
			yield break;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002E21 File Offset: 0x00001021
		public void Stop()
		{
			this.m_EarlyStop = true;
		}

		// Token: 0x04000041 RID: 65
		public float minDuration = 8f;

		// Token: 0x04000042 RID: 66
		public float maxDuration = 10f;

		// Token: 0x04000043 RID: 67
		private float m_MaxLifetime;

		// Token: 0x04000044 RID: 68
		private bool m_EarlyStop;
	}
}

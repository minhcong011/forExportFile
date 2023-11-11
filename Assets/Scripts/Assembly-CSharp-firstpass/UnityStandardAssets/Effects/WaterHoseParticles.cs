using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x02000023 RID: 35
	public class WaterHoseParticles : MonoBehaviour
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00004686 File Offset: 0x00002886
		private void Start()
		{
			this.m_ParticleSystem = base.GetComponent<ParticleSystem>();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004694 File Offset: 0x00002894
		private void OnParticleCollision(GameObject other)
		{
			int collisionEvents = this.m_ParticleSystem.GetCollisionEvents(other, this.m_CollisionEvents);
			for (int i = 0; i < collisionEvents; i++)
			{
				if (Time.time > WaterHoseParticles.lastSoundTime + 0.2f)
				{
					WaterHoseParticles.lastSoundTime = Time.time;
				}
				Rigidbody component = this.m_CollisionEvents[i].colliderComponent.GetComponent<Rigidbody>();
				if (component != null)
				{
					Vector3 velocity = this.m_CollisionEvents[i].velocity;
					component.AddForce(velocity * this.force, ForceMode.Impulse);
				}
				other.BroadcastMessage("Extinguish", SendMessageOptions.DontRequireReceiver);
			}
		}

		// Token: 0x0400009C RID: 156
		public static float lastSoundTime;

		// Token: 0x0400009D RID: 157
		public float force = 1f;

		// Token: 0x0400009E RID: 158
		private List<ParticleCollisionEvent> m_CollisionEvents = new List<ParticleCollisionEvent>();

		// Token: 0x0400009F RID: 159
		private ParticleSystem m_ParticleSystem;
	}
}

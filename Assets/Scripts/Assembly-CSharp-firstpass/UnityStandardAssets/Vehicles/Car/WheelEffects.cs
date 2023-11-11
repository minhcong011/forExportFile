using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x02000044 RID: 68
	[RequireComponent(typeof(AudioSource))]
	public class WheelEffects : MonoBehaviour
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000081E6 File Offset: 0x000063E6
		// (set) Token: 0x0600015C RID: 348 RVA: 0x000081EE File Offset: 0x000063EE
		public bool skidding { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000081F7 File Offset: 0x000063F7
		// (set) Token: 0x0600015E RID: 350 RVA: 0x000081FF File Offset: 0x000063FF
		public bool PlayingAudio { get; private set; }

		// Token: 0x0600015F RID: 351 RVA: 0x00008208 File Offset: 0x00006408
		private void Start()
		{
			this.skidParticles = base.transform.root.GetComponentInChildren<ParticleSystem>();
			if (this.skidParticles == null)
			{
				Debug.LogWarning(" no particle system found on car to generate smoke particles", base.gameObject);
			}
			else
			{
				this.skidParticles.Stop();
			}
			this.m_WheelCollider = base.GetComponent<WheelCollider>();
			this.m_AudioSource = base.GetComponent<AudioSource>();
			this.PlayingAudio = false;
			if (WheelEffects.skidTrailsDetachedParent == null)
			{
				WheelEffects.skidTrailsDetachedParent = new GameObject("Skid Trails - Detached").transform;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008298 File Offset: 0x00006498
		public void EmitTyreSmoke()
		{
			this.skidParticles.transform.position = base.transform.position - base.transform.up * this.m_WheelCollider.radius;
			this.skidParticles.Emit(1);
			if (!this.skidding)
			{
				base.StartCoroutine(this.StartSkidTrail());
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008301 File Offset: 0x00006501
		public void PlayAudio()
		{
			this.m_AudioSource.Play();
			this.PlayingAudio = true;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008315 File Offset: 0x00006515
		public void StopAudio()
		{
			this.m_AudioSource.Stop();
			this.PlayingAudio = false;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00008329 File Offset: 0x00006529
		public IEnumerator StartSkidTrail()
		{
			this.skidding = true;
			this.m_SkidTrail = Object.Instantiate<Transform>(this.SkidTrailPrefab);
			while (this.m_SkidTrail == null)
			{
				yield return null;
			}
			this.m_SkidTrail.parent = base.transform;
			this.m_SkidTrail.localPosition = -Vector3.up * this.m_WheelCollider.radius;
			yield break;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008338 File Offset: 0x00006538
		public void EndSkidTrail()
		{
			if (!this.skidding)
			{
				return;
			}
			this.skidding = false;
			this.m_SkidTrail.parent = WheelEffects.skidTrailsDetachedParent;
			Object.Destroy(this.m_SkidTrail.gameObject, 10f);
		}

		// Token: 0x04000150 RID: 336
		public Transform SkidTrailPrefab;

		// Token: 0x04000151 RID: 337
		public static Transform skidTrailsDetachedParent;

		// Token: 0x04000152 RID: 338
		public ParticleSystem skidParticles;

		// Token: 0x04000155 RID: 341
		private AudioSource m_AudioSource;

		// Token: 0x04000156 RID: 342
		private Transform m_SkidTrail;

		// Token: 0x04000157 RID: 343
		private WheelCollider m_WheelCollider;
	}
}

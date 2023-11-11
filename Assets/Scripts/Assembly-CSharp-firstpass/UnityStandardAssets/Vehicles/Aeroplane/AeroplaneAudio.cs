using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000046 RID: 70
	public class AeroplaneAudio : MonoBehaviour
	{
		// Token: 0x0600016B RID: 363 RVA: 0x000085B4 File Offset: 0x000067B4
		private void Awake()
		{
			this.m_Plane = base.GetComponent<AeroplaneController>();
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			this.m_EngineSoundSource = base.gameObject.AddComponent<AudioSource>();
			this.m_EngineSoundSource.playOnAwake = false;
			this.m_WindSoundSource = base.gameObject.AddComponent<AudioSource>();
			this.m_WindSoundSource.playOnAwake = false;
			this.m_EngineSoundSource.clip = this.m_EngineSound;
			this.m_WindSoundSource.clip = this.m_WindSound;
			this.m_EngineSoundSource.minDistance = this.m_AdvancedSetttings.engineMinDistance;
			this.m_EngineSoundSource.maxDistance = this.m_AdvancedSetttings.engineMaxDistance;
			this.m_EngineSoundSource.loop = true;
			this.m_EngineSoundSource.dopplerLevel = this.m_AdvancedSetttings.engineDopplerLevel;
			this.m_WindSoundSource.minDistance = this.m_AdvancedSetttings.windMinDistance;
			this.m_WindSoundSource.maxDistance = this.m_AdvancedSetttings.windMaxDistance;
			this.m_WindSoundSource.loop = true;
			this.m_WindSoundSource.dopplerLevel = this.m_AdvancedSetttings.windDopplerLevel;
			this.Update();
			this.m_EngineSoundSource.Play();
			this.m_WindSoundSource.Play();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000086F0 File Offset: 0x000068F0
		private void Update()
		{
			float t = Mathf.InverseLerp(0f, this.m_Plane.MaxEnginePower, this.m_Plane.EnginePower);
			this.m_EngineSoundSource.pitch = Mathf.Lerp(this.m_EngineMinThrottlePitch, this.m_EngineMaxThrottlePitch, t);
			this.m_EngineSoundSource.pitch += this.m_Plane.ForwardSpeed * this.m_EngineFwdSpeedMultiplier;
			this.m_EngineSoundSource.volume = Mathf.InverseLerp(0f, this.m_Plane.MaxEnginePower * this.m_AdvancedSetttings.engineMasterVolume, this.m_Plane.EnginePower);
			float magnitude = this.m_Rigidbody.velocity.magnitude;
			this.m_WindSoundSource.pitch = this.m_WindBasePitch + magnitude * this.m_WindSpeedPitchFactor;
			this.m_WindSoundSource.volume = Mathf.InverseLerp(0f, this.m_WindMaxSpeedVolume, magnitude) * this.m_AdvancedSetttings.windMasterVolume;
		}

		// Token: 0x04000164 RID: 356
		[SerializeField]
		private AudioClip m_EngineSound;

		// Token: 0x04000165 RID: 357
		[SerializeField]
		private float m_EngineMinThrottlePitch = 0.4f;

		// Token: 0x04000166 RID: 358
		[SerializeField]
		private float m_EngineMaxThrottlePitch = 2f;

		// Token: 0x04000167 RID: 359
		[SerializeField]
		private float m_EngineFwdSpeedMultiplier = 0.002f;

		// Token: 0x04000168 RID: 360
		[SerializeField]
		private AudioClip m_WindSound;

		// Token: 0x04000169 RID: 361
		[SerializeField]
		private float m_WindBasePitch = 0.2f;

		// Token: 0x0400016A RID: 362
		[SerializeField]
		private float m_WindSpeedPitchFactor = 0.004f;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		private float m_WindMaxSpeedVolume = 100f;

		// Token: 0x0400016C RID: 364
		[SerializeField]
		private AeroplaneAudio.AdvancedSetttings m_AdvancedSetttings = new AeroplaneAudio.AdvancedSetttings();

		// Token: 0x0400016D RID: 365
		private AudioSource m_EngineSoundSource;

		// Token: 0x0400016E RID: 366
		private AudioSource m_WindSoundSource;

		// Token: 0x0400016F RID: 367
		private AeroplaneController m_Plane;

		// Token: 0x04000170 RID: 368
		private Rigidbody m_Rigidbody;

		// Token: 0x02000088 RID: 136
		[Serializable]
		public class AdvancedSetttings
		{
			// Token: 0x040002F3 RID: 755
			public float engineMinDistance = 50f;

			// Token: 0x040002F4 RID: 756
			public float engineMaxDistance = 1000f;

			// Token: 0x040002F5 RID: 757
			public float engineDopplerLevel = 1f;

			// Token: 0x040002F6 RID: 758
			[Range(0f, 1f)]
			public float engineMasterVolume = 0.5f;

			// Token: 0x040002F7 RID: 759
			public float windMinDistance = 10f;

			// Token: 0x040002F8 RID: 760
			public float windMaxDistance = 100f;

			// Token: 0x040002F9 RID: 761
			public float windDopplerLevel = 1f;

			// Token: 0x040002FA RID: 762
			[Range(0f, 1f)]
			public float windMasterVolume = 0.5f;
		}
	}
}

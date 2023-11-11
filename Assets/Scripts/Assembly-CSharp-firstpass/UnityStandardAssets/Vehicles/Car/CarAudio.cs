using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x0200003B RID: 59
	[RequireComponent(typeof(CarController))]
	public class CarAudio : MonoBehaviour
	{
		// Token: 0x0600012A RID: 298 RVA: 0x000072BC File Offset: 0x000054BC
		private void StartSound()
		{
			this.m_CarController = base.GetComponent<CarController>();
			this.m_HighAccel = this.SetUpEngineAudioSource(this.highAccelClip);
			if (this.engineSoundStyle == CarAudio.EngineAudioOptions.FourChannel)
			{
				this.m_LowAccel = this.SetUpEngineAudioSource(this.lowAccelClip);
				this.m_LowDecel = this.SetUpEngineAudioSource(this.lowDecelClip);
				this.m_HighDecel = this.SetUpEngineAudioSource(this.highDecelClip);
			}
			this.m_StartedSound = true;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007330 File Offset: 0x00005530
		private void StopSound()
		{
			AudioSource[] components = base.GetComponents<AudioSource>();
			for (int i = 0; i < components.Length; i++)
			{
				Object.Destroy(components[i]);
			}
			this.m_StartedSound = false;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007364 File Offset: 0x00005564
		private void Update()
		{
			float sqrMagnitude = (Camera.main.transform.position - base.transform.position).sqrMagnitude;
			if (this.m_StartedSound && sqrMagnitude > this.maxRolloffDistance * this.maxRolloffDistance)
			{
				this.StopSound();
			}
			if (!this.m_StartedSound && sqrMagnitude < this.maxRolloffDistance * this.maxRolloffDistance)
			{
				this.StartSound();
			}
			if (this.m_StartedSound)
			{
				float num = CarAudio.ULerp(this.lowPitchMin, this.lowPitchMax, this.m_CarController.Revs);
				num = Mathf.Min(this.lowPitchMax, num);
				if (this.engineSoundStyle == CarAudio.EngineAudioOptions.Simple)
				{
					this.m_HighAccel.pitch = num * this.pitchMultiplier * this.highPitchMultiplier;
					this.m_HighAccel.dopplerLevel = (this.useDoppler ? this.dopplerLevel : 0f);
					this.m_HighAccel.volume = 1f;
					return;
				}
				this.m_LowAccel.pitch = num * this.pitchMultiplier;
				this.m_LowDecel.pitch = num * this.pitchMultiplier;
				this.m_HighAccel.pitch = num * this.highPitchMultiplier * this.pitchMultiplier;
				this.m_HighDecel.pitch = num * this.highPitchMultiplier * this.pitchMultiplier;
				float num2 = Mathf.Abs(this.m_CarController.AccelInput);
				float num3 = 1f - num2;
				float num4 = Mathf.InverseLerp(0.2f, 0.8f, this.m_CarController.Revs);
				float num5 = 1f - num4;
				num4 = 1f - (1f - num4) * (1f - num4);
				num5 = 1f - (1f - num5) * (1f - num5);
				num2 = 1f - (1f - num2) * (1f - num2);
				num3 = 1f - (1f - num3) * (1f - num3);
				this.m_LowAccel.volume = num5 * num2;
				this.m_LowDecel.volume = num5 * num3;
				this.m_HighAccel.volume = num4 * num2;
				this.m_HighDecel.volume = num4 * num3;
				this.m_HighAccel.dopplerLevel = (this.useDoppler ? this.dopplerLevel : 0f);
				this.m_LowAccel.dopplerLevel = (this.useDoppler ? this.dopplerLevel : 0f);
				this.m_HighDecel.dopplerLevel = (this.useDoppler ? this.dopplerLevel : 0f);
				this.m_LowDecel.dopplerLevel = (this.useDoppler ? this.dopplerLevel : 0f);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007618 File Offset: 0x00005818
		private AudioSource SetUpEngineAudioSource(AudioClip clip)
		{
			AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
			audioSource.clip = clip;
			audioSource.volume = 0f;
			audioSource.loop = true;
			audioSource.time = Random.Range(0f, clip.length);
			audioSource.Play();
			audioSource.minDistance = 5f;
			audioSource.maxDistance = this.maxRolloffDistance;
			audioSource.dopplerLevel = 0f;
			return audioSource;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007687 File Offset: 0x00005887
		private static float ULerp(float from, float to, float value)
		{
			return (1f - value) * from + value * to;
		}

		// Token: 0x0400010C RID: 268
		public CarAudio.EngineAudioOptions engineSoundStyle = CarAudio.EngineAudioOptions.FourChannel;

		// Token: 0x0400010D RID: 269
		public AudioClip lowAccelClip;

		// Token: 0x0400010E RID: 270
		public AudioClip lowDecelClip;

		// Token: 0x0400010F RID: 271
		public AudioClip highAccelClip;

		// Token: 0x04000110 RID: 272
		public AudioClip highDecelClip;

		// Token: 0x04000111 RID: 273
		public float pitchMultiplier = 1f;

		// Token: 0x04000112 RID: 274
		public float lowPitchMin = 1f;

		// Token: 0x04000113 RID: 275
		public float lowPitchMax = 6f;

		// Token: 0x04000114 RID: 276
		public float highPitchMultiplier = 0.25f;

		// Token: 0x04000115 RID: 277
		public float maxRolloffDistance = 500f;

		// Token: 0x04000116 RID: 278
		public float dopplerLevel = 1f;

		// Token: 0x04000117 RID: 279
		public bool useDoppler = true;

		// Token: 0x04000118 RID: 280
		private AudioSource m_LowAccel;

		// Token: 0x04000119 RID: 281
		private AudioSource m_LowDecel;

		// Token: 0x0400011A RID: 282
		private AudioSource m_HighAccel;

		// Token: 0x0400011B RID: 283
		private AudioSource m_HighDecel;

		// Token: 0x0400011C RID: 284
		private bool m_StartedSound;

		// Token: 0x0400011D RID: 285
		private CarController m_CarController;

		// Token: 0x02000085 RID: 133
		public enum EngineAudioOptions
		{
			// Token: 0x040002EB RID: 747
			Simple,
			// Token: 0x040002EC RID: 748
			FourChannel
		}
	}
}

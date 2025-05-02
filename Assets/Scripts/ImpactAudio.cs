// dnSpy decompiler from Assembly-CSharp.dll class: ImpactAudio
using System;
using UnityEngine;

public class ImpactAudio : MonoBehaviour
{
	private void Awake()
	{
		this.myaudio = base.GetComponent<AudioSource>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.magnitude > 2f && !this.myaudio.isPlaying)
		{
			int num = UnityEngine.Random.Range(1, this.impactSounds.Length);
			this.myaudio.clip = this.impactSounds[num];
			this.myaudio.pitch = 0.9f + 0.1f * UnityEngine.Random.value;
			this.myaudio.PlayOneShot(this.myaudio.clip);
			this.impactSounds[num] = this.impactSounds[0];
			this.impactSounds[0] = this.myaudio.clip;
		}
	}

	private AudioSource myaudio;

	public AudioClip[] impactSounds;
}

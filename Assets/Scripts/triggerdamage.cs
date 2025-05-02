// dnSpy decompiler from Assembly-CSharp.dll class: triggerdamage
using System;
using UnityEngine;

public class triggerdamage : MonoBehaviour
{
	private void Start()
	{
		this.myAudioSource.Stop();
	}

	private void Update()
	{
		if (!this.setOn)
		{
			this.myAudioSource.Stop();
			ParticleSystem[] componentsInChildren = this.particles.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem particleSystem in componentsInChildren)
			{
				var _temp_val_3989 = particleSystem.emission; _temp_val_3989.rateOverTime = 0f;
			}
			ParticleSystem[] componentsInChildren2 = this.bloodparticles.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem particleSystem2 in componentsInChildren2)
			{
				var _temp_val_3988 = particleSystem2.emission; _temp_val_3988.rateOverTime = 0f;
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (this.setOn)
		{
			if (other.gameObject.tag == "flesh")
			{
				ParticleSystem[] componentsInChildren = this.bloodparticles.GetComponentsInChildren<ParticleSystem>();
				foreach (ParticleSystem particleSystem in componentsInChildren)
				{
					var _temp_val_3989 = particleSystem.emission; _temp_val_3989.rateOverTime = 10f;
				}
				if (!this.myAudioSource.isPlaying)
				{
					this.myAudioSource.clip = this.impactsound;
					this.myAudioSource.loop = true;
					this.myAudioSource.volume = 1f;
					this.myAudioSource.Play();
				}
				other.transform.SendMessageUpwards("Damage", this.damage * Time.deltaTime, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				ParticleSystem[] componentsInChildren2 = this.particles.GetComponentsInChildren<ParticleSystem>();
				foreach (ParticleSystem particleSystem2 in componentsInChildren2)
				{
					var _temp_val_3988 = particleSystem2.emission; _temp_val_3988.rateOverTime = 10f;
				}
				if (!this.myAudioSource.isPlaying)
				{
					this.myAudioSource.clip = this.impactsound;
					this.myAudioSource.loop = true;
					this.myAudioSource.volume = 1f;
					this.myAudioSource.Play();
				}
				other.transform.SendMessageUpwards("Damage", this.damage * Time.deltaTime, SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			ParticleSystem[] componentsInChildren3 = this.particles.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem particleSystem3 in componentsInChildren3)
			{
                var c = particleSystem3.emission;
                c.rateOverTime = 0f;
			}
			ParticleSystem[] componentsInChildren4 = this.bloodparticles.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem particleSystem4 in componentsInChildren4)
			{
                var x = particleSystem4.emission;
                x.rateOverTime = 0f;
			}
			this.myAudioSource.Stop();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		ParticleSystem[] componentsInChildren = this.particles.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particleSystem in componentsInChildren)
		{
			var _temp_val_3989 = particleSystem.emission; _temp_val_3989.rateOverTime = 0f;
		}
		ParticleSystem[] componentsInChildren2 = this.bloodparticles.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particleSystem2 in componentsInChildren2)
		{
			var _temp_val_3988 = particleSystem2.emission; _temp_val_3988.rateOverTime = 0f;
		}
		this.myAudioSource.Stop();
	}

	public float damage = 5f;

	public Transform particles;

	public Transform bloodparticles;

	public AudioClip impactsound;

	public AudioSource myAudioSource;

	public bool setOn;
}

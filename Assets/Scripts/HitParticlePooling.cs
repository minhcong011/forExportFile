// dnSpy decompiler from Assembly-CSharp.dll class: HitParticlePooling
using System;
using UnityEngine;

public class HitParticlePooling : MonoBehaviour
{
	private void OnEnable()
	{
		for (int i = 0; i < this.particles.Length; i++)
		{
			this.particles[i].Play();
		}
		UnityEngine.Object.Destroy(base.gameObject, 0.5f);
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	public ParticleSystem[] particles;
}

// dnSpy decompiler from Assembly-CSharp.dll class: shell
using System;
using UnityEngine;

public class shell : MonoBehaviour
{
	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject, this.waitTime);
		base.transform.localRotation = base.transform.localRotation * Quaternion.Euler(0f, UnityEngine.Random.Range(-90f, 90f), 0f);
	}

	private void Update()
	{
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (base.gameObject.layer != this.desiredmask.value)
		{
			base.gameObject.layer = 1 << this.desiredmask.value;
		}
		if (!this.myAudioSource.isPlaying)
		{
			this.myAudioSource.clip = this.shellsounds[UnityEngine.Random.Range(0, this.shellsounds.Length)];
			this.myAudioSource.Play();
		}
	}

	public float waitTime = 2f;

	public AudioSource myAudioSource;

	public AudioClip[] shellsounds;

	public LayerMask desiredmask;
}

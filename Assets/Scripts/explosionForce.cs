// dnSpy decompiler from Assembly-CSharp.dll class: explosionForce
using System;
using UnityEngine;

public class explosionForce : MonoBehaviour
{
	private void Start()
	{
		base.transform.rotation = Quaternion.Euler(this.myrotation);
		this.myaudio = base.GetComponent<AudioSource>();
		UnityEngine.Object.Destroy(base.gameObject, this.waitTime);
		int num = UnityEngine.Random.Range(1, this.explodeSounds.Length);
		this.myaudio.clip = this.explodeSounds[num];
		this.myaudio.pitch = 0.9f + 0.1f * UnityEngine.Random.value;
		this.myaudio.PlayOneShot(this.myaudio.clip);
		this.explodeSounds[num] = this.explodeSounds[0];
		this.explodeSounds[0] = this.myaudio.clip;
		Vector3 position = base.transform.position;
		Collider[] array = Physics.OverlapSphere(position, this.radius);
		foreach (Collider collider in array)
		{
			if (collider.GetComponent<Rigidbody>() != null)
			{
				Rigidbody component = collider.GetComponent<Rigidbody>();
				component.AddExplosionForce(this.power, position, this.radius, 3f);
			}
		}
	}

	public float radius = 5f;

	public float power = 200f;

	public float waitTime = 5f;

	public float damage = 150f;

	private AudioSource myaudio;

	public AudioClip[] explodeSounds;

	public Vector3 myrotation;
}

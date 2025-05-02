// dnSpy decompiler from Assembly-CSharp.dll class: playrandomsound
using System;
using UnityEngine;

public class playrandomsound : MonoBehaviour
{
	private void Start()
	{
		this.myaudio = base.GetComponent<AudioSource>();
		int num = UnityEngine.Random.Range(1, this.Sounds.Length);
		this.myaudio.clip = this.Sounds[num];
		this.myaudio.pitch = 0.9f + 0.1f * UnityEngine.Random.value;
		this.myaudio.PlayOneShot(this.myaudio.clip);
		this.Sounds[num] = this.Sounds[0];
		this.Sounds[0] = this.myaudio.clip;
	}

	private AudioSource myaudio;

	public AudioClip[] Sounds;
}

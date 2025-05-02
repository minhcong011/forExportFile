// dnSpy decompiler from Assembly-CSharp.dll class: meleeWeapon
using System;
using System.Collections;
using UnityEngine;

public class meleeWeapon : MonoBehaviour
{
	private void Awake()
	{
		this.myanimation = base.GetComponent<Animation>();
		this.weaponfirer = this.rayfirer.GetComponent<raycastfire>();
	}

	private void melee()
	{
		if (!this.myanimation.isPlaying)
		{
			int num = UnityEngine.Random.Range(0, this.attackAnims.Length);
			this.myAudioSource.clip = this.attackSound;
			this.myAudioSource.pitch = 0.9f + 0.1f * UnityEngine.Random.value;
			this.myAudioSource.Play();
			this.myanimation.Play(this.attackAnims[num].name);
			base.StartCoroutine(this.firedelayed(this.firedelay));
		}
	}

	private IEnumerator firedelayed(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		this.weaponfirer.fireMelee();
		yield break;
	}

	public AudioClip attackSound;

	public AudioSource myAudioSource;

	public AnimationClip[] attackAnims;

	public Transform rayfirer;

	private raycastfire weaponfirer;

	private Animation myanimation;

	public float firedelay = 0.2f;
}

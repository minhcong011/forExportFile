// dnSpy decompiler from Assembly-CSharp.dll class: throwgrenade
using System;
using System.Collections;
using UnityEngine;

public class throwgrenade : MonoBehaviour
{
	private void Awake()
	{
		this.myanimation = base.GetComponent<Animation>();
	}

	private void throwstuff()
	{
		if (this.player == null)
		{
			this.player = GameObject.FindGameObjectWithTag("Player");
		}
		if (this.inventory == null)
		{
			this.inventory = this.player.GetComponent<weaponselector>();
		}
		if (!this.myanimation.isPlaying && this.inventory.grenade > 0)
		{
			this.inventory.grenade--;
			base.StartCoroutine(this.throwprojectile(this.ejectdelay));
			this.myAudioSource.clip = this.throwSound;
			this.myAudioSource.pitch = 0.9f + 0.1f * UnityEngine.Random.value;
			this.myAudioSource.Play();
			this.myanimation.Play("throwAnim");
		}
	}

	private IEnumerator throwprojectile(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		GameObject grenadeInstance = UnityEngine.Object.Instantiate<GameObject>(this.projectile, base.transform.position, base.transform.rotation);
		grenadeInstance.GetComponent<Rigidbody>().AddRelativeForce(0f, this.throwforce / 4f, this.throwforce);
		yield break;
	}

	public float throwforce = 200f;

	public float ejectdelay = 1f;

	private float lastLaunch;

	public GameObject projectile;

	public AudioClip throwSound;

	public AudioSource myAudioSource;

	public GameObject player;

	private weaponselector inventory;

	private Animation myanimation;
}

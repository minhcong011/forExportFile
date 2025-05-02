// dnSpy decompiler from Assembly-CSharp.dll class: knife
using System;
using System.Collections;
using ControlFreak2;
using UnityEngine;

public class knife : MonoBehaviour
{
	private void Awake()
	{
		this.weaponfirer = this.rayfirer.GetComponent<raycastfire>();
		this.playercontrol = this.player.GetComponent<playercontroller>();
		this.myanimation = base.GetComponent<Animation>();
	}

	private void Start()
	{
		this.myanimation.Stop();
		this.onstart();
	}

	private void Update()
	{
		float num = this.speed * Time.deltaTime;
		this.inventory.currentammo = 0;
		float fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, this.normalFOV, Time.deltaTime * 2f);
		float fieldOfView2 = Mathf.Lerp(this.weaponcamera.fieldOfView, this.weaponnormalFOV, Time.deltaTime * 2f);
		Camera.main.fieldOfView = fieldOfView;
		this.weaponcamera.fieldOfView = fieldOfView2;
		if (CF2Input.GetButton("ThrowGrenade") && !this.myanimation.isPlaying && this.inventory.grenade > 0 && Time.timeSinceLevelLoad > this.inventory.lastGrenade + 1f)
		{
			this.inventory.lastGrenade = Time.timeSinceLevelLoad;
			base.StartCoroutine(this.setThrowGrenade());
		}
		if (this.retract)
		{
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.retractPos, num * 2f);
		}
		else if (this.playercontrol.running)
		{
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.runposition, num);
			this.wantedrotation = new Vector3(this.runXrotation, this.runYrotation, 0f);
		}
		else
		{
			this.wantedrotation = Vector3.zero;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.normalposition, num);
		}
		base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(this.wantedrotation), num * 3f);
		if (CF2Input.GetButton("Aim") || (double)CF2Input.GetAxis("Aim") > 0.1)
		{
			this.doswitch();
		}
		if (CF2Input.GetButton("Fire1") || (double)CF2Input.GetAxis("Fire1") > 0.1)
		{
			this.fire();
		}
	}

	private void onstart()
	{
		this.myAudioSource.Stop();
		this.fireAudioSource.Stop();
		this.retract = false;
		this.myanimation.Stop();
		if (this.weaponfirer == null)
		{
			this.weaponfirer = this.rayfirer.GetComponent<raycastfire>();
		}
		this.weaponfirer.inaccuracy = this.inaccuracy;
		this.weaponfirer.damage = this.damage;
		this.weaponfirer.range = this.range;
		this.weaponfirer.force = this.force;
		this.weaponfirer.projectilecount = 1;
		if (this.inventory == null)
		{
			this.inventory = this.player.GetComponent<weaponselector>();
			this.inventory.InitCurrentWeaponAmmo(-1);
		}
		this.inventory.showAIM(false);
		this.myAudioSource.clip = this.readySound;
		this.myAudioSource.loop = false;
		this.myAudioSource.Play();
		this.myanimation.Play(this.readytoA.name);
		this.isA = true;
	}

	public void fire()
	{
		if (!this.myanimation.isPlaying && this.isA)
		{
			this.fireAudioSource.clip = this.fireSounds[UnityEngine.Random.Range(0, this.fireSounds.Length)];
			this.fireAudioSource.pitch = 0.98f + 0.1f * UnityEngine.Random.value;
			this.fireAudioSource.Play();
			this.myanimation.clip = this.fireAnimsA[UnityEngine.Random.Range(0, this.fireAnimsA.Length)];
			this.myanimation.Play();
			base.StartCoroutine(this.firedelayed(0.3f));
		}
		else if (!this.myanimation.isPlaying)
		{
			this.fireAudioSource.clip = this.fireSounds[UnityEngine.Random.Range(0, this.fireSounds.Length)];
			this.fireAudioSource.pitch = 0.98f + 0.1f * UnityEngine.Random.value;
			this.fireAudioSource.Play();
			this.myanimation.clip = this.fireAnimsB[UnityEngine.Random.Range(0, this.fireAnimsB.Length)];
			this.myanimation.Play();
			base.StartCoroutine(this.firedelayed(0.3f));
		}
	}

	private void doRetract()
	{
		if (this.isA)
		{
			this.myanimation.Play(this.hideA.name);
		}
		else
		{
			this.myanimation.Play(this.hideB.name);
		}
	}

	private void doNormal()
	{
		this.retract = false;
		this.onstart();
	}

	private IEnumerator firedelayed(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		this.weaponfirer.fireMelee();
		yield break;
	}

	private void doswitch()
	{
		if (this.isA && !this.myanimation.isPlaying)
		{
			this.myanimation.clip = this.switchB;
			this.myanimation.Play();
			this.myAudioSource.clip = this.readySound;
			this.myAudioSource.loop = false;
			this.myAudioSource.volume = 1f;
			this.myAudioSource.Play();
			this.isA = false;
		}
		else if (!this.myanimation.isPlaying)
		{
			this.myanimation.clip = this.switchA;
			this.myanimation.Play();
			this.myAudioSource.clip = this.readySound;
			this.myAudioSource.loop = false;
			this.myAudioSource.volume = 1f;
			this.myAudioSource.Play();
			this.isA = true;
		}
	}

	private IEnumerator setThrowGrenade()
	{
		this.retract = true;
		this.grenadethrower.gameObject.SetActive(true);
		this.grenadethrower.gameObject.BroadcastMessage("throwstuff");
		yield return new WaitForSeconds(this.grenadethrower.GetComponent<Animation>()["throwAnim"].length);
		this.retract = false;
		this.grenadethrower.gameObject.SetActive(false);
		yield break;
	}

	public Vector3 normalposition;

	public float speed = 2f;

	public Transform player;

	public AudioSource myAudioSource;

	public AudioSource fireAudioSource;

	public AudioClip[] fireSounds;

	public AudioClip readySound;

	public AnimationClip[] fireAnimsA;

	public AnimationClip[] fireAnimsB;

	public AnimationClip hideA;

	public AnimationClip hideB;

	public AnimationClip readytoA;

	public AnimationClip readytoB;

	public AnimationClip switchA;

	public AnimationClip switchB;

	public float normalFOV = 65f;

	public float weaponnormalFOV = 32f;

	private bool isA = true;

	public float fireAnimSpeed = 1.1f;

	public float inaccuracy = 0.02f;

	public float force = 500f;

	public float damage = 50f;

	public float range = 2f;

	public Vector3 retractPos;

	private bool retract;

	public Transform rayfirer;

	public Transform grenadethrower;

	private Vector3 wantedrotation;

	public float runXrotation = 20f;

	public float runYrotation;

	public Vector3 runposition = Vector3.zero;

	private raycastfire weaponfirer;

	private weaponselector inventory;

	public Camera weaponcamera;

	private playercontroller playercontrol;

	private Animation myanimation;
}

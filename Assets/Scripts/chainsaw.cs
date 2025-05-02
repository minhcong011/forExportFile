// dnSpy decompiler from Assembly-CSharp.dll class: chainsaw
using System;
using System.Collections;
using ControlFreak2;
using UnityEngine;

public class chainsaw : MonoBehaviour
{
	private void Awake()
	{
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
		this.inventory.currentammo = 0;
		float num = this.speed * Time.deltaTime;
		float fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, this.normalFOV, Time.deltaTime * 2f);
		float fieldOfView2 = Mathf.Lerp(this.weaponcamera.fieldOfView, this.weaponnormalFOV, Time.deltaTime * 2f);
		Camera.main.fieldOfView = fieldOfView;
		this.weaponcamera.fieldOfView = fieldOfView2;
		if (CF2Input.GetButton("ThrowGrenade") && !this.myanimation.IsPlaying(this.fireAnim.name) && Time.timeSinceLevelLoad > this.inventory.lastGrenade + 1f)
		{
			this.inventory.lastGrenade = Time.timeSinceLevelLoad;
			base.StartCoroutine(this.setThrowGrenade());
		}
		if (this.retract)
		{
			this.canfire = false;
			this.bladecollider.GetComponent<triggerdamage>().setOn = false;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.retractPos, num * 2f);
		}
		else if (this.playercontrol.running)
		{
			this.canfire = true;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.runposition, num);
			this.wantedrotation = new Vector3(this.runXrotation, this.runYrotation, 0f);
		}
		else
		{
			this.canfire = true;
			this.wantedrotation = Vector3.zero;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.normalposition, num);
		}
		base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(this.wantedrotation), num * 3f);
		if ((CF2Input.GetButton("Fire1") || (double)CF2Input.GetAxis("Fire1") > 0.1) && this.canfire && !this.myanimation.IsPlaying(this.readyAnim.name) && !this.myanimation.IsPlaying(this.hideAnim.name))
		{
			this.bladecollider.GetComponent<triggerdamage>().setOn = true;
			if (!this.fireAudioSource.isPlaying)
			{
				this.fireAudioSource.clip = this.fireSound;
				this.fireAudioSource.loop = true;
				this.fireAudioSource.Play();
			}
			this.blade.GetComponent<Renderer>().material.SetFloat("_panning", 1f);
			this.myanimation[this.fireAnim.name].speed = this.fireAnimSpeed;
			this.myanimation.CrossFade(this.fireAnim.name);
		}
		else if (!this.myanimation.IsPlaying(this.readyAnim.name) && !this.myanimation.IsPlaying(this.hideAnim.name))
		{
			this.bladecollider.GetComponent<triggerdamage>().setOn = false;
			this.fireAudioSource.Stop();
			this.myanimation[this.idleAnim.name].speed = this.idleAnimSpeed;
			this.myanimation.CrossFade(this.idleAnim.name);
			this.blade.GetComponent<Renderer>().material.SetFloat("_panning", 0f);
		}
	}

	private void onstart()
	{
		this.myAudioSource.Stop();
		this.fireAudioSource.Stop();
		this.retract = false;
		if (this.inventory == null)
		{
			this.inventory = this.player.GetComponent<weaponselector>();
			this.inventory.InitCurrentWeaponAmmo(-1);
		}
		this.inventory.showAIM(false);
		this.bladecollider.GetComponent<triggerdamage>().setOn = false;
		this.myanimation.Stop();
		this.myAudioSource.clip = this.readySound;
		this.myAudioSource.loop = false;
		this.myAudioSource.volume = 1f;
		this.myAudioSource.Play();
		this.myanimation.Play(this.readyAnim.name);
		this.canfire = true;
	}

	private void doRetract()
	{
		this.myanimation.Play(this.hideAnim.name);
	}

	private void doNormal()
	{
		this.onstart();
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

	public Transform bladecollider;

	public Vector3 normalposition;

	public Vector3 retractPos;

	public float speed = 1f;

	public Transform player;

	public float normalFOV = 65f;

	public float weaponnormalFOV = 32f;

	public AudioSource myAudioSource;

	public Transform blade;

	public AudioSource fireAudioSource;

	public AudioClip fireSound;

	public AudioClip readySound;

	public float smoothdamping = 2f;

	public AnimationClip fireAnim;

	public float fireAnimSpeed = 1f;

	public AnimationClip idleAnim;

	public float idleAnimSpeed = 0.2f;

	public AnimationClip readyAnim;

	public AnimationClip hideAnim;

	public Camera weaponcamera;

	public float runXrotation = 20f;

	public float runYrotation;

	public Vector3 runposition = Vector3.zero;

	private Vector3 wantedrotation;

	private bool retract;

	private bool canfire = true;

	public Transform grenadethrower;

	private playercontroller playercontrol;

	private weaponselector inventory;

	private Animation myanimation;
}

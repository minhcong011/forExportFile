// dnSpy decompiler from Assembly-CSharp.dll class: flashlight
using System;
using System.Collections;
using ControlFreak2;
using UnityEngine;

public class flashlight : MonoBehaviour
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
		float fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, this.normalFOV, Time.deltaTime * 2f);
		float fieldOfView2 = Mathf.Lerp(this.weaponcamera.fieldOfView, this.weaponnormalFOV, Time.deltaTime * 2f);
		Camera.main.fieldOfView = fieldOfView;
		this.weaponcamera.fieldOfView = fieldOfView2;
		this.inventory.currentammo = 0;
		if (CF2Input.GetButton("ThrowGrenade") && this.myanimation.IsPlaying(this.idleAnim.name) && this.inventory.grenade > 0 && Time.timeSinceLevelLoad > this.inventory.lastGrenade + 1f)
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
		if (CF2Input.GetButton("Fire1") || ((double)CF2Input.GetAxis("Fire1") > 0.1 && this.myanimation.IsPlaying(this.idleAnim.name)))
		{
			this.myanimation.Stop(this.idleAnim.name);
			this.fire();
		}
		else if (!this.myanimation.isPlaying)
		{
			this.myanimation[this.idleAnim.name].speed = this.idleAnimSpeed;
			this.myanimation.CrossFade(this.idleAnim.name);
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
		}
		this.inventory.showAIM(false);
		this.myanimation.Play(this.readyAnim.name);
		this.myAudioSource.clip = this.readySound;
		this.myAudioSource.loop = false;
		this.myAudioSource.Play();
	}

	private void fire()
	{
		if (!this.myanimation.isPlaying)
		{
			this.fireAudioSource.clip = this.fireSounds[UnityEngine.Random.Range(0, this.fireSounds.Length)];
			this.fireAudioSource.pitch = 0.98f + 0.1f * UnityEngine.Random.value;
			this.fireAudioSource.Play();
			this.myanimation.clip = this.fireAnims[UnityEngine.Random.Range(0, this.fireAnims.Length)];
			this.myanimation.Play();
			base.StartCoroutine(this.firedelayed(0.3f));
		}
	}

	private void doRetract()
	{
		this.myanimation.Play(this.hideAnim.name);
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

	public AnimationClip[] fireAnims;

	public AnimationClip idleAnim;

	public AnimationClip readyAnim;

	public AnimationClip hideAnim;

	public float normalFOV = 65f;

	public float weaponnormalFOV = 32f;

	public float idleAnimSpeed = 0.4f;

	public float inaccuracy = 0.02f;

	public float force = 500f;

	public float damage = 50f;

	public float range = 2f;

	public Vector3 retractPos;

	private bool retract;

	public Camera weaponcamera;

	public Transform rayfirer;

	public Transform grenadethrower;

	private Vector3 wantedrotation;

	public float runXrotation = 20f;

	public float runYrotation;

	public Vector3 runposition = Vector3.zero;

	private raycastfire weaponfirer;

	private weaponselector inventory;

	private playercontroller playercontrol;

	private Animation myanimation;
}

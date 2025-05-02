// dnSpy decompiler from Assembly-CSharp.dll class: baseballbat
using System;
using System.Collections;
using ControlFreak2;
using UnityEngine;

public class baseballbat : MonoBehaviour
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
		if (this.retract)
		{
			this.canfire = false;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.retractPos, 5f * Time.deltaTime);
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
		if (CF2Input.GetButton("ThrowGrenade") && !base.GetComponent<Animation>().isPlaying && this.inventory.grenade > 0 && Time.timeSinceLevelLoad > this.inventory.lastGrenade + 1f)
		{
			this.inventory.lastGrenade = Time.timeSinceLevelLoad;
			base.StartCoroutine(this.setThrowGrenade());
		}
		if ((CF2Input.GetButton("Fire1") && this.canfire) || ((double)CF2Input.GetAxis("Fire1") > 0.1 && this.canfire))
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
		this.myanimation.Play(this.readyAnim.name);
		this.canfire = true;
	}

	private void fire()
	{
		if (!this.myanimation.isPlaying)
		{
			this.fireAudioSource.clip = this.fireSounds[UnityEngine.Random.Range(0, this.fireSounds.Length)];
			this.fireAudioSource.pitch = 0.98f + 0.1f * UnityEngine.Random.value;
			this.fireAudioSource.Play();
			this.myanimation.clip = this.fireAnimsA[UnityEngine.Random.Range(0, this.fireAnimsA.Length)];
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

	public AnimationClip[] fireAnimsA;

	public float normalFOV = 65f;

	public float weaponnormalFOV = 32f;

	public float fireAnimSpeed = 1.1f;

	public float inaccuracy = 0.02f;

	public float force = 500f;

	public float damage = 50f;

	public float range = 2f;

	public AnimationClip readyAnim;

	public AnimationClip hideAnim;

	public Vector3 retractPos;

	private bool retract;

	private bool canfire = true;

	public Transform rayfirer;

	public Transform grenadethrower;

	public float runXrotation = 20f;

	public float runYrotation;

	public Vector3 runposition = Vector3.zero;

	private Vector3 wantedrotation;

	public Camera weaponcamera;

	private raycastfire weaponfirer;

	private weaponselector inventory;

	private playercontroller playercontrol;

	private Animation myanimation;
}

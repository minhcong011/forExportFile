// dnSpy decompiler from Assembly-CSharp.dll class: flamethrower
using System;
using System.Collections;
using ControlFreak2;
using UnityEngine;

public class flamethrower : FPSCommonWeaponSpecs
{
	private void Awake()
	{
		this.playercontrol = this.player.GetComponent<playercontroller>();
		this.myanimation = base.GetComponent<Animation>();
	}

	private void Start()
	{
		this.playercontrol = this.player.GetComponent<playercontroller>();
		this.flamelight.gameObject.SetActive(false);
		this.curve = new AnimationCurve();
		this.curve.AddKey(0f, 0.1f);
		this.curve.AddKey(0.75f, 1f);
		this.nextField = this.normalFOV;
		this.weaponnextfield = this.weaponnormalFOV;
		this.myanimation.Stop();
		this.onstart();
	}

	private void OnDisable()
	{
		this.zoom = false;
	}

	public void ZoomPressed()
	{
		this.zoom = !this.zoom;
	}

	private void Update()
	{
		float num = this.speed * Time.deltaTime;
		if (CF2Input.GetButton("Reload") && this.currentammo != this.clipSize && Mathf.RoundToInt((float)this.ammo) > 0)
		{
			this.reload();
		}
		float fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, this.nextField, Time.deltaTime * 2f);
		float fieldOfView2 = Mathf.Lerp(this.weaponcamera.fieldOfView, this.weaponnextfield, Time.deltaTime * 2f);
		Camera.main.fieldOfView = fieldOfView;
		this.weaponcamera.fieldOfView = fieldOfView2;
		this.inventory.currentammo = Mathf.RoundToInt((float)this.currentammo);
		if (CF2Input.GetButton("ThrowGrenade") && this.myanimation[this.fireAnim.name].speed == 0f && Time.timeSinceLevelLoad > this.inventory.lastGrenade + 1f)
		{
			this.inventory.lastGrenade = Time.timeSinceLevelLoad;
			base.StartCoroutine(this.setThrowGrenade());
		}
		if (CF2Input.GetButton("Melee") && this.myanimation[this.fireAnim.name].speed == 0f)
		{
			base.StartCoroutine(this.setMelee());
		}
		if (this.retract)
		{
			this.canfire = false;
			this.canaim = false;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.retractPos, num * 2f);
			this.weaponnextfield = this.weaponnormalFOV;
			this.nextField = this.normalFOV;
		}
		else if (this.playercontrol.running)
		{
			this.canfire = false;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.runposition, num);
			this.wantedrotation = new Vector3(this.runXrotation, this.runYrotation, 0f);
			this.weaponnextfield = this.weaponnormalFOV;
			this.nextField = this.normalFOV;
		}
		else
		{
			this.canfire = true;
			this.wantedrotation = Vector3.zero;
			if (this.zoom && this.canaim && !this.playercontrol.running)
			{
				base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.aimposition, num);
				this.weaponnextfield = this.weaponaimFOV;
				this.nextField = this.aimFOV;
			}
			else
			{
				base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.normalposition, num);
				this.weaponnextfield = this.weaponnormalFOV;
				this.nextField = this.normalFOV;
			}
		}
		base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(this.wantedrotation), num * 3f);
		if ((float)this.currentammo <= 0f)
		{
			if ((float)this.ammo <= 0f)
			{
				this.canfire = false;
				this.canreload = false;
				if ((CF2Input.GetButton("Fire1") && this.canfire) || (double)CF2Input.GetAxis("Fire1") > 0.1)
				{
					if (!this.myAudioSource.isPlaying)
					{
						this.myAudioSource.PlayOneShot(this.emptySound);
					}
				}
				else
				{
					this.canreload = true;
				}
			}
			else
			{
				this.reload();
			}
		}
		if ((CF2Input.GetButton("Fire1") || (double)CF2Input.GetAxis("Fire1") > 0.1) && this.canfire && !this.isreloading && !this.myanimation.IsPlaying(this.readyAnim.name) && !this.myanimation.IsPlaying(this.hideAnim.name))
		{
			this.flamelight.gameObject.SetActive(true);
			this.currentammo -= (int)(5f * Time.deltaTime);
			ParticleSystem[] componentsInChildren = this.flames.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem particleSystem in componentsInChildren)
			{
				var _temp_val_3188 = particleSystem.emission; _temp_val_3188.rateOverTime = 50f;
			}
			if (!this.fireAudioSource.isPlaying)
			{
				this.fireAudioSource.clip = this.fireSound;
				this.fireAudioSource.loop = true;
				this.fireAudioSource.Play();
			}
			this.myanimation[this.fireAnim.name].speed = this.fireAnimSpeed;
			this.myanimation.Play(this.fireAnim.name);
		}
		else
		{
			ParticleSystem[] componentsInChildren2 = this.flames.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem particleSystem2 in componentsInChildren2)
			{
				var _temp_val_3187 = particleSystem2.emission; _temp_val_3187.rateOverTime = 0f;
			}
			this.fireAudioSource.Stop();
			this.myanimation[this.fireAnim.name].speed = 0f;
			this.flamelight.gameObject.SetActive(false);
		}
	}

	private void doRetract()
	{
		this.myanimation.Play(this.hideAnim.name);
	}

	private void onstart()
	{
		this.myAudioSource.Stop();
		this.fireAudioSource.Stop();
		if (this.inventory == null)
		{
			this.inventory = this.player.GetComponent<weaponselector>();
			this.inventory.InitCurrentWeaponAmmo(this.ammo);
		}
		this.flamelight.gameObject.SetActive(false);
		this.myanimation.Stop();
		if (this.isreloading)
		{
			this.reload();
		}
		else
		{
			this.myAudioSource.clip = this.readySound;
			this.myAudioSource.loop = false;
			this.myAudioSource.volume = 1f;
			this.myAudioSource.Play();
			this.myanimation.Play(this.readyAnim.name);
			this.canaim = true;
			this.canfire = true;
		}
	}

	private void reload()
	{
		if (this.canreload && !this.isreloading)
		{
			base.StartCoroutine(this.setreload(this.myanimation[this.reloadAnim.name].length));
			this.myAudioSource.clip = this.reloadSound;
			this.myAudioSource.loop = false;
			this.myAudioSource.volume = 1f;
			this.myAudioSource.Play();
			this.myanimation.Play(this.reloadAnim.name);
		}
	}

	private void doNormal()
	{
		this.onstart();
	}

	private IEnumerator setreload(float waitTime)
	{
		this.playercontrol.canclimb = false;
		this.inventory.canswitch = false;
		int oldammo = Mathf.RoundToInt((float)this.currentammo);
		this.isreloading = true;
		this.canaim = false;
		yield return new WaitForSeconds(waitTime * 0.5f);
		this.currentammo = Mathf.Clamp(this.clipSize, this.clipSize - oldammo, this.ammo);
		this.ammo -= Mathf.Clamp(this.clipSize, this.clipSize, this.ammo);
		this.inventory.UpdateCurrentWeaponAmmo(Mathf.RoundToInt((float)this.ammo));
		yield return new WaitForSeconds(waitTime * 0.5f);
		this.isreloading = false;
		this.canaim = true;
		this.inventory.canswitch = true;
		this.playercontrol.canclimb = true;
		yield break;
	}

	private IEnumerator setThrowGrenade()
	{
		this.retract = true;
		this.grenadethrower.gameObject.SetActive(true);
		this.grenadethrower.gameObject.BroadcastMessage("throwstuff");
		Animation throwerAnimation = this.grenadethrower.GetComponent<Animation>();
		yield return new WaitForSeconds(throwerAnimation.clip.length);
		this.retract = false;
		this.canaim = true;
		this.grenadethrower.gameObject.SetActive(false);
		yield break;
	}

	private void pickAmmo(int inventoryAmmo)
	{
		this.ammo = inventoryAmmo;
	}

	private IEnumerator setMelee()
	{
		if (!this.meleeweapon.gameObject.activeInHierarchy)
		{
			this.retract = true;
			this.meleeweapon.gameObject.SetActive(true);
			this.meleeweapon.gameObject.BroadcastMessage("melee");
			Animation meleeAnimation = this.meleeweapon.GetComponent<Animation>();
			yield return new WaitForSeconds(meleeAnimation.clip.length);
			this.retract = false;
			this.canaim = true;
			this.meleeweapon.gameObject.SetActive(false);
		}
		yield break;
	}

	public Vector3 normalposition;

	public Vector3 aimposition;

	public Vector3 retractPos;

	public AudioSource myAudioSource;

	public AudioSource fireAudioSource;

	public AudioClip emptySound;

	public AudioClip fireSound;

	public Transform flames;

	public AudioClip readySound;

	public AudioClip reloadSound;

	public AnimationClip fireAnim;

	public AnimationClip reloadAnim;

	public AnimationClip readyAnim;

	public AnimationClip hideAnim;

	public Camera weaponcamera;

	public float runXrotation = 20f;

	public float runYrotation;

	public Vector3 runposition = Vector3.zero;

	private float nextField;

	private float weaponnextfield;

	private Vector3 wantedrotation;

	private bool canaim = true;

	private bool canfire = true;

	private bool canreload = true;

	private bool retract;

	private bool isreloading;

	public Transform grenadethrower;

	private AnimationCurve curve;

	public Transform player;

	public Transform flamelight;

	private playercontroller playercontrol;

	private weaponselector inventory;

	public Transform meleeweapon;

	private Animation myanimation;

	public bool zoom;
}

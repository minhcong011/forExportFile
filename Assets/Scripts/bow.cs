// dnSpy decompiler from Assembly-CSharp.dll class: bow
using System;
using System.Collections;
using ControlFreak2;
using UnityEngine;

public class bow : FPSCommonWeaponSpecs
{
	private void Awake()
	{
		this.playercontrol = this.player.GetComponent<playercontroller>();
		this.myanimation = base.GetComponent<Animation>();
		this.cameracontroller = this.recoilCamera.GetComponent<camerarotate>();
	}

	private void Start()
	{
		this.clipSize = this.currentammo;
		this.lastaiming = this.isaiming;
		this.nextField = this.normalFOV;
		this.weaponnextfield = this.weaponnormalFOV;
		this.myanimation.Stop();
		this.onstart();
	}

	public void SetZoomSlider(float val)
	{
		this.zoomSliderVal = val;
	}

	private void OnDisable()
	{
		this.zoom = false;
		this.zoomSliderVal = 0f;
		this.nextField = this.normalFOV;
	}

	public void ZoomPressed()
	{
		this.zoom = !this.zoom;
	}

	private void Update()
	{
		float num = this.speed * Time.deltaTime;
		float fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, this.nextField, Time.deltaTime * 2f);
		float fieldOfView2 = Mathf.Lerp(this.weaponcamera.fieldOfView, this.weaponnextfield, Time.deltaTime * 2f);
		Camera.main.fieldOfView = fieldOfView;
		this.weaponcamera.fieldOfView = fieldOfView2;
		this.inventory.currentammo = this.currentammo;
		if (CF2Input.GetButton("ThrowGrenade") && !this.myanimation.isPlaying && this.inventory.grenade > 0 && this.canfire2 && Time.timeSinceLevelLoad > this.inventory.lastGrenade + 1f)
		{
			this.inventory.lastGrenade = Time.timeSinceLevelLoad;
			base.StartCoroutine(this.setThrowGrenade());
		}
		if (CF2Input.GetButton("Melee") && !this.myanimation.isPlaying && this.canfire2)
		{
			base.StartCoroutine(this.setMelee());
		}
		if (this.retract)
		{
			this.isaiming = false;
			this.canaim = false;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.retractPos, 5f * Time.deltaTime);
			this.weaponnextfield = this.weaponnormalFOV;
			this.nextField = this.normalFOV;
		}
		else if (this.playercontrol.running)
		{
			this.isaiming = false;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.runposition, num);
			this.wantedrotation = new Vector3(this.runXrotation, this.runYrotation, 0f);
			this.weaponnextfield = this.weaponnormalFOV;
			this.nextField = this.normalFOV;
		}
		else
		{
			this.wantedrotation = Vector3.zero;
			if (this.zoom && this.canaim && !this.playercontrol.running)
			{
				this.isaiming = true;
				base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.aimposition, num);
				this.weaponnextfield = this.weaponaimFOV;
				this.nextField = this.aimFOV - this.zoomSliderVal;
			}
			else
			{
				this.isaiming = false;
				base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.normalposition, num);
				this.weaponnextfield = this.weaponnormalFOV;
				this.nextField = this.normalFOV;
			}
		}
		base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(this.wantedrotation), num * 3f);
		if (this.isaiming != this.lastaiming && this.canaim)
		{
			base.StartCoroutine(this.doaim());
		}
		if (this.currentammo == 0 || this.currentammo <= 0)
		{
			if (this.ammo <= 0)
			{
				this.canfire = false;
				this.canreload = false;
				if ((CF2Input.GetButton("Fire1") || (double)CF2Input.GetAxis("Fire1") > 0.1) && !this.myAudioSource.isPlaying)
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
		if (!this.isreloading && this.canfire)
		{
			if (CF2Input.GetButton("Fire1") || (double)CF2Input.GetAxis("Fire1") > 0.1)
			{
				this.fire();
			}
		}
		else
		{
			this.inventory.showAIM(false);
		}
		this.lastaiming = this.isaiming;
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
		this.inventory.showAIM(false);
		this.myanimation.Stop();
		if (this.isreloading)
		{
			this.reload();
		}
		else
		{
			this.arrow.gameObject.SetActive(true);
			this.myAudioSource.clip = this.readySound;
			this.myAudioSource.loop = false;
			this.myAudioSource.volume = 1f;
			this.myAudioSource.Play();
			this.canfire = false;
			this.myanimation.Play(this.readyAnim.name);
			this.canaim = true;
		}
	}

	private void fire()
	{
		if (!this.myanimation.isPlaying)
		{
			UnityEngine.Object.Instantiate<Transform>(this.projectile, this.projectilePos.transform.position, this.projectilePos.transform.rotation);
			float num = UnityEngine.Random.Range(-0.05f, -0.01f);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, base.transform.localPosition.z + num);
			this.arrow.gameObject.SetActive(false);
			this.fireAudioSource.clip = this.fireSound;
			this.fireAudioSource.pitch = 0.9f + 0.1f * UnityEngine.Random.value;
			this.fireAudioSource.Play();
			this.myanimation[this.fireAnim.name].speed = this.fireAnimSpeed;
			this.myanimation.Play(this.fireAnim.name);
			this.currentammo--;
			if (this.currentammo <= 0)
			{
				this.reload();
			}
		}
	}

	private void reload()
	{
		if (!this.myanimation.isPlaying && this.canreload && !this.isreloading)
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
		this.canfire = false;
		this.playercontrol.canclimb = false;
		this.inventory.canswitch = false;
		int oldammo = this.currentammo;
		this.isreloading = true;
		this.canaim = false;
		yield return new WaitForSeconds(waitTime * 0.6f);
		this.currentammo = Mathf.Clamp(this.clipSize, this.clipSize - oldammo, this.ammo);
		this.ammo -= Mathf.Clamp(this.clipSize, this.clipSize, this.ammo);
		this.arrow.gameObject.SetActive(true);
		this.inventory.UpdateCurrentWeaponAmmo(this.ammo);
		yield return new WaitForSeconds(waitTime * 0.4f);
		this.isreloading = false;
		this.canaim = true;
		this.inventory.canswitch = true;
		this.playercontrol.canclimb = true;
		yield break;
	}

	private IEnumerator setThrowGrenade()
	{
		this.canfire2 = false;
		this.retract = true;
		this.grenadethrower.gameObject.SetActive(true);
		this.grenadethrower.gameObject.BroadcastMessage("throwstuff");
		Animation throwerAnimation = this.grenadethrower.GetComponent<Animation>();
		yield return new WaitForSeconds(throwerAnimation.clip.length);
		this.retract = false;
		this.canaim = true;
		this.canfire2 = true;
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
			this.canfire2 = false;
			this.retract = true;
			this.meleeweapon.gameObject.SetActive(true);
			this.meleeweapon.gameObject.BroadcastMessage("melee");
			Animation meleeAnimation = this.meleeweapon.GetComponent<Animation>();
			yield return new WaitForSeconds(meleeAnimation.clip.length);
			this.retract = false;
			this.canaim = true;
			this.canfire2 = true;
			this.meleeweapon.gameObject.SetActive(false);
		}
		yield break;
	}

	private IEnumerator doaim()
	{
		if (this.isaiming)
		{
			if (!this.isreloading && !this.myanimation.IsPlaying(this.fireAnim.name))
			{
				base.GetComponent<Animation>().Play(this.aimAnim.name);
				this.myAudioSource.clip = this.aimSound;
				this.myAudioSource.loop = false;
				this.myAudioSource.volume = 1f;
				this.myAudioSource.Play();
				yield return new WaitForSeconds(this.myanimation[this.aimAnim.name].length);
				this.canfire = true;
			}
		}
		else if (!this.isreloading && !this.myanimation.IsPlaying(this.fireAnim.name))
		{
			base.GetComponent<Animation>().Play(this.unaimAnim.name);
			this.myAudioSource.clip = this.aimSound;
			this.myAudioSource.loop = false;
			this.myAudioSource.volume = 1f;
			this.myAudioSource.Play();
			yield return new WaitForSeconds(this.myanimation[this.unaimAnim.name].length);
			this.canfire = false;
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

	public AudioClip readySound;

	public AudioClip reloadSound;

	public AudioClip aimSound;

	public int ammoToReload = 1;

	public float recoil = 5f;

	public Transform arrow;

	public Transform projectile;

	public Transform projectilePos;

	public AnimationClip aimAnim;

	public AnimationClip unaimAnim;

	public AnimationClip fireAnim;

	public AnimationClip reloadAnim;

	public AnimationClip readyAnim;

	public AnimationClip hideAnim;

	public Camera weaponcamera;

	public Transform recoilCamera;

	private float nextField;

	private float weaponnextfield;

	public float runXrotation = 20f;

	public float runYrotation;

	public Vector3 runposition = Vector3.zero;

	private Vector3 wantedrotation;

	private bool canaim = true;

	private bool canfire = true;

	private bool canfire2 = true;

	private bool canreload = true;

	private bool retract;

	private bool isreloading;

	public Transform grenadethrower;

	public Transform player;

	public Transform meleeweapon;

	private playercontroller playercontrol;

	private weaponselector inventory;

	private camerarotate cameracontroller;

	private Animation myanimation;

	private bool isaiming;

	private bool lastaiming;

	public float zoomSliderVal;

	public bool zoom;
}

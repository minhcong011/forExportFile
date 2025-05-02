// dnSpy decompiler from Assembly-CSharp.dll class: machinegun
using System;
using System.Collections;
using ControlFreak2;
using UnityEngine;

public class machinegun : FPSCommonWeaponSpecs
{
	private void Awake()
	{
		this.weaponfirer = this.rayfirer.GetComponent<raycastfire>();
		this.playercontrol = this.player.GetComponent<playercontroller>();
		this.myanimation = base.GetComponent<Animation>();
		this.cameracontroller = this.recoilCamera.GetComponent<camerarotate>();
	}

	private void Start()
	{
		this.clipSize = this.currentammo;
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
		if (CF2Input.GetButton("Reload") && this.currentammo != this.clipSize && this.ammo > 0)
		{
			this.reload();
		}
		float num = this.speed * Time.deltaTime;
		float fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, this.nextField, Time.deltaTime * 2f);
		float fieldOfView2 = Mathf.Lerp(this.weaponcamera.fieldOfView, this.weaponnextfield, Time.deltaTime * 2f);
		Camera.main.fieldOfView = fieldOfView;
		this.weaponcamera.fieldOfView = fieldOfView2;
		this.weaponfirer.inaccuracy = this.inaccuracy;
		this.inventory.currentammo = this.currentammo;
		if (CF2Input.GetButton("ThrowGrenade") && !this.myanimation.isPlaying && this.inventory.grenade > 0 && this.canfire && Time.timeSinceLevelLoad > this.inventory.lastGrenade + 1f)
		{
			this.inventory.lastGrenade = Time.timeSinceLevelLoad;
			base.StartCoroutine(this.setThrowGrenade());
		}
		if (CF2Input.GetButton("Melee") && !this.myanimation.isPlaying && this.canfire)
		{
			base.StartCoroutine(this.setMelee());
		}
		if (this.retract)
		{
			this.canfire = false;
			this.canaim = false;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.retractPos, 5f * Time.deltaTime);
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
				this.inaccuracy = this.spreadAim;
				base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.aimposition, num);
				this.weaponnextfield = this.weaponaimFOV;
				this.nextField = this.aimFOV;
			}
			else
			{
				this.inaccuracy = this.spreadNormal;
				base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.normalposition, num);
				this.weaponnextfield = this.weaponnormalFOV;
				this.nextField = this.normalFOV;
			}
		}
		base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(this.wantedrotation), num * 3f);
		if (this.currentammo < 8)
		{
			if (this.currentammo == 6)
			{
				this.bullet8.gameObject.SetActive(false);
				this.nextbullet = this.bullet7;
			}
			else if (this.currentammo == 5)
			{
				this.bullet7.gameObject.SetActive(false);
				this.bullet8.gameObject.SetActive(false);
				this.nextbullet = this.bullet6;
			}
			else if (this.currentammo == 4)
			{
				this.nextbullet = this.bullet5;
				this.bullet8.gameObject.SetActive(false);
				this.bullet7.gameObject.SetActive(false);
				this.bullet6.gameObject.SetActive(false);
			}
			else if (this.currentammo == 3)
			{
				this.nextbullet = this.bullet4;
				this.bullet8.gameObject.SetActive(false);
				this.bullet7.gameObject.SetActive(false);
				this.bullet6.gameObject.SetActive(false);
				this.bullet5.gameObject.SetActive(false);
			}
			else if (this.currentammo == 2)
			{
				this.nextbullet = this.bullet3;
				this.bullet8.gameObject.SetActive(false);
				this.bullet7.gameObject.SetActive(false);
				this.bullet6.gameObject.SetActive(false);
				this.bullet5.gameObject.SetActive(false);
				this.bullet4.gameObject.SetActive(false);
			}
			else if (this.currentammo == 1)
			{
				this.nextbullet = this.bullet2;
				this.bullet8.gameObject.SetActive(false);
				this.bullet7.gameObject.SetActive(false);
				this.bullet6.gameObject.SetActive(false);
				this.bullet5.gameObject.SetActive(false);
				this.bullet4.gameObject.SetActive(false);
				this.bullet3.gameObject.SetActive(false);
			}
			else if (this.currentammo == 0 || this.currentammo <= 0)
			{
				this.nextbullet = this.bullet1;
				this.bullet8.gameObject.SetActive(false);
				this.bullet7.gameObject.SetActive(false);
				this.bullet6.gameObject.SetActive(false);
				this.bullet5.gameObject.SetActive(false);
				this.bullet4.gameObject.SetActive(false);
				this.bullet3.gameObject.SetActive(false);
				this.bullet2.gameObject.SetActive(false);
				this.bullet1.gameObject.SetActive(false);
				if (this.ammo <= 0)
				{
					this.canfire = false;
					this.canreload = false;
					if ((CF2Input.GetButton("Fire1") && this.canfire) || (double)CF2Input.GetAxis("Fire1") > 0.1 || this.weaponfirer.rayCastFirerAutoFireAim)
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
		}
		else
		{
			this.bullet8.gameObject.SetActive(true);
			this.bullet7.gameObject.SetActive(true);
			this.bullet6.gameObject.SetActive(true);
			this.bullet5.gameObject.SetActive(true);
			this.bullet4.gameObject.SetActive(true);
			this.bullet3.gameObject.SetActive(true);
			this.bullet2.gameObject.SetActive(true);
			this.bullet1.gameObject.SetActive(true);
			this.nextbullet = null;
		}
		if (!this.isreloading && this.canfire)
		{
			if (this.weaponnextfield == this.weaponaimFOV)
			{
				this.inventory.showAIM(false);
			}
			else
			{
				this.inventory.showAIM(true);
			}
			if (CF2Input.GetButton("Fire1") || (double)CF2Input.GetAxis("Fire1") > 0.1 || this.weaponfirer.rayCastFirerAutoFireAim)
			{
				this.fire();
			}
		}
		else
		{
			this.inventory.showAIM(false);
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
		if (this.weaponfirer == null)
		{
			this.weaponfirer = this.rayfirer.GetComponent<raycastfire>();
		}
		this.weaponfirer.inaccuracy = this.inaccuracy;
		this.weaponfirer.damage = this.damage;
		this.weaponfirer.range = this.range;
		this.weaponfirer.force = this.force;
		this.weaponfirer.projectilecount = this.projectilecount;
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
			this.myAudioSource.clip = this.readySound;
			this.myAudioSource.loop = false;
			this.myAudioSource.volume = 1f;
			this.myAudioSource.Play();
			this.myanimation.Play(this.readyAnim.name);
			this.canaim = true;
			this.canfire = true;
		}
	}

	private void fire()
	{
		if (!this.myanimation.isPlaying)
		{
			if (this.nextbullet != null)
			{
				this.nextbullet.gameObject.SetActive(false);
			}
			float num = UnityEngine.Random.Range(-0.05f, 0.05f);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, base.transform.localPosition.z + num);
			base.StartCoroutine(this.flashthemuzzle());
			this.weaponfirer.fire();
			this.fireAudioSource.clip = this.fireSound;
			this.fireAudioSource.pitch = 0.9f + 0.2f * UnityEngine.Random.value;
			this.fireAudioSource.Play();
			this.myanimation[this.fireAnim.name].speed = this.fireAnimSpeed;
			this.myanimation.Play(this.fireAnim.name);
			this.currentammo--;
			base.StartCoroutine(this.ejectshell(this.shellejectdelay));
			if (this.currentammo <= 0)
			{
				this.reload();
			}
		}
		else if (this.nextbullet != null)
		{
			this.nextbullet.gameObject.SetActive(true);
		}
	}

	private void reload()
	{
		if (!this.myanimation.isPlaying && this.canreload && !this.isreloading)
		{
			if ((this.currentammo > 6 || this.currentammo <= 0) && this.ammo >= 6)
			{
				base.StartCoroutine(this.setreload(this.myanimation[this.reloadAnim.name].length));
				this.myAudioSource.clip = this.reloadSound;
				this.myAudioSource.loop = false;
				this.myAudioSource.volume = 1f;
				this.myAudioSource.Play();
				this.myanimation.Play(this.reloadAnim.name);
			}
			else
			{
				base.StartCoroutine(this.setreload(this.myanimation[this.reload2Anim.name].length));
				this.myAudioSource.clip = this.reload2Sound;
				this.myAudioSource.loop = false;
				this.myAudioSource.volume = 1f;
				this.myAudioSource.Play();
				this.myanimation.Play(this.reload2Anim.name);
			}
		}
	}

	private void doNormal()
	{
		this.onstart();
	}

	private IEnumerator ejectshell(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		GameObject shellInstance = UnityEngine.Object.Instantiate<GameObject>(this.shell, this.shellPos.transform.position, this.shellPos.transform.rotation);
		shellInstance.GetComponent<Rigidbody>().AddRelativeForce(30f, 80f, 0f);
		shellInstance.GetComponent<Rigidbody>().AddRelativeTorque(500f, 20f, 800f);
		yield break;
	}

	private IEnumerator setreload(float waitTime)
	{
		this.playercontrol.canclimb = false;
		this.inventory.canswitch = false;
		int oldammo = this.currentammo;
		this.isreloading = true;
		this.canaim = false;
		yield return new WaitForSeconds(waitTime * 0.35f);
		this.currentammo = Mathf.Clamp(this.clipSize, this.clipSize - oldammo, this.ammo);
		this.ammo -= Mathf.Clamp(this.clipSize, this.clipSize, this.ammo);
		this.inventory.UpdateCurrentWeaponAmmo(this.ammo);
		yield return new WaitForSeconds(waitTime * 0.65f);
		this.isreloading = false;
		this.canaim = true;
		this.inventory.canswitch = true;
		this.playercontrol.canclimb = true;
		yield break;
	}

	private IEnumerator flashthemuzzle()
	{
		ParticleSystem smoke = this.muzzlesmoke.GetComponent<ParticleSystem>();
		smoke.Emit(1);
		this.muzzle.transform.localEulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(0f, 360f));
		this.muzzle.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.05f);
		this.muzzle.gameObject.SetActive(false);
		yield break;
	}

	private IEnumerator setThrowGrenade()
	{
		this.canfire = false;
		this.retract = true;
		this.grenadethrower.gameObject.SetActive(true);
		this.grenadethrower.gameObject.BroadcastMessage("throwstuff");
		Animation throwerAnimation = this.grenadethrower.GetComponent<Animation>();
		yield return new WaitForSeconds(throwerAnimation.clip.length);
		this.retract = false;
		this.canaim = true;
		this.canfire = true;
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
			this.canfire = false;
			this.meleeweapon.gameObject.SetActive(true);
			this.meleeweapon.gameObject.BroadcastMessage("melee");
			Animation meleeAnimation = this.meleeweapon.GetComponent<Animation>();
			yield return new WaitForSeconds(meleeAnimation.clip.length);
			this.retract = false;
			this.canaim = true;
			this.canfire = true;
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

	public AudioClip readySound;

	public AudioClip reloadSound;

	public AudioClip reload2Sound;

	public Transform bullet1;

	public Transform bullet2;

	public Transform bullet3;

	public Transform bullet4;

	public Transform bullet5;

	public Transform bullet6;

	public Transform bullet7;

	public Transform bullet8;

	private Transform nextbullet;

	public int projectilecount = 1;

	private float inaccuracy = 0.02f;

	public float spreadNormal = 0.08f;

	public float spreadAim = 0.02f;

	public float force = 500f;

	public float smoothdamping = 2f;

	public float recoil = 5f;

	public AnimationClip fireAnim;

	public AnimationClip reload2Anim;

	public AnimationClip reloadAnim;

	public AnimationClip readyAnim;

	public AnimationClip hideAnim;

	public GameObject shell;

	public Transform shellPos;

	public float shellejectdelay;

	public Transform muzzle;

	public Transform muzzlesmoke;

	public Camera weaponcamera;

	public Transform recoilCamera;

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

	public Transform rayfirer;

	public Transform player;

	public Transform meleeweapon;

	private raycastfire weaponfirer;

	private playercontroller playercontrol;

	private weaponselector inventory;

	private camerarotate cameracontroller;

	private Animation myanimation;

	public bool zoom;
}

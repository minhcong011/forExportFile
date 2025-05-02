// dnSpy decompiler from Assembly-CSharp.dll class: remoter
using System;
using System.Collections;
using ControlFreak2;
using UnityEngine;

public class remoter : FPSCommonWeaponSpecs
{
	private void Awake()
	{
		this.playercontrol = this.player.GetComponent<playercontroller>();
		this.myanimation = base.GetComponent<Animation>();
	}

	private void Start()
	{
		this.clipSize = this.currentammo;
		this.bombready = false;
		this.nextField = this.normalFOV;
		this.weaponnextfield = this.weaponnormalFOV;
		this.myanimation.Stop();
		this.onstart();
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
		if (CF2Input.GetButton("Fire1") || (double)CF2Input.GetAxis("Fire1") > 0.1)
		{
			if (this.bombready)
			{
				this.fire();
			}
			else
			{
				this.placebomb();
			}
		}
		if (this.ammo <= 0)
		{
			this.canreload = false;
			if ((CF2Input.GetButton("Fire1") || (double)CF2Input.GetAxis("Fire1") > 0.1) && !this.myAudioSource.isPlaying && !this.myAudioSource.isPlaying)
			{
				this.myAudioSource.PlayOneShot(this.emptySound);
			}
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
		this.handbomb.gameObject.SetActive(true);
		this.bombready = false;
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
		}
	}

	private void fire()
	{
		if (!this.myanimation.isPlaying)
		{
			base.StartCoroutine(this.setfire());
		}
	}

	private IEnumerator setfire()
	{
		if (this.bombInstance != null)
		{
			remotebomb component = this.bombInstance.GetComponent<remotebomb>();
			component.detonate();
		}
		this.fireAudioSource.clip = this.fireSound;
		this.fireAudioSource.pitch = 0.9f + 0.1f * UnityEngine.Random.value;
		this.fireAudioSource.Play();
		this.myanimation.Play(this.fireAnim.name);
		this.bombready = false;
		yield return new WaitForSeconds(this.myanimation[this.fireAnim.name].length);
		if (this.ammo > 0)
		{
			this.reload();
		}
		yield break;
	}

	private void reload()
	{
		if (this.canreload)
		{
			base.StartCoroutine(this.setreload(this.myanimation[this.readyAnim.name].length));
			this.myAudioSource.clip = this.reloadSound;
			this.myAudioSource.loop = false;
			this.myAudioSource.volume = 1f;
			this.myAudioSource.Play();
			this.myanimation.Play(this.readyAnim.name);
		}
	}

	private void doNormal()
	{
		this.onstart();
	}

	private void placebomb()
	{
		if (!this.myanimation.isPlaying)
		{
			base.StartCoroutine(this.setbomb(this.myanimation[this.placeAnim.name].length));
			this.fireAudioSource.clip = this.placeSound;
			this.fireAudioSource.pitch = 0.9f + 0.1f * UnityEngine.Random.value;
			this.fireAudioSource.Play();
			this.myanimation.Play(this.placeAnim.name);
			this.currentammo--;
		}
	}

	private IEnumerator setbomb(float waitTime)
	{
		yield return new WaitForSeconds(waitTime * 0.5f);
		this.handbomb.gameObject.SetActive(false);
		this.bombInstance = UnityEngine.Object.Instantiate<Transform>(this.bombprefab, this.bombposition.transform.position, this.bombposition.transform.rotation);
		Ray raycheck = new Ray(base.transform.position, Camera.main.transform.forward);
		RaycastHit hit = default(RaycastHit);
		if (Physics.Raycast(raycheck, out hit, 3f, this.mask))
		{
			Rigidbody component = this.bombInstance.GetComponent<Rigidbody>();
			UnityEngine.Object.Destroy(component);
			this.bombInstance.transform.parent = hit.transform;
			this.bombInstance.transform.position = hit.point;
			this.bombInstance.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
		}
		yield return new WaitForSeconds(waitTime * 0.5f);
		this.bombready = true;
		yield break;
	}

	private IEnumerator setreload(float waitTime)
	{
		this.playercontrol.canclimb = false;
		this.inventory.canswitch = false;
		int oldammo = this.currentammo;
		this.handbomb.gameObject.SetActive(true);
		yield return new WaitForSeconds(waitTime * 0.5f);
		this.currentammo = Mathf.Clamp(this.clipSize, this.clipSize - oldammo, this.ammo);
		this.ammo -= Mathf.Clamp(this.clipSize, this.clipSize, this.ammo);
		this.inventory.UpdateCurrentWeaponAmmo(this.ammo);
		yield return new WaitForSeconds(waitTime * 0.5f);
		this.inventory.canswitch = true;
		this.playercontrol.canclimb = true;
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
			this.canfire = true;
			this.meleeweapon.gameObject.SetActive(false);
		}
		yield break;
	}

	public Vector3 normalposition;

	public Vector3 retractPos;

	public AudioSource myAudioSource;

	public AudioSource fireAudioSource;

	public AudioClip emptySound;

	public AudioClip fireSound;

	public AudioClip placeSound;

	public AudioClip readySound;

	public AudioClip reloadSound;

	public float smoothdamping = 2f;

	public AnimationClip placeAnim;

	public AnimationClip fireAnim;

	public AnimationClip readyAnim;

	public AnimationClip hideAnim;

	public LayerMask mask;

	public Transform handbomb;

	public Transform bombprefab;

	public Transform bombposition;

	public Camera weaponcamera;

	public float runXrotation = 20f;

	public float runYrotation;

	public Vector3 runposition = Vector3.zero;

	private float nextField;

	private float weaponnextfield;

	private Vector3 wantedrotation;

	private Transform bombInstance;

	private bool canreload = true;

	private bool retract;

	private bool isreloading;

	public Transform player;

	public Transform grenadethrower;

	public Transform meleeweapon;

	private bool canfire = true;

	private playercontroller playercontrol;

	private weaponselector inventory;

	private bool bombready;

	private Animation myanimation;
}

// dnSpy decompiler from Assembly-CSharp.dll class: molotov
using System;
using System.Collections;
using ControlFreak2;
using UnityEngine;

public class molotov : MonoBehaviour
{
	private void Awake()
	{
		this.playercontrol = this.player.GetComponent<playercontroller>();
		this.myanimation = base.GetComponent<Animation>();
	}

	private void Start()
	{
		this.clipSize = this.currentammo;
		this.nextField = this.normalFOV;
		this.weaponnextfield = this.weaponnormalFOV;
		this.myanimation.Stop();
		this.onstart();
	}

	private void Update()
	{
		float num = this.speed * Time.deltaTime;
		float fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, this.nextField, Time.deltaTime * 2f);
		float fieldOfView2 = Mathf.Lerp(this.weaponcamera.fieldOfView, this.weaponnextfield, Time.deltaTime * 2f);
		Camera.main.fieldOfView = fieldOfView;
		this.weaponcamera.fieldOfView = fieldOfView2;
		this.inventory.currentammo = this.currentammo;
		if (this.retract)
		{
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.retractPos, num * 2f);
		}
		else
		{
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.normalposition, num);
		}
		if (CF2Input.GetButton("Melee") && this.myanimation.IsPlaying(this.idleAnim.name))
		{
			base.StartCoroutine(this.setMelee());
		}
		if (this.playercontrol.running)
		{
			this.wantedrotation = new Vector3(this.runXrotation, 0f, 0f);
		}
		else
		{
			this.wantedrotation = Vector3.zero;
		}
		base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(this.wantedrotation), num * 2f);
		if (this.ammo == 0 && this.currentammo <= 0)
		{
			this.canfire = false;
			if ((CF2Input.GetButton("Fire1") || (double)CF2Input.GetAxis("Fire1") > 0.1) && !this.myAudioSource.isPlaying)
			{
				this.myAudioSource.PlayOneShot(this.emptySound);
			}
		}
		else if (CF2Input.GetButton("Fire1") || ((double)CF2Input.GetAxis("Fire1") > 0.1 && this.myanimation.IsPlaying(this.idleAnim.name) && !this.isreloading && this.canfire))
		{
			this.myanimation.Stop(this.idleAnim.name);
			this.fire();
		}
		else if (!this.myanimation.isPlaying)
		{
			this.myanimation.CrossFade(this.idleAnim.name);
		}
	}

	private void doRetract()
	{
		if (this.canfire)
		{
			this.myanimation.Play(this.hideAnim.name);
		}
		else
		{
			this.retract = true;
		}
	}

	private void onstart()
	{
		this.retract = false;
		this.myAudioSource.Stop();
		this.fireAudioSource.Stop();
		this.bottleparticles.gameObject.SetActive(false);
		this.lighterparticles.gameObject.SetActive(false);
		if (this.inventory == null)
		{
			this.inventory = this.player.GetComponent<weaponselector>();
			this.inventory.InitCurrentWeaponAmmo(this.ammo);
		}
		this.inventory.showAIM(false);
		this.handmolotov.gameObject.SetActive(true);
		this.myanimation.Stop();
		if (this.ammo == 0 && this.currentammo <= 0)
		{
			this.myanimation.Play(this.emptyAnim.name);
			this.canfire = false;
		}
		else
		{
			this.myAudioSource.clip = this.readySound;
			this.myAudioSource.loop = false;
			this.myAudioSource.volume = 1f;
			this.myAudioSource.Play();
			this.myanimation.Play(this.readyAnim.name);
			this.canfire = true;
			this.myanimation[this.idleAnim.name].speed = this.idleAnimSpeed;
			this.myanimation.CrossFadeQueued(this.idleAnim.name);
		}
	}

	private void fire()
	{
		if (!this.myanimation.isPlaying)
		{
			this.fireAudioSource.clip = this.fireSound;
			this.fireAudioSource.pitch = 0.9f + 0.1f * UnityEngine.Random.value;
			this.fireAudioSource.Play();
			this.myanimation[this.fireAnim.name].speed = this.fireAnimSpeed;
			this.myanimation.Play(this.fireAnim.name);
			base.StartCoroutine(this.throwprojectile(this.myanimation[this.fireAnim.name].length));
		}
	}

	private void doNormal()
	{
		this.onstart();
	}

	private IEnumerator throwprojectile(float waitTime)
	{
		this.lighterparticles.gameObject.SetActive(true);
		yield return new WaitForSeconds(waitTime * 0.3f);
		this.lighterparticles.gameObject.SetActive(false);
		this.bottleparticles.gameObject.SetActive(true);
		yield return new WaitForSeconds(waitTime * 0.34f);
		this.handmolotov.gameObject.SetActive(false);
		this.currentammo -= this.clipSize;
		this.inventory.UpdateCurrentWeaponAmmo(this.ammo);
		GameObject grenadeInstance = UnityEngine.Object.Instantiate<GameObject>(this.projectile, base.transform.position, base.transform.rotation);
		grenadeInstance.GetComponent<Rigidbody>().AddRelativeForce(0f, this.throwforce / 4f, this.throwforce);
		yield return new WaitForSeconds(waitTime * 0.36f);
		this.handmolotov.gameObject.SetActive(true);
		this.bottleparticles.gameObject.SetActive(false);
		if (this.ammo > 0)
		{
			this.myAudioSource.clip = this.readySound;
			this.myAudioSource.loop = false;
			this.myAudioSource.volume = 1f;
			this.myAudioSource.Play();
			this.myanimation.Play(this.reloadAnim.name);
			this.playercontrol.canclimb = false;
			this.inventory.canswitch = false;
			this.isreloading = true;
			yield return new WaitForSeconds(this.myanimation[this.reloadAnim.name].length * 0.5f);
			this.currentammo = Mathf.Clamp(this.clipSize, 0, this.ammo);
			this.ammo -= Mathf.Clamp(this.clipSize, 0, this.ammo);
			this.inventory.UpdateCurrentWeaponAmmo(this.ammo);
			yield return new WaitForSeconds(this.myanimation[this.reloadAnim.name].length * 0.5f);
			this.isreloading = false;
			this.inventory.canswitch = true;
			this.playercontrol.canclimb = true;
		}
		else
		{
			this.myanimation.Play(this.emptyAnim.name);
			this.canfire = false;
		}
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

	public float normalFOV = 65f;

	public float weaponnormalFOV = 32f;

	public float speed = 3f;

	public AudioSource myAudioSource;

	public AudioSource fireAudioSource;

	public AudioClip fireSound;

	public AudioClip emptySound;

	public AudioClip readySound;

	public AnimationClip emptyAnim;

	public AnimationClip reloadAnim;

	public AnimationClip idleAnim;

	public float idleAnimSpeed = 0.4f;

	public AnimationClip fireAnim;

	public float fireAnimSpeed = 1.1f;

	public GameObject projectile;

	public float throwforce = 200f;

	public AnimationClip readyAnim;

	public AnimationClip hideAnim;

	public int ammo = 5;

	public int currentammo = 1;

	public int clipSize = 1;

	public Transform lighterparticles;

	public Transform bottleparticles;

	public Camera weaponcamera;

	public float runXrotation = 20f;

	public float runYrotation;

	public Vector3 runposition = Vector3.zero;

	private float nextField;

	private float weaponnextfield;

	private Vector3 wantedrotation;

	private bool canfire = true;

	private bool retract;

	private bool isreloading;

	public Transform grenadethrower;

	public Transform handmolotov;

	public Transform player;

	public Transform meleeweapon;

	private playercontroller playercontrol;

	private weaponselector inventory;

	private Animation myanimation;
}

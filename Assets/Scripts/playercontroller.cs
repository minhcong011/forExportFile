// dnSpy decompiler from Assembly-CSharp.dll class: playercontroller
using System;
using ControlFreak2;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
	private void Awake()
	{
		this.reference = new GameObject().transform;
		this.weaponanimator = this.weaponroot.GetComponent<Animator>();
		this.headanimator = this.head.GetComponent<Animator>();
		this.controller = base.GetComponent<CharacterController>();
		this.rotatescript = base.GetComponent<playerrotate>();
		this.inventory = base.GetComponent<weaponselector>();
	}

	private void Start()
	{
		this.speed = this.normalspeed;
		this.painflashtexture.CrossFadeAlpha(0f, 0f, true);
		this.cameranextposition = this.camerahighposition;
	}

	private void Update()
	{
		this.reference.eulerAngles = new Vector3(0f, this.mycamera.eulerAngles.y, 0f);
		this.forward = this.reference.forward;
		this.right = new Vector3(this.forward.z, 0f, -this.forward.x);
		float axisRaw = CF2Input.GetAxisRaw("Horizontal");
		float axisRaw2 = CF2Input.GetAxisRaw("Vertical");
		Vector3 velocity = this.controller.velocity;
		this.localvelocity = base.transform.InverseTransformDirection(velocity);
		bool flag = this.localvelocity.z > 0.5f;
		if (this.climbladder && !this.controller.isGrounded && this.canclimb)
		{
			this.inventory.hideweapons = true;
			this.airTime = 0f;
			this.crouching = false;
			if (this.localvelocity.magnitude / this.speed >= 0.1f)
			{
				this.myAudioSource.clip = this.climbloop;
				if (!this.myAudioSource.isPlaying)
				{
					this.myAudioSource.loop = true;
					this.myAudioSource.Play();
				}
			}
			else
			{
				this.myAudioSource.Stop();
			}
			Vector3 vector = this.ladderposition - base.transform.position;
			if (vector.magnitude > 0.05f)
			{
				this.addVector = vector.normalized;
			}
			else
			{
				this.addVector = Vector3.zero;
			}
			this.rotatescript.enabled = false;
			this.targetDirection = axisRaw2 * this.climbdirection;
			this.targetDirection = this.targetDirection.normalized;
			this.targetDirection += this.addVector;
			this.moveDirection = this.targetDirection * this.climbspeed;
			Quaternion b = Quaternion.LookRotation(this.ladderforward, Vector3.up);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime * 8f);
		}
		else
		{
			this.inventory.hideweapons = false;
			this.rotatescript.enabled = true;
			this.targetDirection = axisRaw * this.right + axisRaw2 * this.forward;
			this.targetDirection = this.targetDirection.normalized;
			this.targetVelocity = this.targetDirection;
			if (this.controller.isGrounded)
			{
				this.airTime = 0f;
				if (CF2Input.GetButtonDown("Crouch"))
				{
					this.crouchcheck();
				}
				if (!this.crouching)
				{
					this.canrun = true;
					this.controller.center = new Vector3(0f, this.normalHeight / 2f, 0f);
					this.controller.height = this.normalHeight;
					this.canrun2 = true;
					this.cameranextposition = this.camerahighposition;
					this.canclimb = true;
				}
				else if (this.crouching)
				{
					this.canrun = false;
					this.controller.center = new Vector3(0f, this.crouchHeight / 2f, 0f);
					this.controller.height = this.crouchHeight;
					this.canrun2 = false;
					this.cameranextposition = this.cameralowposition;
					this.canclimb = false;
				}
				if (CF2Input.GetButton("Jump") && Time.time > this.nextjump)
				{
					this.nextjump = Time.time + this.jumpinterval;
					this.moveDirection.y = this.jumpHeight;
					this.myAudioSource.PlayOneShot(this.jumpclip);
					this.weaponanimator.SetBool("jump", true);
					this.headanimator.SetBool("jump", true);
					if (this.crouching)
					{
						this.crouchcheck();
					}
				}
				else
				{
					this.weaponanimator.SetBool("jump", false);
					this.headanimator.SetBool("jump", false);
				}
				if (this.localvelocity.magnitude / this.speed >= 0.1f)
				{
					if (this.speed == this.runspeed)
					{
						if (this.myAudioSource.clip == this.walkloop || this.myAudioSource.clip == this.crouchloop)
						{
							this.myAudioSource.clip = this.runloop;
						}
					}
					else if (this.speed == this.crouchspeed)
					{
						if (this.myAudioSource.clip == this.walkloop || this.myAudioSource.clip == this.runloop)
						{
							this.myAudioSource.clip = this.crouchloop;
						}
					}
					else
					{
						this.myAudioSource.clip = this.walkloop;
					}
					if (!this.myAudioSource.isPlaying)
					{
						this.myAudioSource.loop = true;
						this.myAudioSource.Play();
					}
				}
				else
				{
					this.myAudioSource.Pause();
				}
			}
			else
			{
				this.airTime += Time.deltaTime;
				this.moveDirection.y = this.moveDirection.y - this.gravity * Time.deltaTime;
				this.nextjump = Time.time + this.jumpinterval;
				this.myAudioSource.Pause();
			}
			if (CF2Input.GetButton("Fire2") && this.canrun && this.canrun2 && flag)
			{
				this.speed = this.runspeed;
				this.running = true;
			}
			else if (this.crouching)
			{
				this.speed = this.crouchspeed;
				this.running = false;
			}
			else
			{
				this.speed = this.normalspeed;
				this.running = false;
			}
			this.targetVelocity *= this.speed;
			this.moveDirection.z = this.targetVelocity.z;
			this.moveDirection.x = this.targetVelocity.x;
		}
		if (this.hitpoints <= 0f)
		{
		}
		this.cameranewpositionY = Mathf.Lerp(Camera.main.transform.localPosition.y, this.cameranextposition, Time.deltaTime * 4f);
		this.weaponanimator.SetBool("grounded", this.controller.isGrounded);
		this.weaponanimator.SetFloat("speed", this.localvelocity.magnitude, this.dampTime, 0.1f);
		this.headanimator.SetBool("grounded", this.controller.isGrounded);
		this.headanimator.SetFloat("speed", this.localvelocity.magnitude, this.dampTime, 0.1f);
		this.cameranewposition = new Vector3(Camera.main.transform.localPosition.x, this.cameranewpositionY, Camera.main.transform.localPosition.z);
		Camera.main.transform.localPosition = this.cameranewposition;
		this.controller.Move(this.moveDirection * Time.deltaTime);
		if (!this.prevGrounded && this.controller.isGrounded)
		{
			if (this.airTime > this.falltreshold)
			{
				this.Damage(this.falldamage * this.airTime * 2f);
			}
			if (!this.myAudioSource.isPlaying && Time.time > this.nextcheck)
			{
				this.myAudioSource2.PlayOneShot(this.landclip);
			}
			this.nextcheck = Time.time + 0.8f;
		}
		else if (this.prevGrounded && !this.controller.isGrounded)
		{
			this.myAudioSource2.PlayOneShot(this.jumpclip);
		}
		this.prevGrounded = this.controller.isGrounded;
		if (this.hitpoints < this.maxhitpoints)
		{
			this.hitpoints += this.regen * Time.deltaTime;
		}
		string text = Mathf.Round(this.hitpoints / 10f).ToString();
		this.healthtext.text = text;
		float num = this.hitpoints / 1000f;
		this.painflashtexture.CrossFadeAlpha(1f - num, 0.5f, false);
	}

	private void Damage(float damage)
	{
		camerarotate component = this.recoilCamera.GetComponent<camerarotate>();
		if (!this.myAudioSource.isPlaying && this.hitpoints >= 0f)
		{
			int num = UnityEngine.Random.Range(1, this.hurtsounds.Length);
			this.myAudioSource2.clip = this.hurtsounds[num];
			this.myAudioSource2.pitch = 0.9f + 0.1f * UnityEngine.Random.value;
			this.myAudioSource2.Play();
			this.hurtsounds[num] = this.hurtsounds[0];
			this.hurtsounds[0] = this.myAudioSource2.clip;
		}
		this.hitpoints -= damage;
	}

	private void crouchcheck()
	{
		Ray ray = new Ray(base.transform.position, Vector3.up);
		RaycastHit raycastHit = default(RaycastHit);
		if (!Physics.Raycast(ray, out raycastHit, 2.2f, this.mask))
		{
			this.crouching = !this.crouching;
		}
	}

	public Transform mycamera;

	private Transform reference;

	public float jumpHeight = 2f;

	public float jumpinterval = 1.5f;

	private float nextjump = 1.2f;

	private float maxhitpoints = 1000f;

	private float hitpoints = 1000f;

	public float regen = 100f;

	public Text healthtext;

	public AudioClip[] hurtsounds;

	public RawImage painflashtexture;

	private float alpha;

	public Transform recoilCamera;

	public float gravity = 20f;

	public float rotatespeed = 4f;

	private float speed;

	public float normalspeed = 4f;

	public float runspeed = 8f;

	public float crouchspeed = 1f;

	public float crouchHeight = 1f;

	private bool crouching;

	public float normalHeight = 2f;

	public float camerahighposition = 1.75f;

	public float cameralowposition = 0.9f;

	private float cameranewpositionY;

	private Vector3 cameranewposition;

	private float cameranextposition;

	public float dampTime = 2f;

	private float moveAmount;

	public float smoothSpeed = 2f;

	private Vector3 forward = Vector3.forward;

	private Vector3 moveDirection = Vector3.zero;

	private Vector3 right;

	private float movespeed;

	public Vector3 localvelocity;

	public bool climbladder;

	public Quaternion ladderRotation;

	public Vector3 ladderposition;

	public Vector3 ladderforward;

	public Vector3 climbdirection;

	public float climbspeed = 2f;

	public bool canclimb = true;

	private Vector3 addVector = Vector3.zero;

	public bool running;

	public bool canrun = true;

	public AudioSource myAudioSource;

	public AudioSource myAudioSource2;

	public AudioClip walkloop;

	public AudioClip runloop;

	public AudioClip crouchloop;

	public AudioClip climbloop;

	public AudioClip jumpclip;

	public AudioClip landclip;

	private Vector3 targetDirection = Vector3.zero;

	private bool canrun2 = true;

	public bool hideselectedweapon;

	private Vector3 targetVelocity;

	public float falldamage;

	private float airTime;

	public float falltreshold = 2f;

	private bool prevGrounded;

	public Transform Deadplayer;

	public Transform weaponroot;

	private Animator weaponanimator;

	public Transform head;

	private Animator headanimator;

	public LayerMask mask;

	private CharacterController controller;

	private playerrotate rotatescript;

	private weaponselector inventory;

	private float nextcheck;
}

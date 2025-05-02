// dnSpy decompiler from Assembly-CSharp.dll class: NPCAttack
using System;
using UnityEngine;

public class NPCAttack : MonoBehaviour
{
	private void Start()
	{
		this.myTransform = base.transform;
		if (this.muzzleFlash)
		{
			this.muzzleFlashTransform = this.muzzleFlash.transform;
		}
		this.AIComponent = this.myTransform.GetComponent<AI>();
		this.aSource = base.GetComponent<AudioSource>();
		this.shootStartTime = -this.fireRate * 2f;
	}

	private void Update()
	{
		if (this.shootStartTime + this.fireRate < Time.time)
		{
			this.shooting = false;
		}
		if (!this.doneShooting && this.shotsFired < this.burstShots + this.randShotsAmt && (this.AIComponent.target == this.AIComponent.playerTransform || (this.AIComponent.TargetAIComponent && this.AIComponent.TargetAIComponent.enabled)))
		{
			this.Fire();
			if (!this.randBurstState)
			{
				this.randShotsAmt = UnityEngine.Random.Range(0, this.randomShots);
				this.randBurstState = true;
			}
		}
		else
		{
			this.doneShooting = true;
			this.shotsFired = 0;
			this.randBurstState = false;
		}
	}

	private void LateUpdate()
	{
		if (this.muzzleFlash)
		{
			if ((double)(Time.time - this.shootStartTime) < (double)this.fireRate * 0.33)
			{
				if (this.mFlashState)
				{
					this.muzzleFlash.enabled = true;
					this.mFlashState = false;
				}
			}
			else if (!this.mFlashState)
			{
				this.muzzleFlash.enabled = false;
			}
		}
	}

	public void Fire()
	{
		if (!this.reloading)
		{
			if (!this.shooting)
			{
				this.FireOneShot();
				this.shootStartTime = Time.time;
				this.shooting = true;
				this.doneShooting = false;
			}
			else if (this.shootStartTime + this.fireRate < Time.time)
			{
				this.shooting = false;
			}
		}
		else
		{
			this.shooting = false;
		}
	}

	private void FireOneShot()
	{
		if (this.AIComponent.TargetAIComponent)
		{
			if (Vector3.Distance(this.myTransform.position, this.AIComponent.lastVisibleTargetPosition) > 2.5f)
			{
				this.targetPos = new Vector3(this.AIComponent.lastVisibleTargetPosition.x + UnityEngine.Random.Range(-this.inaccuracy, this.inaccuracy), this.AIComponent.lastVisibleTargetPosition.y + UnityEngine.Random.Range(-this.inaccuracy, this.inaccuracy), this.AIComponent.lastVisibleTargetPosition.z + UnityEngine.Random.Range(-this.inaccuracy, this.inaccuracy));
			}
			else
			{
				this.targetPos = new Vector3(this.AIComponent.lastVisibleTargetPosition.x, this.AIComponent.lastVisibleTargetPosition.y, this.AIComponent.lastVisibleTargetPosition.z);
			}
		}
		else if (Vector3.Distance(this.myTransform.position, this.AIComponent.lastVisibleTargetPosition) > 2.5f)
		{
			this.targetPos = new Vector3(this.AIComponent.lastVisibleTargetPosition.x + UnityEngine.Random.Range(-this.inaccuracy, this.inaccuracy), this.AIComponent.lastVisibleTargetPosition.y + UnityEngine.Random.Range(-this.inaccuracy, this.inaccuracy), this.AIComponent.lastVisibleTargetPosition.z + UnityEngine.Random.Range(-this.inaccuracy, this.inaccuracy)) + this.AIComponent.playerObj.transform.up * this.AIComponent.targetEyeHeight;
		}
		else
		{
			this.targetPos = new Vector3(this.AIComponent.lastVisibleTargetPosition.x, this.AIComponent.lastVisibleTargetPosition.y, this.AIComponent.lastVisibleTargetPosition.z);
		}
		this.rayOrigin = new Vector3(this.myTransform.position.x, this.myTransform.position.y + this.AIComponent.eyeHeight, this.myTransform.position.z);
		this.targetDir = (this.targetPos - this.rayOrigin).normalized;
		if (this.muzzleFlashTransform)
		{
		}
		this.hitObject = false;
		RaycastHit raycastHit = default(RaycastHit);
		this.hits = Physics.RaycastAll(this.rayOrigin, this.targetDir, Vector3.Distance(this.rayOrigin, this.targetPos), this.AIComponent.searchMask);
		for (int i = 0; i < this.hits.Length; i++)
		{
			if (!this.hits[i].transform.IsChildOf(this.myTransform))
			{
				this.hitObject = true;
				raycastHit = this.hits[i];
				break;
			}
		}
		if (this.hitObject)
		{
			if (raycastHit.rigidbody)
			{
				raycastHit.rigidbody.AddForceAtPosition(this.force * this.targetDir / (Time.fixedDeltaTime * 100f), raycastHit.point);
			}
			if (raycastHit.collider)
			{
				if (raycastHit.collider.gameObject.layer == 20 || raycastHit.collider.gameObject.layer == 11 || !this.isMeleeAttack)
				{
				}
				this.damageAmt = UnityEngine.Random.Range(this.damage, this.damage + this.damage);
				int layer = raycastHit.collider.gameObject.layer;
				switch (layer)
				{
				case 11:
					break;
				default:
					if (layer != 0)
					{
						if (layer != 20)
						{
						}
					}
					break;
				case 13:
					if (raycastHit.collider.gameObject.GetComponent<CharacterDamage>())
					{
						raycastHit.collider.gameObject.GetComponent<CharacterDamage>().ApplyDamage(this.damageAmt, Vector3.zero, this.myTransform.position, this.myTransform, false, false, null, 0f);
					}
					if (raycastHit.collider.gameObject.GetComponent<LocationDamage>())
					{
						raycastHit.collider.gameObject.GetComponent<LocationDamage>().ApplyDamage(this.damageAmt, Vector3.zero, this.myTransform.position, this.myTransform, false, false);
					}
					break;
				}
			}
		}
		this.aSource.clip = this.firesnd;
		this.aSource.pitch = UnityEngine.Random.Range(this.fireFxRandPitch, 1f);
		if (this.aSource.volume > 0f)
		{
			this.aSource.PlayOneShot(this.aSource.clip, 0.8f / this.aSource.volume);
		}
		this.shotsFired++;
		this.mFlashState = true;
		base.enabled = true;
	}

	private AI AIComponent;

	private GameObject playerObj;

	private Transform myTransform;

	private Vector3 targetPos;

	[Tooltip("Maximum range of NPC attack.")]
	public float range = 100f;

	[Tooltip("Random range in units around target that NPC attack will hit (so NPCs won't have perfect aim).")]
	public float inaccuracy = 0.5f;

	[Tooltip("Fire rate of NPC attack.")]
	public float fireRate = 0.097f;

	[Tooltip("Number of attacks to fire in quick succession during NPC attack (for automatic weapons).")]
	public int burstShots;

	[Tooltip("Maximum number of random shots to add to end of attack (so automatic weapons will fire different number of bullets per attack).")]
	public int randomShots;

	[Tooltip("Physics force to apply to collider hit by NPC attack.")]
	public float force = 20f;

	[Tooltip("Damage to inflict per NPC attack.")]
	public float damage = 10f;

	[Tooltip("True if this is a melee attack (so actions like blocking can identify attack type).")]
	public bool isMeleeAttack;

	private float damageAmt;

	private bool doneShooting = true;

	private int shotsFired;

	private bool randBurstState;

	private int randShotsAmt;

	private bool shooting;

	private bool reloading;

	private bool mFlashState;

	private Vector3 rayOrigin;

	private Vector3 targetDir;

	private RaycastHit[] hits;

	private bool hitObject;

	[Tooltip("Muzzle flash object to display during NPC attacks.")]
	public Renderer muzzleFlash;

	private Transform muzzleFlashTransform;

	[Tooltip("Sound effect for NPC attack.")]
	public AudioClip firesnd;

	[Tooltip("Random pitch chosen between this value and 1.0 for NPC attack sound variety.")]
	public float fireFxRandPitch = 0.86f;

	private AudioSource aSource;

	private float shootStartTime;
}

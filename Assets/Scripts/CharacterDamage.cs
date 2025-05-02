// dnSpy decompiler from Assembly-CSharp.dll class: CharacterDamage
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterDamage : MonoBehaviour
{
	private void OnEnable()
	{
		this.myTransform = base.transform;
		this.RemoveBodyComponent = base.GetComponent<RemoveBody>();
		Mathf.Clamp01(this.sloMoDeathChance);
		if (this.removeBody && this.RemoveBodyComponent)
		{
			this.RemoveBodyComponent.enabled = false;
		}
		if (!this.AIComponent)
		{
			this.AIComponent = this.myTransform.GetComponent<AI>();
		}
		this.initialHitPoints = this.hitPoints;
		this.bodies = base.GetComponentsInChildren<Rigidbody>();
	}

	private void Update()
	{
		if (this.bodies.Length > 1)
		{
			if (!this.ragdollActive)
			{
				if (!this.ragdollState)
				{
					foreach (Rigidbody rigidbody in this.bodies)
					{
						rigidbody.isKinematic = true;
					}
					if (this.AIComponent != null)
					{
						this.AIComponent.AnimatorComponent.enabled = true;
					}
					if (this.gunObj)
					{
						this.gunObj.gameObject.SetActive(true);
						if (this.gunInst)
						{
							UnityEngine.Object.Destroy(this.gunInst);
						}
					}
					this.ragdollState = true;
				}
			}
			else if (this.ragdollState)
			{
				this.AIComponent.AnimatorComponent.enabled = false;
				foreach (Collider collider in this.AIComponent.colliders)
				{
					if (this.AIComponent.playerTransform.gameObject.activeInHierarchy)
					{
						Physics.IgnoreCollision(collider, this.AIComponent.playerTransform.GetComponent<CapsuleCollider>(), true);
					}
				}
				foreach (Rigidbody rigidbody2 in this.bodies)
				{
					rigidbody2.isKinematic = false;
				}
				if (this.gunObj)
				{
					this.gunObj.gameObject.SetActive(false);
					this.gunInst = UnityEngine.Object.Instantiate<GameObject>(this.gunItem, this.gunObj.position, this.gunObj.rotation);
					foreach (Collider collider2 in this.AIComponent.colliders)
					{
						Physics.IgnoreCollision(collider2, this.gunInst.GetComponent<Collider>(), true);
					}
					this.gunInst.transform.Rotate(this.gunItemRotation);
					Vector3 position = this.gunInst.transform.position + base.transform.forward * 0.45f;
					this.gunInst.transform.position = position;
				}
				this.ragdollState = false;
			}
		}
	}

	public void ApplyDamage(float damage, Vector3 attackDir, Vector3 attackerPos, Transform attacker, bool isPlayer, bool isExplosion, Rigidbody hitBody = null, float bodyForce = 0f)
	{
		if (this.hitPoints <= 0f)
		{
			return;
		}
		if (!this.AIComponent.damaged && !this.AIComponent.huntPlayer && this.hitPoints / this.initialHitPoints < 0.65f && attacker && !isExplosion)
		{
			if (!isPlayer)
			{
				if (attacker.GetComponent<AI>().factionNum != this.AIComponent.factionNum)
				{
					this.AIComponent.target = attacker;
					this.AIComponent.TargetAIComponent = attacker.GetComponent<AI>();
					this.AIComponent.targetEyeHeight = this.AIComponent.TargetAIComponent.eyeHeight;
					this.AIComponent.damaged = true;
				}
			}
			else if (!this.AIComponent.ignoreFriendlyFire)
			{
				this.AIComponent.target = this.AIComponent.playerObj.transform;
				this.AIComponent.targetEyeHeight = 0.45f;
				this.AIComponent.playerAttacked = true;
				this.AIComponent.TargetAIComponent = null;
				this.AIComponent.damaged = true;
			}
		}
		if (this.hitPoints - damage > 0f)
		{
			if (this.AIComponent.playerIsBehind)
			{
				this.hitPoints -= damage * 32f;
			}
			else
			{
				this.hitPoints -= damage;
			}
		}
		else
		{
			this.hitPoints = 0f;
		}
		this.attackDir2 = attackDir;
		this.attackerPos2 = attackerPos;
		this.explosionCheck = isExplosion;
		this.AIComponent.attackedTime = Time.time;
		if (this.hitPoints <= 0f)
		{
			this.onDie.Invoke();
			this.AIComponent.vocalFx.Stop();
			if (this.bodies.Length < 2)
			{
				this.Die();
			}
			else if (!this.ragdollActive)
			{
				this.RagDollDie(hitBody, bodyForce);
			}
		}
	}

	private void RagDollDie(Rigidbody hitBody, float bodyForce)
	{
		if (this.dieSound)
		{
		}
		this.AIComponent.AnimatorComponent.enabled = false;
		this.ragdollActive = true;
		if (this.AIComponent.NPCAttackComponent.muzzleFlash)
		{
			this.AIComponent.NPCAttackComponent.muzzleFlash.enabled = false;
		}
		this.AIComponent.NPCAttackComponent.enabled = false;
		this.AIComponent.StopAllCoroutines();
		this.AIComponent.agent.enabled = false;
		this.AIComponent.enabled = false;
		base.StartCoroutine(this.ApplyForce(hitBody, bodyForce));
		if (this.RemoveBodyComponent)
		{
			if (this.removeBody)
			{
				this.RemoveBodyComponent.enabled = true;
				this.RemoveBodyComponent.bodyStayTime = this.bodyStayTime;
			}
			else
			{
				this.RemoveBodyComponent.enabled = false;
			}
		}
	}

	public IEnumerator ApplyForce(Rigidbody body, float force)
	{
		yield return new WaitForSeconds(0.02f);
		if (!this.explosionCheck)
		{
			body.AddForce(this.attackDir2 * this.attackForce, ForceMode.Impulse);
		}
		else
		{
			foreach (Rigidbody rigidbody in this.bodies)
			{
				rigidbody.AddForce((this.myTransform.position - (this.attackerPos2 + Vector3.up * -2.5f)).normalized * UnityEngine.Random.Range(2.5f, 4.5f), ForceMode.Impulse);
			}
		}
		yield break;
	}

	private void Die()
	{
		if (this.dieSound)
		{
		}
		this.AIComponent.agent.Stop();
		this.AIComponent.StopAllCoroutines();
		if (this.deadReplacement)
		{
			this.dead = UnityEngine.Object.Instantiate<Transform>(this.deadReplacement, base.transform.position, base.transform.rotation);
			this.RemoveBodyComponent = this.dead.GetComponent<RemoveBody>();
			CharacterDamage.CopyTransformsRecurse(base.transform, this.dead);
			RaycastHit raycastHit;
			if (Physics.SphereCast(this.attackerPos2, 0.2f, this.attackDir2, out raycastHit, 750f, this.raymask) && raycastHit.rigidbody && this.attackDir2.x != 0f)
			{
				raycastHit.rigidbody.AddForce(this.attackDir2 * 10f, ForceMode.Impulse);
			}
			else
			{
				Component[] componentsInChildren = this.dead.GetComponentsInChildren<Rigidbody>();
				foreach (Rigidbody rigidbody in componentsInChildren)
				{
					if (this.explosionCheck)
					{
						rigidbody.AddForce((this.myTransform.position - (this.attackerPos2 + Vector3.up * -2.5f)).normalized * UnityEngine.Random.Range(4.5f, 7.5f), ForceMode.Impulse);
					}
					else if (rigidbody.transform.name == "Chest")
					{
						rigidbody.AddForce((this.myTransform.position - this.attackerPos2).normalized * 10f, ForceMode.Impulse);
					}
				}
			}
			if (this.RemoveBodyComponent)
			{
				if (this.removeBody)
				{
					this.RemoveBodyComponent.enabled = true;
					this.RemoveBodyComponent.bodyStayTime = this.bodyStayTime;
				}
				else
				{
					this.RemoveBodyComponent.enabled = false;
				}
			}
			if (this.notParent)
			{
				UnityEngine.Object.Destroy(base.transform.parent.gameObject);
			}
			else
			{
				UnityEngine.Object.Destroy(base.transform.gameObject);
			}
		}
	}

	private static void CopyTransformsRecurse(Transform src, Transform dst)
	{
		dst.position = src.position;
		dst.rotation = src.rotation;
		IEnumerator enumerator = dst.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				Transform transform2 = src.Find(transform.name);
				if (transform2)
				{
					CharacterDamage.CopyTransformsRecurse(transform2, transform);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public UnityEvent onDie;

	private AI AIComponent;

	private RemoveBody RemoveBodyComponent;

	[Tooltip("Number of hitpoints for this character or body part.")]
	public float hitPoints = 100f;

	private float initialHitPoints;

	[Tooltip("Force to apply to this collider when NPC is killed.")]
	public float attackForce = 2.75f;

	[Tooltip("Item to spawn when NPC is killed.")]
	public GameObject gunItem;

	[Tooltip("Weapon mesh to hide when NPC dies (replaced with usable gun item).")]
	public Transform gunObj;

	private GameObject gunInst;

	[Tooltip("Rotation of spawed gun object after NPC death.")]
	public Vector3 gunItemRotation = new Vector3(0f, 180f, 180f);

	private Rigidbody[] bodies;

	[Tooltip("True if ragdoll mode is active for this NPC.")]
	public bool ragdollActive;

	private bool ragdollState;

	[Tooltip("If NPC only has one capsule collider for hit detection, replace the NPC's character mesh with a ragdoll, instead of transitioning instantly to ragdoll.")]
	public Transform deadReplacement;

	private Transform dead;

	[Tooltip("Sound effect to play when NPC dies.")]
	public AudioClip dieSound;

	[Tooltip("Determine if this object or parent should be removed on death. This is to allow for different hit detection collider types as children of NPC parent.")]
	public bool notParent;

	[Tooltip("Should this NPC's body be removed after Body Stay Time?")]
	public bool removeBody;

	[Tooltip("Time for body to stay in the scene before it is removed.")]
	public float bodyStayTime = 15f;

	[Tooltip("Time for dropped weapon item to stay in scene before it is removed.")]
	public float gunStayTime = -1f;

	[Tooltip("Chance between 0 and 1 that death of this NPC will trigger slow motion for a few seconds (regardless of the body part hit).")]
	[Range(0f, 1f)]
	public float sloMoDeathChance;

	[Tooltip("True if backstabbing this NPC should trigger slow motion for the duration of slo mo backstab time.")]
	public bool sloMoBackstab = true;

	[Tooltip("Duration of slow motion time in seconds if slo mo death chance check is successful.")]
	public float sloMoDeathTime = 0.9f;

	[Tooltip("Duration of slow motion time in seconds if this NPC is backstabbed.")]
	public float sloMoBackstabTime = 0.9f;

	private Vector3 attackerPos2;

	private Vector3 attackDir2;

	private Transform myTransform;

	private bool explosionCheck;

	private LayerMask raymask = 8192;
}

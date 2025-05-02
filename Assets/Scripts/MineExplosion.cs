// dnSpy decompiler from Assembly-CSharp.dll class: MineExplosion
using System;
using System.Collections;
using UnityEngine;

public class MineExplosion : MonoBehaviour
{
	private void Start()
	{
		this.myTransform = base.transform;
		if (!this.isRadiusCollider)
		{
			this.radius = this.myTransform.GetComponentInChildren<SphereCollider>().radius;
			this.AlignToGround();
		}
	}

	private void AlignToGround()
	{
		if (Physics.Raycast(this.myTransform.position, -base.transform.up, out this.hitInit, 2f, this.initPosMask.value))
		{
			this.myTransform.rotation = Quaternion.FromToRotation(Vector3.up, this.hitInit.normal);
			this.myTransform.position = this.hitInit.point;
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (this.isRadiusCollider && !col.isTrigger && (col.gameObject.layer == 11 || col.gameObject.layer == 13 || col.attachedRigidbody != null) && !this.detonated)
		{
			this.detonated = true;
			this.myTransform.parent.transform.GetComponent<MineExplosion>().triggered = true;
			this.myTransform.parent.transform.GetComponent<MineExplosion>().detonated = true;
			this.myTransform.GetComponent<SphereCollider>().enabled = false;
			this.myTransform.parent.transform.GetComponent<MineExplosion>().StartCoroutine("Detonate");
		}
	}

	private IEnumerator DetectDestroyed()
	{
		while (this.isRadiusCollider || !this.audioPlayed || base.GetComponent<AudioSource>().isPlaying)
		{
			yield return new WaitForSeconds(0.5f);
		}
		UnityEngine.Object.Destroy(this.myTransform.gameObject);
		yield break;
		yield break;
	}

	private IEnumerator Detonate()
	{
		if (this.triggered)
		{
			if (this.beepFx)
			{
				base.GetComponent<AudioSource>().clip = this.beepFx;
				base.GetComponent<AudioSource>().Play();
			}
			yield return new WaitForSeconds(this.damageDelay);
		}
		MonoBehaviour.print("exploding");
		this.explosionObj = this.explosion;
		this.explosionObj.transform.position = this.myTransform.position;
		IEnumerator enumerator = this.explosionObj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.GetComponent<ParticleSystem>())
				{
					this.partSys = transform.GetComponent<ParticleSystem>();
					this.partSys.Emit(Mathf.RoundToInt(this.partSys.emissionRate));
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
		this.myTransform.GetComponent<MeshRenderer>().enabled = false;
		base.GetComponent<AudioSource>().clip = this.explosionFX;
		base.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.75f * Time.timeScale, 1f * Time.timeScale);
		base.GetComponent<AudioSource>().Play();
		this.audioPlayed = true;
		if (!this.triggered)
		{
			yield return new WaitForSeconds(this.damageDelay);
		}
		Collider[] hitColliders = Physics.OverlapSphere(this.myTransform.position, this.radius * 1.5f, this.blastMask);
		for (int i = 0; i < hitColliders.Length; i++)
		{
			Transform transform2 = hitColliders[i].transform;
			if (transform2 != this.myTransform && Physics.Linecast(transform2.position, this.myTransform.position, out this.hit, this.blastMask) && this.hit.collider == this.myTransform.GetComponent<Collider>())
			{
				int layer = hitColliders[i].GetComponent<Collider>().gameObject.layer;
				switch (layer)
				{
				case 11:
					break;
				default:
					if (layer != 0)
					{
					}
					break;
				case 13:
					if (hitColliders[i].GetComponent<Collider>().gameObject.GetComponent<CharacterDamage>())
					{
						hitColliders[i].GetComponent<Collider>().gameObject.GetComponent<CharacterDamage>().ApplyDamage(this.explosionDamage, Vector3.zero, this.myTransform.position, null, false, true, null, 0f);
					}
					if (hitColliders[i].GetComponent<Collider>().gameObject.GetComponent<LocationDamage>())
					{
						hitColliders[i].GetComponent<Collider>().gameObject.GetComponent<LocationDamage>().ApplyDamage(this.explosionDamage, Vector3.zero, this.myTransform.position, null, false, true);
					}
					break;
				}
				if (transform2.GetComponent<Rigidbody>())
				{
					transform2.GetComponent<Rigidbody>().AddExplosionForce(this.blastForce * transform2.GetComponent<Rigidbody>().mass, this.myTransform.position, this.radius, 3f, ForceMode.Impulse);
				}
			}
			if (i >= hitColliders.Length - 1)
			{
				this.myTransform.GetComponent<BoxCollider>().enabled = false;
			}
		}
		yield break;
	}

	public void ApplyDamage(float damage)
	{
		if (!this.isRadiusCollider && !this.detonated)
		{
			this.detonated = true;
			this.myTransform.GetComponentInChildren<SphereCollider>().enabled = false;
			base.StartCoroutine(this.Detonate());
			base.StartCoroutine(this.DetectDestroyed());
		}
	}

	[Tooltip("Damage dealt by explosion.")]
	public float explosionDamage = 200f;

	[Tooltip("Delay before this object applies explosion force and damage to other objects;.")]
	public float damageDelay = 0.2f;

	[Tooltip("Explosive physics force applied to objects in blast radius.")]
	public float blastForce = 15f;

	[Tooltip("Explosion sound effect.")]
	public AudioClip explosionFX;

	[Tooltip("mine trigger sound effect.")]
	public AudioClip beepFx;

	[Tooltip("Radius of explosion.")]
	private float radius;

	[Tooltip("True if object is the mine detection radius object (used only for triggering mine, not explosion effects).")]
	public bool isRadiusCollider;

	[Tooltip("Layers that will be hit by explosion.")]
	public LayerMask blastMask;

	[Tooltip("Layers that mine will auto-align angles to surface on scene start.")]
	public LayerMask initPosMask;

	private Transform myTransform;

	private bool audioPlayed;

	private bool triggered;

	private bool detonated;

	private bool inPosition;

	private RaycastHit hit;

	private RaycastHit hitInit;

	private Vector3 explodePos;

	private ParticleSystem partSys;

	public GameObject explosion;

	private GameObject explosionObj;
}

// dnSpy decompiler from Assembly-CSharp.dll class: Damage_Tps
using System;
using System.Collections;
using UnityEngine;

public class Damage_Tps : DamageBase
{
	private void Start()
	{
		if (!this.Owner || !this.Owner.GetComponent<Collider>())
		{
			return;
		}
		if (base.GetComponent<Collider>().enabled && this.Owner.GetComponent<Collider>().enabled)
		{
			Physics.IgnoreCollision(base.GetComponent<Collider>(), this.Owner.GetComponent<Collider>());
		}
	}

	private void OnEnable()
	{
		this.timetemp = Time.time;
		this.isCollided = false;
		base.Invoke("resendToPool", 4f);
		if (base.GetComponent<MeshRenderer>() != null)
		{
			base.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	public void resendToPool()
	{
		if (!this.isPooling)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		ObjectPool.instance.PoolObject(base.gameObject);
		if (base.GetComponent<Rigidbody>() != null)
		{
			base.GetComponent<Rigidbody>().velocity = Vector3.zero;
			base.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
		if (base.GetComponent<TrailRenderer>() != null)
		{
			base.GetComponent<TrailRenderer>().Clear();
		}
		this.isCollided = false;
		base.gameObject.transform.position = Vector3.zero;
		if (base.GetComponent<Rigidbody>() != null)
		{
			base.GetComponent<Rigidbody>().velocity = Vector3.zero;
			base.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
	}

	private void OnDisable()
	{
		base.CancelInvoke("resendToPool");
		if (!this.isPooling)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void ApplyEffect(Collision coll)
	{
		Quaternion rotation = Quaternion.FromToRotation(Vector3.up, coll.contacts[0].point);
		string tag = coll.gameObject.tag;
		if (tag != null)
		{
			if (tag == "Ground")
			{
				return;
			}
			if (tag == "Enemy")
			{
				GameObject objectForType = ObjectPool.instance.GetObjectForType("EnemyHitEffect_Pool", false);
				if (objectForType != null)
				{
					objectForType.transform.position = coll.contacts[0].point;
					objectForType.transform.rotation = rotation;
					objectForType.transform.SetParent(coll.transform);
					objectForType.SetActive(true);
				}
				return;
			}
		}
		rotation = Quaternion.FromToRotation(Vector3.up, coll.contacts[0].normal);
		GameObject objectForType2 = ObjectPool.instance.GetObjectForType("MetalDecalPool", false);
		if (objectForType2 != null)
		{
			objectForType2.transform.position = coll.contacts[0].point;
			objectForType2.transform.rotation = rotation;
			objectForType2.transform.SetParent(coll.transform);
			objectForType2.SetActive(true);
		}
	}

	public void Active()
	{
		if (this.Effect)
		{
			GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.Effect, base.transform.position, base.transform.rotation);
			UnityEngine.Object.Destroy(obj, 2f);
		}
		if (this.Explosive)
		{
			base.StartCoroutine(this.ExplosionDamage());
		}
		else
		{
			this.resendToPool();
		}
	}

	private IEnumerator ExplosionDamage()
	{
		MonoBehaviour.print("Uk : Yes");
		if (base.GetComponent<MeshRenderer>() != null)
		{
			base.GetComponent<MeshRenderer>().enabled = false;
		}
		foreach (Collider hit in Physics.OverlapSphere(base.transform.position, this.ExplosionRadius))
		{
			if (hit)
			{
				if (hit.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.tag == "Enemy")
				{
					hit.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.GetComponent<CommonEnemyBehaviour>().ApplyDamage(200f, "normal", 0.25f, true);
					if (hit.GetComponent<Rigidbody>())
					{
						hit.GetComponent<Rigidbody>().AddExplosionForce(200f, base.transform.position, this.ExplosionRadius, 3f);
					}
					yield return new WaitForSeconds(0.03f);
				}
				else if (hit.GetComponent<Collider>().gameObject.tag == this.TargetTag[1] && this.isEnemyBullet)
				{
					hit.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.GetComponent<PlayerHealth>().DamagePlayer1(this.Damage);
				}
				else if (!this.isEnemyBullet && hit.GetComponent<Collider>().gameObject.tag == this.TargetTag[2])
				{
					ObstacleExplosion component = hit.GetComponent<Collider>().gameObject.GetComponent<ObstacleExplosion>();
					if (component != null)
					{
						if (this.bulletType == Damage_Tps.BulletType.RPG)
						{
							component.setDamage(800f);
						}
						else
						{
							component.setDamage(100f);
						}
					}
				}
			}
		}
		yield return null;
		this.resendToPool();
		yield break;
	}

	private void NormalDamage(Collision collision)
	{
		if (collision.gameObject.GetComponent<DamageManager>())
		{
			collision.gameObject.GetComponent<DamageManager>().ApplyDamage(this.Damage);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (this.isCollided)
		{
			return;
		}
		this.isCollided = true;
		if (this.Explosive && !this.isEnemyBullet)
		{
			if (collision.gameObject.tag != "Player")
			{
				this.Active();
			}
			return;
		}
		if (this.isDecalsNeeded)
		{
			if ((collision.gameObject.GetComponent<PartDetector>() == null && collision.gameObject.tag != this.TargetTag[0]) || collision.gameObject.tag == "Player")
			{
				this.ApplyEffect(collision);
				if (this.Effect)
				{
					GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.Effect, base.transform.position, base.transform.rotation);
					UnityEngine.Object.Destroy(obj, 2f);
				}
			}
			else if (collision.gameObject.tag == this.TargetTag[0] || collision.gameObject.GetComponent<PartDetector>() || collision.gameObject.transform.root.tag == "Enemy")
			{
				Quaternion rotation = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].point);
				GameObject objectForType = ObjectPool.instance.GetObjectForType("EnemyHitEffect_Pool", false);
				if (objectForType != null)
				{
					objectForType.transform.position = collision.contacts[0].point;
					objectForType.transform.rotation = rotation;
					objectForType.transform.SetParent(collision.transform);
					objectForType.SetActive(true);
				}
			}
		}
		if (this.HitedActive)
		{
			if (collision.gameObject.tag != "Particle" && collision.gameObject.tag != base.gameObject.tag)
			{
				if (!this.isEnemyBullet)
				{
					if (collision.gameObject.tag == this.TargetTag[0])
					{
						CommonEnemyBehaviour commonEnemyBehaviour = collision.gameObject.transform.root.transform.gameObject.GetComponent(typeof(CommonEnemyBehaviour)) as CommonEnemyBehaviour;
						AnimalController animalController = collision.gameObject.transform.root.transform.gameObject.GetComponent(typeof(AnimalController)) as AnimalController;
						switch (this.bulletType)
						{
						case Damage_Tps.BulletType.Pistol:
						case Damage_Tps.BulletType.MachineGun:
						case Damage_Tps.BulletType.Sniper:
						case Damage_Tps.BulletType.ShotGun:
						case Damage_Tps.BulletType.tps_Pistol:
						case Damage_Tps.BulletType.tps_ak47:
						case Damage_Tps.BulletType.tps_machineGun:
							if (commonEnemyBehaviour != null)
							{
								commonEnemyBehaviour.ApplyDamage(this.Damage, "normal", 0.3f, true);
							}
							if (animalController != null)
							{
								animalController.ApplyDamage((float)this.DamageForUkCharacter, "body");
							}
							break;
						case Damage_Tps.BulletType.None:
							if (commonEnemyBehaviour != null)
							{
								commonEnemyBehaviour.ApplyDamage(5f, "normal", 0.3f, true);
							}
							break;
						}
					}
					else if (collision.gameObject.GetComponent<PartDetector>() != null)
					{
						collision.gameObject.GetComponent<PartDetector>().ApplyDamage(this.Damage);
					}
					else if (!this.isEnemyBullet && collision.gameObject.tag == "ExplosiveObstacles")
					{
						ObstacleExplosion component = collision.gameObject.GetComponent<ObstacleExplosion>();
						if (component != null)
						{
							if (this.bulletType == Damage_Tps.BulletType.RPG)
							{
								component.setDamage(300f);
							}
							else
							{
								component.setDamage(50f);
							}
						}
					}
				}
				else if (this.isEnemyBullet && ((collision.gameObject.tag == "Player" && Singleton<GameController>.Instance.refAllController != null && collision.gameObject == Singleton<GameController>.Instance.refAllController.getCurrentController().gameObject) || collision.gameObject.tag == "MachineGunPlayer"))
				{
					Damage_Tps.BulletType bulletType = this.bulletType;
					if (bulletType != Damage_Tps.BulletType.EnemyTankBullet)
					{
						if (bulletType != Damage_Tps.BulletType.None)
						{
							if (bulletType == Damage_Tps.BulletType.MachineGun)
							{
								collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer1(this.Damage);
							}
						}
						else
						{
							collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer1(this.Damage);
						}
					}
					else
					{
						collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer1(0.3f);
					}
				}
				else if (!this.isEnemyBullet && collision.gameObject.tag == "ExplosiveObstacles")
				{
					ObstacleExplosion component2 = collision.gameObject.GetComponent<ObstacleExplosion>();
					if (component2 != null)
					{
						if (this.bulletType == Damage_Tps.BulletType.RPG)
						{
							component2.setDamage(300f);
						}
						else
						{
							component2.setDamage(50f);
						}
					}
				}
				else if (!this.isEnemyBullet && collision.gameObject.GetComponent<SpecialTaskObject>() != null)
				{
					SpecialTaskObject specialTaskObject = collision.gameObject.GetComponent(typeof(SpecialTaskObject)) as SpecialTaskObject;
					if (specialTaskObject != null)
					{
						specialTaskObject.ApplyDamage(100f);
					}
					MonoBehaviour.print(" collision inside");
				}
				if (!this.isEnemyBullet && collision.gameObject.GetComponent<AnimalPartDetector>() != null)
				{
					AnimalPartDetector animalPartDetector = collision.gameObject.GetComponent(typeof(AnimalPartDetector)) as AnimalPartDetector;
					MonoBehaviour.print(" collision inside");
				}
				if (this.isEnemyBullet)
				{
					this.resendToPool();
				}
				else
				{
					this.Active();
				}
			}
			else if (collision.gameObject.tag == base.gameObject.tag)
			{
				this.resendToPool();
			}
			else
			{
				this.resendToPool();
			}
		}
	}

	public bool Explosive;

	public float ExplosionRadius = 1f;

	public float ExplosionForce = 1000f;

	public bool HitedActive = true;

	public float TimeActive;

	private float timetemp;

	public bool isEnemyBullet;

	private int DamageForUkCharacter = 20;

	private bool isCollided;

	public bool isDecalsNeeded = true;

	public bool isPooling = true;

	public Damage_Tps.BulletType bulletType = Damage_Tps.BulletType.None;

	public enum BulletType
	{
		Pistol,
		MachineGun,
		Sniper,
		RPG,
		None,
		EnemyTankBullet,
		ShotGun,
		Gernade,
		tps_Pistol,
		tps_ak47,
		tps_machineGun
	}
}

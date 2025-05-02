// dnSpy decompiler from Assembly-CSharp.dll class: Damage
using System;
using System.Collections;
using UnityEngine;

public class Damage : DamageBase
{
	private void Start()
	{
		WeaponManagerStore weaponManager = Singleton<GameController>.Instance.weaponManager;
		if (weaponManager == null)
		{
			return;
		}
		switch (this.bulletType)
		{
		case global::Damage.BulletType.Pistol:
		case global::Damage.BulletType.MachineGun:
			this.DamageForUkCharacter = (int)Convert.ToInt16(weaponManager.GetGunModelWithId(1).GetDamageItem().GetCurrentItemValues());
			break;
		case global::Damage.BulletType.Sniper:
			this.DamageForUkCharacter = 100;
			break;
		case global::Damage.BulletType.RPG:
			this.DamageForUkCharacter = 100;
			break;
		case global::Damage.BulletType.ShotGun:
			this.DamageForUkCharacter = (int)Convert.ToInt16(weaponManager.GetGunModelWithId(2).GetDamageItem().GetCurrentItemValues());
			break;
		case global::Damage.BulletType.Gernade:
			this.DamageForUkCharacter = 100;
			break;
		}
		if (!this.Owner || !this.Owner.GetComponent<Collider>())
		{
			return;
		}
		if (base.GetComponent<Collider>().enabled && this.Owner.GetComponent<Collider>().enabled)
		{
			Physics.IgnoreCollision(base.GetComponent<Collider>(), this.Owner.GetComponent<Collider>());
		}
		this.timetemp = Time.time;
	}

	private void Update()
	{
		if (!this.HitedActive && Time.time >= this.timetemp + this.TimeActive)
		{
			this.Active();
		}
	}

	public void Active()
	{
		base.gameObject.tag = "Untagged";
		if (this.Effect)
		{
			GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.Effect, base.transform.position, base.transform.rotation);
			UnityEngine.Object.Destroy(obj, 2f);
		}
		if (this.Explosive)
		{
			base.StartCoroutine(this.ExplosionDamage());
		}
		Damage.BulletType bulletType = this.bulletType;
		if (bulletType == global::Damage.BulletType.RPG)
		{
			base.gameObject.SendMessage("Kill");
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private IEnumerator ExplosionDamage()
	{
		Collider[] hitColliders = Physics.OverlapSphere(base.transform.position, this.ExplosionRadius);
		UnityEngine.Debug.Log("hit lengt " + hitColliders.Length);
		foreach (Collider hit in hitColliders)
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
			}
		}
		yield return null;
		UnityEngine.Object.Destroy(base.gameObject, 0.1f);
		yield break;
	}

	private void ExplosionDamage1()
	{
		foreach (Collider collider in Physics.OverlapSphere(base.transform.position, this.ExplosionRadius))
		{
			if (collider)
			{
				if (collider.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.tag == this.TargetTag[0] && !this.isEnemyBullet)
				{
					CommonEnemyBehaviour commonEnemyBehaviour = collider.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.GetComponent(typeof(CommonEnemyBehaviour)) as CommonEnemyBehaviour;
					Damage.BulletType bulletType = this.bulletType;
					if (bulletType != global::Damage.BulletType.RPG)
					{
						if (bulletType == global::Damage.BulletType.Gernade)
						{
							if (commonEnemyBehaviour != null)
							{
								commonEnemyBehaviour.ApplyDamage((float)this.DamageForUkCharacter, "normal", 0.3f, true);
							}
						}
					}
					else if (commonEnemyBehaviour != null)
					{
						commonEnemyBehaviour.ApplyDamage((float)this.DamageForUkCharacter, "normal", 0.3f, true);
					}
				}
				else if (collider.GetComponent<Collider>().gameObject.tag == this.TargetTag[1])
				{
					collider.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.GetComponent<PlayerHealth>().DamagePlayer(this.Damage, false);
				}
				else if (!this.isEnemyBullet && collider.GetComponent<Collider>().gameObject.tag == this.TargetTag[2])
				{
					ObstacleExplosion component = collider.GetComponent<Collider>().gameObject.GetComponent<ObstacleExplosion>();
					if (component != null)
					{
						if (this.isRocket)
						{
							component.setDamage(800f);
						}
						else
						{
							component.setDamage(100f);
						}
					}
				}
				if (collider.GetComponent<Rigidbody>())
				{
					collider.GetComponent<Rigidbody>().AddExplosionForce(this.ExplosionForce, base.transform.position, this.ExplosionRadius, 3f);
				}
			}
		}
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
		base.GetComponent<Collider>().enabled = false;
		Damage.BulletType bulletType = this.bulletType;
		if (bulletType == global::Damage.BulletType.RPG || bulletType == global::Damage.BulletType.Gernade)
		{
			MonoBehaviour.print("oncollided");
			this.Active();
		}
		if (this.HitedActive && collision.gameObject.tag != "Particle" && collision.gameObject.tag != base.gameObject.tag)
		{
			if (!this.isEnemyBullet && collision.gameObject.tag == this.TargetTag[0])
			{
				CommonEnemyBehaviour commonEnemyBehaviour = collision.gameObject.transform.root.transform.gameObject.GetComponent(typeof(CommonEnemyBehaviour)) as CommonEnemyBehaviour;
				AnimalController animalController = collision.gameObject.transform.root.transform.gameObject.GetComponent(typeof(AnimalController)) as AnimalController;
				MonoBehaviour.print(" collision inside");
				if (!this.isRocket)
				{
					switch (this.bulletType)
					{
					case global::Damage.BulletType.Pistol:
					case global::Damage.BulletType.MachineGun:
					case global::Damage.BulletType.Sniper:
					case global::Damage.BulletType.ShotGun:
						if (commonEnemyBehaviour != null)
						{
							commonEnemyBehaviour.ApplyDamage((float)this.DamageForUkCharacter, "normal", 0.3f, true);
						}
						if (animalController != null)
						{
							animalController.ApplyDamage((float)this.DamageForUkCharacter, "body");
						}
						break;
					case global::Damage.BulletType.None:
						if (commonEnemyBehaviour != null)
						{
							commonEnemyBehaviour.ApplyDamage(5f, "normal", 0.3f, true);
						}
						break;
					}
					MonoBehaviour.print(" collision after");
				}
			}
			else if (this.isEnemyBullet && collision.gameObject.tag == "Player" && Singleton<GameController>.Instance.refAllController != null && collision.gameObject == Singleton<GameController>.Instance.refAllController.getCurrentController().gameObject)
			{
				Damage.BulletType bulletType2 = this.bulletType;
				if (bulletType2 != global::Damage.BulletType.EnemyTankBullet)
				{
					if (bulletType2 == global::Damage.BulletType.None)
					{
						collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer(this.Damage, false);
					}
				}
				else
				{
					collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer(0.3f, false);
				}
			}
			else if (!this.isEnemyBullet && collision.gameObject.tag == this.TargetTag[2])
			{
				ObstacleExplosion component = collision.gameObject.GetComponent<ObstacleExplosion>();
				if (component != null)
				{
					if (this.isRocket)
					{
						component.setDamage(300f);
					}
					else
					{
						component.setDamage(50f);
					}
				}
			}
			else if (this.bulletType == global::Damage.BulletType.Gernade && collision.gameObject.tag == "Player")
			{
				base.GetComponent<Collider>().enabled = true;
				return;
			}
			if (!this.isEnemyBullet && collision.gameObject.GetComponent<AnimalPartDetector>() != null)
			{
				AnimalPartDetector animalPartDetector = collision.gameObject.GetComponent(typeof(AnimalPartDetector)) as AnimalPartDetector;
				MonoBehaviour.print(" collision inside");
				if (!this.isRocket)
				{
					switch (this.bulletType)
					{
					case global::Damage.BulletType.Pistol:
					case global::Damage.BulletType.MachineGun:
					case global::Damage.BulletType.Sniper:
					case global::Damage.BulletType.ShotGun:
						if (animalPartDetector != null)
						{
							animalPartDetector.ApplyDamage((float)this.DamageForUkCharacter);
						}
						break;
					case global::Damage.BulletType.None:
						if (animalPartDetector != null)
						{
							animalPartDetector.ApplyDamage(5f);
						}
						break;
					}
					MonoBehaviour.print(" collision after");
				}
			}
			this.Active();
		}
	}

	public bool Explosive;

	public float ExplosionRadius = 1f;

	public float ExplosionForce = 1000f;

	public bool HitedActive = true;

	public float TimeActive;

	private float timetemp;

	public bool isEnemyBullet;

	public bool isRocket;

	public bool damageFromBullet;

	private int DamageForUkCharacter = 20;

	public Damage.BulletType bulletType = global::Damage.BulletType.None;

	public enum BulletType
	{
		Pistol,
		MachineGun,
		Sniper,
		RPG,
		None,
		EnemyTankBullet,
		ShotGun,
		Gernade
	}
}

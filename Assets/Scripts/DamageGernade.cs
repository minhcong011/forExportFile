// dnSpy decompiler from Assembly-CSharp.dll class: DamageGernade
using System;
using System.Collections;
using UnityEngine;

public class DamageGernade : DamageBase
{
	private void Start()
	{
		base.Invoke("Active", 7f);
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
		this.isCollided = false;
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
	}

	private IEnumerator ExplosionDamage()
	{
		base.CancelInvoke("Active");
		MeshRenderer i = base.GetComponentInChildren<MeshRenderer>();
		if (i != null)
		{
			UnityEngine.Object.Destroy(i.gameObject);
		}
		foreach (Collider hit in Physics.OverlapSphere(base.transform.position, this.ExplosionRadius))
		{
			if (hit)
			{
				if (hit.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.tag == "Enemy")
				{
					CommonEnemyBehaviour behav = hit.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.GetComponent<CommonEnemyBehaviour>();
					if (behav != null && !behav.isDead)
					{
						behav.ApplyDamage(200f, "normal", 0.4f, false);
						if (hit.GetComponent<Rigidbody>() == null)
						{
							hit.gameObject.AddComponent<Rigidbody>();
						}
						if (hit.GetComponent<Rigidbody>())
						{
							hit.GetComponent<Rigidbody>().AddExplosionForce(this.ExplosionForce, base.transform.position, 10f, 2f);
						}
						yield return new WaitForSeconds(0.035f);
					}
				}
			}
		}
		yield return null;
		UnityEngine.Object.Destroy(base.gameObject, 0.1f);
		yield break;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (this.isCollided)
		{
			return;
		}
		UnityEngine.Debug.Log("collider" + collision.gameObject.tag);
		if (collision.collider.gameObject.tag != "Particle" && collision.collider.gameObject.tag != "Player")
		{
			this.isCollided = true;
			UnityEngine.Debug.Log("colli" + collision.gameObject.name);
			base.Invoke("Active", 1f);
		}
	}

	public bool Explosive;

	public float ExplosionRadius = 1f;

	public float ExplosionForce = 1000f;

	private bool isCollided;

	public DamageGernade.BulletType bulletType = DamageGernade.BulletType.None;

	public enum BulletType
	{
		Gernade,
		tps_machineGun,
		None
	}
}

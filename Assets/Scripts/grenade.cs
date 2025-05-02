// dnSpy decompiler from Assembly-CSharp.dll class: grenade
using System;
using System.Collections;
using UnityEngine;

public class grenade : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.waitanddestroy());
	}

	private void update()
	{
		if (this.hitpoints <= 0f)
		{
			this.explode();
		}
	}

	private void explode()
	{
		UnityEngine.Object.Instantiate<Transform>(this.explosion, base.transform.position, base.transform.rotation);
		base.StartCoroutine(this.ExplosionDamage());
		UnityEngine.Object.Destroy(base.gameObject, 0.7f);
	}

	private IEnumerator waitanddestroy()
	{
		yield return new WaitForSeconds(this.waitTime);
		this.explode();
		yield break;
	}

	private void Damage(float damage)
	{
		this.hitpoints -= damage;
	}

	private IEnumerator ExplosionDamage()
	{
		foreach (Collider hit in Physics.OverlapSphere(base.transform.position, this.explodeRadius))
		{
			if (hit)
			{
				if (hit.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.tag == "Enemy")
				{
					hit.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.GetComponent<CommonEnemyBehaviour>().ApplyDamage(200f, "normal", 0.25f, true);
					if (hit.GetComponent<Rigidbody>())
					{
						hit.GetComponent<Rigidbody>().AddExplosionForce(200f, base.transform.position, this.explodeRadius, 3f);
					}
					yield return new WaitForSeconds(0.03f);
				}
				else if (hit.GetComponent<Collider>().gameObject.tag == "ExplosiveObstacles")
				{
					ObstacleExplosion component = hit.GetComponent<Collider>().gameObject.GetComponent<ObstacleExplosion>();
					if (component != null)
					{
						component.setDamage(800f);
					}
				}
			}
		}
		yield return null;
		yield break;
	}

	public Transform explosion;

	public float waitTime = 2f;

	public float hitpoints = 50f;

	public float explodeRadius = 6f;
}

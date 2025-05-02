// dnSpy decompiler from Assembly-CSharp.dll class: molotovcocktail
using System;
using System.Collections;
using UnityEngine;

public class molotovcocktail : MonoBehaviour
{
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
		UnityEngine.Object.Instantiate<Transform>(this.brokenmodel, base.transform.position, base.transform.rotation);
		base.StartCoroutine(this.ExplosionDamage());
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (this.isCollided)
		{
			return;
		}
		this.isCollided = true;
		this.explode();
	}

	private void Damage(float damage)
	{
		this.hitpoints -= damage;
	}

	private IEnumerator ExplosionDamage()
	{
		foreach (Collider hit in Physics.OverlapSphere(base.transform.position, 6f))
		{
			if (hit)
			{
				if (hit.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.tag == "Enemy")
				{
					hit.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.GetComponent<CommonEnemyBehaviour>().ApplyDamage(200f, "normal", 0.25f, true);
					if (hit.GetComponent<Rigidbody>())
					{
						hit.GetComponent<Rigidbody>().AddExplosionForce(200f, base.transform.position, 6f, 3f);
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
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	public Transform explosion;

	public float waitTime = 2f;

	public float hitpoints = 50f;

	public Transform brokenmodel;

	private bool isCollided;
}

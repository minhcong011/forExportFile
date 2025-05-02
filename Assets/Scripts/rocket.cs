// dnSpy decompiler from Assembly-CSharp.dll class: rocket
using System;
using System.Collections;
using UnityEngine;

public class rocket : MonoBehaviour
{
	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject, this.waitTime);
	}

	private void Update()
	{
		base.GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, this.speed);
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

	private void OnCollisionEnter(Collision hit)
	{
		if (this.isCollided)
		{
			return;
		}
		this.isCollided = true;
		UnityEngine.Object.Instantiate<GameObject>(this.explosion, base.transform.position, Quaternion.identity);
		base.StartCoroutine(this.ExplosionDamage());
	}

	public float speed = 100f;

	public GameObject explosion;

	public float waitTime = 5f;

	public float explodeRadius = 5f;

	private bool isCollided;
}

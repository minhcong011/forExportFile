// dnSpy decompiler from Assembly-CSharp.dll class: remotebomb
using System;
using System.Collections;
using UnityEngine;

public class remotebomb : MonoBehaviour
{
	public void detonate()
	{
		base.StartCoroutine(this.explode(this.waitdetonate));
	}

	private IEnumerator explode(float waittime)
	{
		yield return new WaitForSeconds(waittime);
		UnityEngine.Object.Instantiate<Transform>(this.explosion, base.transform.position, base.transform.rotation);
		base.StartCoroutine(this.ExplosionDamage());
		yield break;
	}

	private IEnumerator ExplosionDamage()
	{
		foreach (Collider hit in Physics.OverlapSphere(base.transform.position, 5f))
		{
			if (hit)
			{
				if (hit.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.tag == "Enemy")
				{
					hit.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.GetComponent<CommonEnemyBehaviour>().ApplyDamage(200f, "normal", 0.25f, true);
					if (hit.GetComponent<Rigidbody>())
					{
						hit.GetComponent<Rigidbody>().AddExplosionForce(200f, base.transform.position, 5f, 3f);
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

	public float waitdetonate = 0.8f;

	public Transform explosion;
}

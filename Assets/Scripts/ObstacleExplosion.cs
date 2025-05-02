// dnSpy decompiler from Assembly-CSharp.dll class: ObstacleExplosion
using System;
using System.Collections;
using UnityEngine;

public class ObstacleExplosion : MonoBehaviour
{
	private void Start()
	{
	}

	public void setDamage(float val)
	{
		if (this.destroyed)
		{
			return;
		}
		this.health -= val;
		if (this.health <= 0f)
		{
			this.destroyed = true;
			this.ShowEffect();
		}
	}

	public void ShowEffect()
	{
		if (this.particleEffect)
		{
			GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.particleEffect, base.transform.position, base.transform.rotation);
			Singleton<GameController>.Instance.soundController.PlayBlastSound();
			UnityEngine.Object.Destroy(obj, 3f);
		}
		if (this.Explosive)
		{
			base.StartCoroutine(this.ExplosionDamage());
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject, 0.1f);
	}

	private IEnumerator ExplosionDamage()
	{
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
					hit.GetComponent<Collider>().gameObject.transform.root.transform.gameObject.GetComponent<CommonEnemyBehaviour>().ApplyDamage(200f, "normal", 0.2f, true);
					yield return new WaitForSeconds(0.02f);
				}
				if (hit.GetComponent<Rigidbody>())
				{
					hit.GetComponent<Rigidbody>().AddExplosionForce(200f, base.transform.position, this.ExplosionRadius, 3f);
				}
			}
		}
		yield return null;
		UnityEngine.Object.Destroy(base.gameObject, 0.1f);
		yield break;
	}

	private IEnumerator killEnemy(CommonEnemyBehaviour refBehav)
	{
		UnityEngine.Debug.Log("coming in routine");
		yield return new WaitForSeconds(0.1f);
		refBehav.ApplyDamage(200f, "normal", 0.3f, true);
		yield break;
	}

	public float health = 100f;

	public float ExplosionRadius = 15f;

	private bool destroyed;

	public bool Explosive;

	public GameObject particleEffect;
}

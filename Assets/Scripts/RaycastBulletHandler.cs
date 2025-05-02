// dnSpy decompiler from Assembly-CSharp.dll class: RaycastBulletHandler
using System;
using UnityEngine;

public class RaycastBulletHandler : MonoBehaviour
{
	private void Start()
	{
	}

	public void InitBullet(RaycastHit hit, float damage)
	{
		UnityEngine.Debug.Log(" coming bullet" + hit.collider.gameObject.tag);
		CommonEnemyBehaviour commonEnemyBehaviour = hit.collider.transform.root.gameObject.GetComponent(typeof(CommonEnemyBehaviour)) as CommonEnemyBehaviour;
		if (hit.collider.gameObject.GetComponent<PartDetector>() != null)
		{
			hit.collider.gameObject.GetComponent<PartDetector>().ApplyDamage(damage);
			this.ApplyEffect(hit);
		}
		else if (commonEnemyBehaviour != null)
		{
			if (commonEnemyBehaviour != null)
			{
				commonEnemyBehaviour.ApplyDamage(damage, "normal", 0.3f, true);
			}
			if (hit.collider.gameObject.tag == "Enemy")
			{
			}
			this.ApplyEffect(hit);
		}
		else if (hit.collider.gameObject.tag == "ExplosiveObstacles")
		{
			ObstacleExplosion component = hit.collider.gameObject.GetComponent<ObstacleExplosion>();
			if (component != null)
			{
				component.setDamage(150f);
			}
		}
		else
		{
			Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.point);
			if (this.bulletHitEffect != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bulletHitEffect);
				gameObject.transform.position = hit.point;
				gameObject.transform.rotation = rotation;
				gameObject.SetActive(true);
			}
		}
	}

	private void ApplyEffect(RaycastHit coll)
	{
		Quaternion rotation = Quaternion.FromToRotation(Vector3.up, coll.point);
		GameObject objectForType = ObjectPool.instance.GetObjectForType("EnemyHitEffect_Pool", false);
		if (objectForType != null)
		{
			objectForType.transform.position = coll.point;
			objectForType.transform.rotation = rotation;
			objectForType.transform.SetParent(coll.transform);
			objectForType.SetActive(true);
		}
		if (this.bulletHitEffect != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bulletHitEffect);
			gameObject.transform.position = coll.point;
			gameObject.transform.rotation = rotation;
			gameObject.SetActive(true);
		}
	}

	public GameObject bulletHitEffect;
}

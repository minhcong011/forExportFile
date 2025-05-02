// dnSpy decompiler from Assembly-CSharp.dll class: raycastfire
using System;
using UnityEngine;
using UnityEngine.UI;

public class raycastfire : MonoBehaviour
{
	public void fireMelee()
	{
		Vector3 forward = base.transform.forward;
		Vector3 up = base.transform.up;
		Vector3 right = base.transform.right;
		Vector3 vector = forward;
		vector += UnityEngine.Random.Range(-0.1f, 0.1f) * up + UnityEngine.Random.Range(-0.1f, 0.1f) * right;
		Ray ray = new Ray(base.transform.position, vector);
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(ray, out raycastHit, 3f, this.mask))
		{
			if (raycastHit.rigidbody)
			{
				raycastHit.rigidbody.AddForceAtPosition(500f * forward, raycastHit.point);
			}
			raycastHit.transform.SendMessageUpwards("Damage", 50f, SendMessageOptions.DontRequireReceiver);
			if (raycastHit.transform.tag == "flesh")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impactblood, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
				gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, (float)UnityEngine.Random.Range(-90, 90), 0f);
			}
			else
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impactmelee, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
				gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, (float)UnityEngine.Random.Range(-90, 90), 0f);
				gameObject.transform.parent = raycastHit.transform;
			}
		}
	}

	private void Update()
	{
		this.ray1 = new Ray(base.transform.position, base.transform.forward);
		this.hit1 = default(RaycastHit);
		if (Physics.Raycast(this.ray1, out this.hit1, this.range, this.mask))
		{
			CommonEnemyBehaviour x = this.hit1.collider.gameObject.transform.root.transform.gameObject.GetComponent(typeof(CommonEnemyBehaviour)) as CommonEnemyBehaviour;
			if (this.hit1.collider.gameObject.GetComponent<PartDetector>() != null)
			{
				if (this.autoFire)
				{
					this.rayCastFirerAutoFireAim = true;
				}
				this.isAimOnTarget = true;
			}
			else if (x != null)
			{
				if (this.autoFire)
				{
					this.rayCastFirerAutoFireAim = true;
				}
				this.isAimOnTarget = true;
			}
			else
			{
				this.rayCastFirerAutoFireAim = false;
				this.isAimOnTarget = false;
			}
		}
		else
		{
			this.rayCastFirerAutoFireAim = false;
			this.isAimOnTarget = false;
		}
		if (this.isAimOnTarget)
		{
			this.aim.color = Color.red;
		}
		else
		{
			this.aim.color = Color.green;
		}
	}

	public void fire()
	{
		this.Fired();
		for (int i = 0; i < this.projectilecount; i++)
		{
			this.firebullet();
		}
	}

	private void firebullet()
	{
		Vector3 forward = base.transform.forward;
		Vector3 up = base.transform.up;
		Vector3 right = base.transform.right;
		Vector3 vector = forward;
		vector += UnityEngine.Random.Range(-this.inaccuracy, this.inaccuracy) * up + UnityEngine.Random.Range(-this.inaccuracy, this.inaccuracy) * right;
		Ray ray = new Ray(base.transform.position, vector);
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(ray, out raycastHit, this.range, this.mask))
		{
			if (raycastHit.rigidbody)
			{
				raycastHit.rigidbody.AddForceAtPosition(this.force * forward, raycastHit.point);
			}
			CommonEnemyBehaviour commonEnemyBehaviour = raycastHit.collider.gameObject.transform.root.transform.gameObject.GetComponent(typeof(CommonEnemyBehaviour)) as CommonEnemyBehaviour;
			if (raycastHit.collider.gameObject.GetComponent<PartDetector>() != null)
			{
				raycastHit.collider.gameObject.GetComponent<PartDetector>().ApplyDamage(this.damage);
			}
			else if (commonEnemyBehaviour != null)
			{
				commonEnemyBehaviour.ApplyDamage(this.damage, "normal", 0.3f, true);
			}
			if (raycastHit.transform.tag == "Untagged")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impactnormal, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
				gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, (float)UnityEngine.Random.Range(-90, 90), 0f);
				gameObject.transform.localEulerAngles = new Vector3(0f, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
				gameObject.transform.parent = raycastHit.transform;
			}
			else if (raycastHit.transform.tag == "concrete")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impactconcrete, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
				gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, (float)UnityEngine.Random.Range(-90, 90), 0f);
				gameObject.transform.parent = raycastHit.transform;
			}
			else if (raycastHit.transform.tag == "nodecal")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impactnodecal, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
				gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, (float)UnityEngine.Random.Range(-90, 90), 0f);
				gameObject.transform.parent = raycastHit.transform;
			}
			else if (raycastHit.transform.tag == "metal")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impactmetal, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
				gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, (float)UnityEngine.Random.Range(-90, 90), 0f);
				gameObject.transform.parent = raycastHit.transform;
			}
			else if (raycastHit.transform.tag == "wood")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impactwood, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
				gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, (float)UnityEngine.Random.Range(-90, 90), 0f);
				gameObject.transform.parent = raycastHit.transform;
			}
			else if (raycastHit.transform.tag == "water")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impactwater, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
				gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, (float)UnityEngine.Random.Range(-90, 90), 0f);
				gameObject.transform.parent = raycastHit.transform;
			}
			else if (raycastHit.transform.tag == "glass")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impactglass, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
				gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, (float)UnityEngine.Random.Range(-90, 90), 0f);
				gameObject.transform.parent = raycastHit.transform;
			}
			else if (raycastHit.transform.tag == "flesh")
			{
				raycastHit.transform.SendMessageUpwards("hitvector", raycastHit.point, SendMessageOptions.DontRequireReceiver);
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impactblood, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
				gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, (float)UnityEngine.Random.Range(-90, 90), 0f);
			}
			else if (raycastHit.transform.tag == "Enemy")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impactblood, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
				gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, (float)UnityEngine.Random.Range(-90, 90), 0f);
			}
			else if (raycastHit.transform.tag == "ExplosiveObstacles")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impactmetal, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
				gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, (float)UnityEngine.Random.Range(-90, 90), 0f);
				gameObject.transform.parent = raycastHit.transform;
				ObstacleExplosion component = raycastHit.collider.gameObject.GetComponent<ObstacleExplosion>();
				if (component != null)
				{
					component.setDamage(this.damage);
				}
			}
		}
	}

	public void AutoFirePressed()
	{
		this.autoFire = !this.autoFire;
	}

	public void Fired()
	{
		if (!this.firstFired)
		{
			if (Singleton<GameController>.Instance.gameSceneController != null && !Singleton<GameController>.Instance.gameSceneController.canAlert)
			{
				Singleton<GameController>.Instance.gameSceneController.AlertEnemies();
			}
			this.firstFired = true;
		}
	}

	public float force = 500f;

	public float damage = 50f;

	public float range = 100f;

	public LayerMask mask;

	public int projectilecount = 1;

	public float inaccuracy = 0.1f;

	public GameObject impactnormal;

	public GameObject impactconcrete;

	public GameObject impactwood;

	public GameObject impactblood;

	public GameObject impactwater;

	public GameObject impactmetal;

	public GameObject impactglass;

	public GameObject impactmelee;

	public GameObject impactnodecal;

	public bool autoFire = true;

	public bool rayCastFirerAutoFireAim;

	public Image aim;

	private bool isAimOnTarget;

	private bool firstFired;

	private Ray ray1;

	private RaycastHit hit1;
}

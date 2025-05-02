// dnSpy decompiler from Assembly-CSharp.dll class: explosive
using System;
using UnityEngine;

public class explosive : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (this.hitpoints <= 0f)
		{
			UnityEngine.Object.Instantiate<Transform>(this.spawnobject, base.transform.position, base.transform.rotation);
			UnityEngine.Object.Instantiate<GameObject>(this.explosion, base.transform.position, Quaternion.identity);
			Vector3 position = base.transform.position;
			Collider[] array = Physics.OverlapSphere(position, this.radius);
			foreach (Collider collider in array)
			{
				if (collider.GetComponent<Rigidbody>() != null)
				{
					Rigidbody component = collider.GetComponent<Rigidbody>();
					component.AddExplosionForce(this.power, position, this.radius, 3f);
				}
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Damage(float damage)
	{
		this.hitpoints -= damage;
	}

	public float hitpoints = 100f;

	public Transform spawnobject;

	public GameObject explosion;

	public float radius = 3f;

	public float power = 100f;
}

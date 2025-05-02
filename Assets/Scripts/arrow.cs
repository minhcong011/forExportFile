// dnSpy decompiler from Assembly-CSharp.dll class: arrow
using System;
using UnityEngine;

public class arrow : MonoBehaviour
{
	private void Update()
	{
		if (base.GetComponent<Rigidbody>() != null)
		{
			base.GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, this.speed);
		}
	}

	public void SetDamage(float d)
	{
		this.damage = d;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Vector3 forward = base.transform.forward;
		ContactPoint contactPoint = collision.contacts[0];
		Collider collider = collision.collider;
		base.transform.parent = collider.transform;
		base.transform.position = contactPoint.point;
		if (collider.GetComponent<Rigidbody>())
		{
			collider.GetComponent<Rigidbody>().AddForceAtPosition(100f * forward, contactPoint.point);
		}
		CommonEnemyBehaviour commonEnemyBehaviour = collider.gameObject.transform.root.transform.gameObject.GetComponent(typeof(CommonEnemyBehaviour)) as CommonEnemyBehaviour;
		if (collider.gameObject.GetComponent<PartDetector>() != null)
		{
			collider.gameObject.GetComponent<PartDetector>().ApplyDamage(this.damage);
		}
		else if (commonEnemyBehaviour != null)
		{
			commonEnemyBehaviour.ApplyDamage(this.damage, "normal", 0.3f, true);
		}
		UnityEngine.Object.Destroy(base.GetComponent<Rigidbody>(), 0.1f);
	}

	public float speed = 100f;

	public float damage = 50f;
}

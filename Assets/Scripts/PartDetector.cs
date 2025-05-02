// dnSpy decompiler from Assembly-CSharp.dll class: PartDetector
using System;
using UnityEngine;

public class PartDetector : MonoBehaviour
{
	private void OnEnable()
	{
		if (base.GetComponent<SphereCollider>() != null)
		{
			base.GetComponent<SphereCollider>().enabled = true;
		}
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().isKinematic = false;
		}
	}

	public void ApplyDamage(float d)
	{
		this.enemyController.ApplyDamage(d, this.partName, 0.3f, true);
	}

	public string partName = "body";

	public CommonEnemyBehaviour enemyController;
}

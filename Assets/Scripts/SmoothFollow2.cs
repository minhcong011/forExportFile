// dnSpy decompiler from Assembly-CSharp.dll class: SmoothFollow2
using System;
using UnityEngine;

public class SmoothFollow2 : MonoBehaviour
{
	private void Update()
	{
		Vector3 b;
		if (this.followBehind)
		{
			b = this.target.TransformPoint(0f, this.height, -this.distance);
		}
		else
		{
			b = this.target.TransformPoint(0f, this.height, this.distance);
		}
		base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * this.damping);
		if (this.smoothRotation)
		{
			Quaternion b2 = Quaternion.LookRotation(this.target.position - base.transform.position, this.target.up);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b2, Time.deltaTime * this.rotationDamping);
		}
		else
		{
			base.transform.LookAt(this.target, this.target.up);
		}
	}

	public Transform target;

	public float distance = 3f;

	public float height = 3f;

	public float damping = 5f;

	public bool smoothRotation = true;

	public bool followBehind = true;

	public float rotationDamping = 10f;
}

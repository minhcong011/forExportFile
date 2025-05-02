// dnSpy decompiler from Assembly-CSharp.dll class: smoothLookAt
using System;
using UnityEngine;

public class smoothLookAt : MonoBehaviour
{
	private void Start()
	{
	}

	private void LateUpdate()
	{
		Quaternion b = Quaternion.LookRotation(this.target.position - base.transform.position);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * this.damping);
	}

	public Transform target;

	public float damping = 6f;
}

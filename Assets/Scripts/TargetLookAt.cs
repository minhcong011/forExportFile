// dnSpy decompiler from Assembly-CSharp.dll class: TargetLookAt
using System;
using UnityEngine;

public class TargetLookAt : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		base.transform.LookAt(this.lookAtObj);
	}

	public Transform lookAtObj;
}

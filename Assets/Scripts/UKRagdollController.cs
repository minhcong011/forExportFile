// dnSpy decompiler from Assembly-CSharp.dll class: UKRagdollController
using System;
using UnityEngine;

public class UKRagdollController : MonoBehaviour
{
	private void Awake()
	{
		foreach (Rigidbody rigidbody in base.transform.root.GetComponentsInChildren<Rigidbody>())
		{
			if (rigidbody.gameObject.GetComponent<PartDetector>() == null)
			{
				rigidbody.isKinematic = true;
				rigidbody.useGravity = false;
			}
		}
	}

	public void EnableRagdoll()
	{
		foreach (Rigidbody rigidbody in base.transform.root.GetComponentsInChildren<Rigidbody>())
		{
			rigidbody.isKinematic = false;
			rigidbody.useGravity = true;
		}
	}
}

// dnSpy decompiler from Assembly-CSharp.dll class: ScaleParticlesFromBound
using System;
using UnityEngine;

public class ScaleParticlesFromBound : MonoBehaviour
{
	private void GetMeshFilterParent(Transform t)
	{
		Collider component = t.parent.GetComponent<Collider>();
		if (component == null)
		{
			this.GetMeshFilterParent(t.parent);
		}
		else
		{
			this.targetCollider = component;
		}
	}

	private void Start()
	{
		this.GetMeshFilterParent(base.transform);
		if (this.targetCollider == null)
		{
			return;
		}
		Vector3 size = this.targetCollider.bounds.size;
		base.transform.localScale = size;
	}

	private void Update()
	{
	}

	private Collider targetCollider;
}

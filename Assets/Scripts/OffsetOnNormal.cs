// dnSpy decompiler from Assembly-CSharp.dll class: OffsetOnNormal
using System;
using UnityEngine;

public class OffsetOnNormal : MonoBehaviour
{
	private void Awake()
	{
		this.startPosition = base.transform.position;
	}

	private void OnEnable()
	{
		RaycastHit raycastHit;
		Physics.Raycast(this.startPosition, Vector3.down, out raycastHit);
		if (this.offsetGameObject != null)
		{
			base.transform.position = this.offsetGameObject.transform.position + raycastHit.normal * this.offset;
		}
		else
		{
			base.transform.position = raycastHit.point + raycastHit.normal * this.offset;
		}
	}

	private void Update()
	{
	}

	public float offset = 1f;

	public GameObject offsetGameObject;

	private Vector3 startPosition;
}

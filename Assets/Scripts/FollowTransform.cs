// dnSpy decompiler from Assembly-UnityScript.dll class: FollowTransform
using System;
using UnityEngine;

[Serializable]
public class FollowTransform : MonoBehaviour
{
	public virtual void Start()
	{
		this.thisTransform = this.transform;
	}

	public virtual void Update()
	{
		this.thisTransform.position = this.targetTransform.position;
		if (this.faceForward)
		{
			this.thisTransform.forward = this.targetTransform.forward;
		}
	}

	public virtual void Main()
	{
	}

	public Transform targetTransform;

	public bool faceForward;

	private Transform thisTransform;
}

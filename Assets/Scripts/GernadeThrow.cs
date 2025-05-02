// dnSpy decompiler from Assembly-CSharp.dll class: GernadeThrow
using System;
using UnityEngine;

public class GernadeThrow : MonoBehaviour
{
	private void Start()
	{
	}

	public void ThroughIt()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Gernade);
		gameObject.transform.localPosition = this.origin.position;
		gameObject.transform.forward = base.transform.forward;
		Physics.IgnoreCollision(base.gameObject.GetComponent<CapsuleCollider>(), gameObject.GetComponent<CapsuleCollider>());
	}

	private void Update()
	{
	}

	public GameObject Gernade;

	public Transform origin;
}

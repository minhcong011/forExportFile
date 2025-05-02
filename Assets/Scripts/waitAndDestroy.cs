// dnSpy decompiler from Assembly-CSharp.dll class: waitAndDestroy
using System;
using UnityEngine;

public class waitAndDestroy : MonoBehaviour
{
	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject, this.waitTime);
	}

	public float waitTime = 5f;
}

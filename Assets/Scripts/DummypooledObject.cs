// dnSpy decompiler from Assembly-CSharp.dll class: DummypooledObject
using System;
using UnityEngine;

public class DummypooledObject : MonoBehaviour
{
	private void Start()
	{
		base.Invoke("disableMe", 4f);
	}

	private void disableMe()
	{
		ObjectPool.instance.PoolObject(base.gameObject);
	}

	private void Update()
	{
	}
}

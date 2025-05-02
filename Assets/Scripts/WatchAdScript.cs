// dnSpy decompiler from Assembly-CSharp.dll class: WatchAdScript
using System;
using UnityEngine;

public class WatchAdScript : MonoBehaviour
{
	private void Start()
	{
	}

	public void DisableMe()
	{
		base.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			this.DisableMe();
		}
	}
}

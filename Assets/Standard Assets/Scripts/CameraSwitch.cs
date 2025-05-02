// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: CameraSwitch
using System;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
	private void Start()
	{
		this.cam2.enabled = false;
	}

	private void Update()
	{
	}

	public void switch2Cam2()
	{
		this.cam2.enabled = true;
		this.cam1.enabled = false;
		base.Invoke("startMission", 2f);
	}

	public void startMission()
	{
		base.gameObject.SendMessage("FadeIN");
	}

	public Camera cam1;

	public Camera cam2;
}

// dnSpy decompiler from Assembly-CSharp.dll class: PlayerSpecificUIControls
using System;
using UnityEngine;

public class PlayerSpecificUIControls : MonoBehaviour
{
	private void Start()
	{
	}

	public void showJoystick(bool show = true)
	{
		if (this.joystick != null)
		{
			this.joystick.SetActive(show);
			this.blocker.SetActive(show);
		}
	}

	public GameObject joystick;

	public GameObject blocker;
}

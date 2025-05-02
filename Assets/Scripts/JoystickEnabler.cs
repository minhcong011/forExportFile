// dnSpy decompiler from Assembly-CSharp.dll class: JoystickEnabler
using System;
using UnityEngine;

public class JoystickEnabler : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void enableJoystick()
	{
		this.joystick.SetActive(true);
	}

	public GameObject joystick;
}

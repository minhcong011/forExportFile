// dnSpy decompiler from Assembly-CSharp.dll class: NewFPSWeaponsUIController
using System;
using UnityEngine;

public class NewFPSWeaponsUIController : MonoBehaviour
{
	private void Start()
	{
		this.wSelector = UnityEngine.Object.FindObjectOfType<weaponselector>();
		if (this.wSelector == null)
		{
			UnityEngine.Debug.Log("wSelector not found");
		}
	}

	public void AddAmoos()
	{
		if (this.wSelector == null)
		{
			UnityEngine.Debug.Log("wSelector not found");
			return;
		}
		this.wSelector.AddWeaponAmmos(this.ammoCount);
	}

	public void ShowJoystick(bool val)
	{
		if (this.joystick != null)
		{
			this.joystick.SetActive(val);
		}
	}

	public weaponselector wSelector;

	public int ammoCount = 10;

	public GameObject joystick;
}

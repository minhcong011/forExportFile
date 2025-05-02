// dnSpy decompiler from Assembly-CSharp.dll class: PauseUI
using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
	private void Start()
	{
		float num = Constants.getSensitivity();
		this.sensitivity.value = num - 0.5f;
	}

	public void sensitivityChanged()
	{
		Constants.setSensitivity(this.sensitivity.value + 0.5f);
	}

	public void Resume()
	{
		MouseLooking[] array = UnityEngine.Object.FindObjectsOfType<MouseLooking>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].setSettingsSensitivity();
		}
		MouseLookingTPS[] array2 = UnityEngine.Object.FindObjectsOfType<MouseLookingTPS>();
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].setSettingsSensitivity();
		}
		MouseLookingMachine[] array3 = UnityEngine.Object.FindObjectsOfType<MouseLookingMachine>();
		for (int k = 0; k < array.Length; k++)
		{
		}
		base.UIClicks("Resume");
	}

	public Slider sensitivity;
}

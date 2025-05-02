// dnSpy decompiler from Assembly-CSharp.dll class: UICompatibility
using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class UICompatibility : MonoBehaviour
{
	public void SetCompatibility(bool value)
	{
		EasyTouch.SetUICompatibily(value);
	}
}

// dnSpy decompiler from Assembly-CSharp.dll class: RealTime
using System;
using UnityEngine;

public class RealTime : MonoBehaviour
{
	public static float time
	{
		get
		{
			return Time.unscaledTime;
		}
	}

	public static float deltaTime
	{
		get
		{
			return Time.unscaledDeltaTime;
		}
	}
}

// dnSpy decompiler from Assembly-CSharp.dll class: DamageBase
using System;
using UnityEngine;

public class DamageBase : MonoBehaviour
{
	public GameObject Effect;

	[HideInInspector]
	public GameObject Owner;

	public float Damage = 20f;

	public string[] TargetTag = new string[]
	{
		"Enemy"
	};
}

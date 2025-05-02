// dnSpy decompiler from Assembly-CSharp.dll class: BonusItem
using System;
using UnityEngine;

[Serializable]
public class BonusItem : IComparable
{
	public float Percent
	{
		get
		{
			return this.ProbabilityPercent / 100f;
		}
	}

	public int CompareTo(object obj)
	{
		return this.Percent.CompareTo(((BonusItem)obj).Percent);
	}

	public int ID;

	[Range(0f, 100f)]
	public float ProbabilityPercent;

	public float Angle;
}

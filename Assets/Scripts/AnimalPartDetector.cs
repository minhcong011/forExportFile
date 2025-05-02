// dnSpy decompiler from Assembly-CSharp.dll class: AnimalPartDetector
using System;
using UnityEngine;

public class AnimalPartDetector : MonoBehaviour
{
	public void ApplyDamage(float d)
	{
		this.animalController.ApplyDamage(d, this.partName);
	}

	public string partName = "body";

	public AnimalController animalController;
}

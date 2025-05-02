// dnSpy decompiler from Assembly-UnityScript.dll class: DisAppearObjects
using System;
using UnityEngine;

[Serializable]
public class DisAppearObjects : MonoBehaviour
{
	public virtual void OnDrawGizmos()
	{
		Component[] componentsInChildren = this.gameObject.GetComponentsInChildren(typeof(Transform));
		int i = 0;
		Component[] array = componentsInChildren;
		int length = array.Length;
		while (i < length)
		{
			Gizmos.DrawSphere(((Transform)array[i]).position, 0.25f);
			i++;
		}
	}

	public virtual void Main()
	{
	}
}

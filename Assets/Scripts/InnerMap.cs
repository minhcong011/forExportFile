// dnSpy decompiler from Assembly-CSharp.dll class: InnerMap
using System;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("MiniMap/Inner map")]
[RequireComponent(typeof(Image))]
public class InnerMap : MonoBehaviour
{
	public RectTransform InnerMapRect
	{
		get
		{
			if (!this._innerMapRect)
			{
				this._innerMapRect = base.GetComponent<RectTransform>();
			}
			return this._innerMapRect;
		}
	}

	public float getMapRadius()
	{
		Vector3[] array = new Vector3[4];
		this.InnerMapRect.GetLocalCorners(array);
		float result;
		if (Mathf.Abs(array[0].x) < Mathf.Abs(array[0].y))
		{
			result = Mathf.Abs(array[0].x);
		}
		else
		{
			result = Mathf.Abs(array[0].y);
		}
		return result;
	}

	private RectTransform _innerMapRect;
}

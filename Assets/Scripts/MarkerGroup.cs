// dnSpy decompiler from Assembly-CSharp.dll class: MarkerGroup
using System;
using UnityEngine;

[AddComponentMenu("MiniMap/Marker Group")]
[RequireComponent(typeof(RectTransform))]
public class MarkerGroup : MonoBehaviour
{
	public RectTransform MarkerGroupRect
	{
		get
		{
			if (!this._rectTransform)
			{
				this._rectTransform = base.GetComponent<RectTransform>();
			}
			return this._rectTransform;
		}
	}

	private RectTransform _rectTransform;
}

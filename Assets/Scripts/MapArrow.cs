// dnSpy decompiler from Assembly-CSharp.dll class: MapArrow
using System;
using UnityEngine;

[AddComponentMenu("MiniMap/Map arrow")]
public class MapArrow : MonoBehaviour
{
	public RectTransform ArrowRect
	{
		get
		{
			if (!this._arrowRect)
			{
				this._arrowRect = base.GetComponent<RectTransform>();
				if (!this._arrowRect)
				{
					UnityEngine.Debug.LogError("RectTransform not found. MapArrow script must by attached to an Image.");
				}
			}
			return this._arrowRect;
		}
	}

	public void rotate(Quaternion quat)
	{
		this.ArrowRect.rotation = quat;
	}

	private RectTransform _arrowRect;
}

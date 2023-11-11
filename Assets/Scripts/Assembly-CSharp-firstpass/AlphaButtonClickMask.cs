﻿using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000002 RID: 2
public class AlphaButtonClickMask : MonoBehaviour, ICanvasRaycastFilter
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public void Start()
	{
		this._image = base.GetComponent<Image>();
		Texture2D texture = this._image.sprite.texture;
		bool flag = false;
		if (texture != null)
		{
			try
			{
				texture.GetPixels32();
				goto IL_41;
			}
			catch (UnityException ex)
			{
				Debug.LogError(ex.Message);
				flag = true;
				goto IL_41;
			}
		}
		flag = true;
		IL_41:
		if (flag)
		{
			Debug.LogError("This script need an Image with a readbale Texture2D to work.");
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000020BC File Offset: 0x000002BC
	public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
	{
		Vector2 vector;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(this._image.rectTransform, sp, eventCamera, out vector);
		Vector2 pivot = this._image.rectTransform.pivot;
		Vector2 vector2 = new Vector2(pivot.x + vector.x / this._image.rectTransform.rect.width, pivot.y + vector.y / this._image.rectTransform.rect.height);
		Vector2 vector3 = new Vector2(this._image.sprite.rect.x + vector2.x * this._image.sprite.rect.width, this._image.sprite.rect.y + vector2.y * this._image.sprite.rect.height);
		vector3.x /= (float)this._image.sprite.texture.width;
		vector3.y /= (float)this._image.sprite.texture.height;
		return this._image.sprite.texture.GetPixelBilinear(vector3.x, vector3.y).a > 0.1f;
	}

	// Token: 0x04000001 RID: 1
	protected Image _image;
}

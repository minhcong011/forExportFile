// dnSpy decompiler from Assembly-CSharp.dll class: GamePromoItem
using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePromoItem : MonoBehaviour
{
	private void Start()
	{
	}

	public void SetInitials(GameIcons iconRef)
	{
		this.gameIconRef = iconRef;
		this.iconImage.sprite = iconRef.icon;
	}

	public void Clicked()
	{
		if (this.gameIconRef != null)
		{
			Application.OpenURL(this.gameIconRef.urlLink);
		}
	}

	public GameIcons gameIconRef;

	public Image iconImage;
}

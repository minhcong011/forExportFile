// dnSpy decompiler from Assembly-CSharp.dll class: AutoFireUIHandler
using System;
using UnityEngine;
using UnityEngine.UI;

public class AutoFireUIHandler : MonoBehaviour
{
	private void Start()
	{
		if (this.rayCastFirer == null)
		{
			this.rayCastFirer = UnityEngine.Object.FindObjectOfType<raycastfire>();
		}
		if (this.rayCastFirer.autoFire)
		{
			this.image.sprite = this.sprites[0];
		}
		else
		{
			this.image.sprite = this.sprites[1];
		}
	}

	public void BtnPressed()
	{
		this.rayCastFirer.AutoFirePressed();
		if (this.rayCastFirer.autoFire)
		{
			this.image.sprite = this.sprites[0];
		}
		else
		{
			this.image.sprite = this.sprites[1];
		}
	}

	public Sprite[] sprites;

	public Image image;

	public raycastfire rayCastFirer;
}

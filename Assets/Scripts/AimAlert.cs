// dnSpy decompiler from Assembly-CSharp.dll class: AimAlert
using System;
using UnityEngine;
using UnityEngine.UI;

public class AimAlert : MonoBehaviour
{
	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (Camera.main == null)
		{
			return;
		}
		Ray ray;
		if (this.lookingCam != null)
		{
			ray = this.lookingCam.ScreenPointToRay(RectTransformUtility.WorldToScreenPoint(null, base.transform.position));
		}
		else
		{
			ray = Camera.main.ScreenPointToRay(RectTransformUtility.WorldToScreenPoint(null, base.transform.position));
		}
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit))
		{
			if (raycastHit.collider.gameObject.tag == "Enemy" || raycastHit.collider.gameObject.transform.root.gameObject.tag == "Enemy")
			{
				this.aim.color = Color.red;
			}
			else
			{
				this.aim.color = Color.white;
			}
		}
		else
		{
			this.aim.color = Color.white;
		}
	}

	public Image aim;

	public Camera lookingCam;
}

// dnSpy decompiler from Assembly-CSharp.dll class: camerarotate
using System;
using ControlFreak2;
using UnityEngine;

public class camerarotate : MonoBehaviour
{
	private void Update()
	{
		if (CF2Input.GetKey(KeyCode.Escape))
		{
			this.stop = !this.stop;
		}
		if (this.stop)
		{
			CFCursor.lockState = CursorLockMode.Confined;
			CFCursor.visible = true;
		}
		else
		{
			CFCursor.lockState = CursorLockMode.Locked;
		}
		if (CF2Input.GetButton("Aim"))
		{
			this.sensitivityY = this.aimSens;
		}
		else
		{
			this.sensitivityY = this.normalSens;
		}
		this.rotationY += CF2Input.GetAxis("Mouse Y") * this.sensitivityY * this.smooth * (Time.deltaTime * this.speed);
		this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
		base.transform.localEulerAngles = new Vector3(-this.rotationY, base.transform.localEulerAngles.y, 0f);
	}

	public void dorecoil(float recoil)
	{
	}

	private float sensitivityY = 6f;

	public float minimumY = -70f;

	public float maximumY = 70f;

	private float rotationY;

	public float aimSens = 2f;

	public float normalSens = 6f;

	public float speed = 1f;

	public float smooth = 0.5f;

	private bool stop;
}

// dnSpy decompiler from Assembly-CSharp.dll class: playerrotate
using System;
using ControlFreak2;
using UnityEngine;

public class playerrotate : MonoBehaviour
{
	private void Awake()
	{
		this.refWeaponSelector = base.GetComponent<weaponselector>();
	}

	private void Update()
	{
		if (this.refWeaponSelector.isZoomed)
		{
			this.sensitivityX = this.aimSens;
		}
		else
		{
			this.sensitivityX = this.normalSens;
		}
		this.rotationX = CF2Input.GetAxis("Mouse X") * this.sensitivityX * this.smooth * (Time.deltaTime * this.speed);
		base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y + this.rotationX, 0f);
	}

	private float sensitivityX = 6f;

	public float aimSens = 2f;

	public float normalSens = 6f;

	private float rotationX;

	public float speed = 1f;

	public float smooth = 0.5f;

	public weaponselector refWeaponSelector;
}

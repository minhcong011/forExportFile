// dnSpy decompiler from Assembly-CSharp.dll class: SliderControl
using System;
using UnityEngine;

public class SliderControl : MonoBehaviour
{
	private void Start()
	{
	}

	public void OnValueChanged(float val)
	{
		this.sliderValue = val * 1f;
		this.obj.transform.localEulerAngles = new Vector3(this.sliderValue * 18f, this.obj.transform.localEulerAngles.y, this.obj.transform.localEulerAngles.z);
	}

	public UISlider slide;

	public GameObject obj;

	private float sliderValue;
}

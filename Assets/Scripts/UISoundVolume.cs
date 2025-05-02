// dnSpy decompiler from Assembly-CSharp.dll class: UISoundVolume
using System;
using UnityEngine;

[RequireComponent(typeof(UISlider))]
[AddComponentMenu("NGUI/Interaction/Sound Volume")]
public class UISoundVolume : MonoBehaviour
{
	private void Awake()
	{
		this.mSlider = base.GetComponent<UISlider>();
		this.mSlider.value = NGUITools.soundVolume;
		EventDelegate.Add(this.mSlider.onChange, new EventDelegate.Callback(this.OnChange));
	}

	private void OnChange()
	{
		NGUITools.soundVolume = UIProgressBar.current.value;
	}

	private UISlider mSlider;
}
